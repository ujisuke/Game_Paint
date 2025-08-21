using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;

namespace Assets.Scripts.Objects.Familiars.Fish.Model
{
    public class FishStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private FamiliarAttackModel attack;
        private Vector2 targetPos;
        private float seconds;

        public FishStateAttack(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
            seconds = 0f;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new FishStateAttack(fM, fC);

        public void OnStateEnter()
        {
            var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<FamiliarAttackController>().Initialize(fM.FamiliarData, fM.IsEnemy, fM.ColorName);
            attack = newAttack.GetComponent<FamiliarAttackController>().FamiliarAttackModel;
            ResetTargetPos();
            fC.PlayAnim("Attack");
        }

        public void OnUpdate()
        {
            fM.MoveIgnoringStage(fM.FamiliarData.GetUP("Speed") * Time.deltaTime * (targetPos - fM.PA.Pos).normalized);
            attack.MoveIgnoringStage(fM.PA.Pos - attack.PA.Pos);
            if ((targetPos - fM.PA.Pos).magnitude <= 0.6f)
                ResetTargetPos();
            seconds += Time.deltaTime;
            if (seconds >= fM.FamiliarData.GetUP("Seconds"))
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }

        private void ResetTargetPos()
        {
            targetPos = fM.PA.Pos + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * fM.FamiliarData.GetUP("Range");
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
        }
    }
}
