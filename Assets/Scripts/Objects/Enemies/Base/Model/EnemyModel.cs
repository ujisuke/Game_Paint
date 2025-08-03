using Cysharp.Threading.Tasks;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;
using System.Threading;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EnemyModel
    {
        private PA pA;
        private Status status;
        private HurtBox hurtBox;
        private readonly EStateMachine eStateMachine;
        private readonly EnemyData enemyData;
        private readonly EnemyController enemyController;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        public PA PA => pA;
        public HurtBox HurtBox => hurtBox;
        public EnemyData EnemyData => enemyData;
        public bool IsAttackSpeedDecreased => status.IsAttackSpeedDecreased;

        public EnemyModel(EnemyData enemyData, IEStateAfterBorn eStateAfterBorn, Vector2 pos, EnemyController enemyController, ColorEffectData colorEffectData)
        {
            this.enemyData = enemyData;
            pA = new PA(pos, 0f);
            status = new Status(new HP(enemyData.MaxHP), 0f, 0f, 0f, 0f, colorEffectData);
            hurtBox = new HurtBox(pA.Pos, enemyData.HurtBoxScale, true);
            eStateMachine = new EStateMachine(this, eStateAfterBorn, enemyController);
            this.enemyController = enemyController;
            ObjectsStorageModel.Instance.AddEnemy(this);
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        public void OnUpdate()
        {
            eStateMachine.OnUpdate();
            status = status.CountDown();
        }
        
        public void Move(Vector2 dir)
        {
            pA = pA.Move(dir);
            hurtBox = hurtBox.Move(pA.Pos);
        }

        public void ChangeState(IEState state) => eStateMachine.ChangeState(state);

        public float GetUP(string key) => enemyData.GetUP(key);

        public async UniTask TakeDamageFromFamiliar(FamiliarAttackModel familiarAttackModel)
        {
            status = status.TakeDamageFromFamiliar(familiarAttackModel);
            hurtBox = hurtBox.Inactivate();
            await UniTask.Delay(enemyData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.Activate();
        }

        public bool IsDead() => status.HP.IsDead();

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemoveEnemy();
            GameObject.Destroy(enemyController.gameObject);
        }
    }
}
