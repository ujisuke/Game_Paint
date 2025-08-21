using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Objects.Familiars.Dog.Model
{
    public class DogStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private FamiliarAttackModel attack;

        public DogStateAttack(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new DogStateAttack(fM, fC);

        public void OnStateEnter()
        {
            var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<FamiliarAttackController>().Initialize(fM.FamiliarData, fM.IsEnemy, fM.ColorName);
            attack = newAttack.GetComponent<FamiliarAttackController>().FamiliarAttackModel;
            Attack().Forget();
        }

        private async UniTask Attack()
        {
            float moveSeconds = fM.FamiliarData.GetUP("MoveSeconds");
            fC.PlayAnim("Move", moveSeconds);
            Vector2 moveDir = 0.01f * fM.FamiliarData.GetUP("MoveRange") * (ObjectStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy) - fM.PA.Pos).normalized;
            for (float i = 0; i < 100; i++)
            {
                fM.MoveIgnoringStage(moveDir);
                attack.MoveIgnoringStage(fM.PA.Pos - attack.PA.Pos);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSeconds * 0.01f), cancellationToken: fM.Token);
            }
            float staySeconds = fM.FamiliarData.GetUP("StaySeconds");
            fC.PlayAnim("Stay", staySeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(staySeconds), cancellationToken: fM.Token);
            fM.ChangeState(new FStateDead(fM));
        }

        public void OnUpdate()
        {

        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
