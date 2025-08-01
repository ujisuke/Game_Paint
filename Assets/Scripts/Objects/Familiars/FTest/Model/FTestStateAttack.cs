using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.Objects.Familiars.Base.Controller;

namespace Assets.Scripts.Objects.Familiars.FTest.Model
{
    public class FTestStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private FamiliarController fC;
        private FamiliarAttackModel attack;
        private int i;

        public FTestStateAttack(FamiliarModel familiarModel) => fM = familiarModel;
        
        public IFState Initialize(FamiliarModel familiarModel, FamiliarController fC) => new FTestStateMove(familiarModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateAttack");
            var newAttack = GameObject.Instantiate(fM.AttackPrefab, fM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<FamiliarAttackController>().Initialize(fM.FamiliarData,fM.IsEnemy, fM.ColorName);
            attack = newAttack.GetComponent<FamiliarAttackController>().FamiliarAttackModel;
            i = 0;
        }

        public void OnUpdate()
        {
            if (i < 10)
                i++;
            else if (i >= 50)
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
