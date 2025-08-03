using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;

namespace Assets.Scripts.Objects.Familiars.Scorpion.Model
{
    public class ScorpionStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private FamiliarAttackModel attack;

        public ScorpionStateAttack(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new ScorpionStateAttack(fM, fC);

        public void OnStateEnter()
        {
            var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<FamiliarAttackController>().Initialize(fM.FamiliarData, fM.IsEnemy, fM.ColorName);
            attack = newAttack.GetComponent<FamiliarAttackController>().FamiliarAttackModel;
            var targetPos = ObjectsStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy);
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
            fC.PlayAnim("Attack");
        }

        public void OnUpdate()
        {
            var targetPos = ObjectsStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy);
            fM.Move(fM.FamiliarData.GetUniqueParameter("Speed") * Time.deltaTime * (targetPos - fM.PA.Pos).normalized);
            attack.Move(fM.PA.Pos - attack.PA.Pos);
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
            if ((targetPos - fM.PA.Pos).magnitude <= 0.6f)
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
