using System.Threading;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;

namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public class FamiliarModel
    {
        private PSA pSA;
        private Status status;
        private HurtBox hurtBox;
        private readonly FStateMachine fStateMachine;
        private readonly FamiliarData familiarData;
        private readonly FamiliarController familiarController;
        private readonly bool isEnemy;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        private readonly ColorName colorName;
        public PSA PSA => pSA;
        public HurtBox HurtBox => hurtBox;
        public ColorName ColorName => colorName;
        public FamiliarData FamiliarData => familiarData;
        public bool IsEnemy => isEnemy;

        public FamiliarModel(FamiliarData familiarData, IFStateAfterBorn fStateAfterBorn, Vector2 position, FamiliarController familiarController, ColorName colorName, bool isEnemy)
        {
            this.familiarData = familiarData;
            pSA = new PSA(position, familiarData.Scale, 0f);
            status = new Status(familiarData.MaxHP, 0f, 0f, 0f);
            hurtBox = new HurtBox(pSA.Pos, familiarData.HurtBoxScale, true);
            fStateMachine = new FStateMachine(this, fStateAfterBorn);
            this.familiarController = familiarController;
            this.colorName = colorName;
            this.isEnemy = isEnemy;
            ObjectsStorageModel.Instance.AddFamiliar(this, isEnemy);
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        public void OnUpdate() => fStateMachine.OnUpdate();

        public void Move(Vector2 dir)
        {
            pSA = pSA.Move(dir);
            hurtBox = hurtBox.Move(pSA.Pos);
        }
        
        public void ChangeState(IFState state) => fStateMachine.ChangeState(state);

        public async UniTask TakeDamageFromFamiliar(FamiliarAttackModel familiarAttackModel)
        {
            status = status.TakeDamageFromFamiliar(familiarAttackModel);
            hurtBox = hurtBox.Inactivate();
            await UniTask.Delay(familiarData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.Activate();
        }

        public async UniTask TakeDamageFromEnemy(int damageValue)
        {
            status = status.TakeDamageFromEnemy(damageValue);
            hurtBox = hurtBox.Inactivate();
            await UniTask.Delay(FamiliarData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.Activate();
        }

        public bool IsDead() => status.HP.IsDead();

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemoveFamiliar(this);
            GameObject.Destroy(familiarController.gameObject);
        }
    }
}
