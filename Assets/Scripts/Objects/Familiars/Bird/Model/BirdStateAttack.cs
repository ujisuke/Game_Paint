using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.Datas;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Objects.Familiars.Bird.Model
{
    public class BirdStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private FamiliarAttackModel attack;
        private Vector2 moveDir;
        private float secondsDelta;

        public BirdStateAttack(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new BirdStateAttack(fM, fC);

        public void OnStateEnter()
        {
            var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<FamiliarAttackController>().Initialize(fM.FamiliarData, fM.IsEnemy, fM.ColorName);
            attack = newAttack.GetComponent<FamiliarAttackController>().FamiliarAttackModel;
            Vector2 targetPos = ObjectStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy);
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
            fC.PlayAnim("Attack");
            float range = fM.FamiliarData.GetUP("Range");
            moveDir = (targetPos - fM.PA.Pos).normalized * range * 0.01f;
            secondsDelta = range / fM.FamiliarData.GetUP("Speed") * 0.01f;
            Attack().Forget();
        }

        private async UniTask Attack()
        {
            for (int i = 0; i < 100; i++)
            {
                fM.MoveIgnoringStage(moveDir);
                attack.MoveIgnoringStage(fM.PA.Pos - attack.PA.Pos);
                await UniTask.Delay(TimeSpan.FromSeconds(secondsDelta), cancellationToken: fM.Token);
            }
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
