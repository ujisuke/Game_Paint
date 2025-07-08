using Assets.Scripts.Familiars.Base.Model;
using Assets.Scripts.ObjectAttacks.Base.Controller;
using Assets.Scripts.ObjectAttacks.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Familiars.FTest.Model
{
    public class FTestStateAttack : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private ObjectAttackModel attack;
        private int i;

        public FTestStateAttack(FamiliarModel familiarModel) => fM = familiarModel;
        
        public IFState Initialize(FamiliarModel familiarModel) => new FTestStateMove(familiarModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateAttack");
            i = 0;
        }

        public void OnStateFixedUpdate()
        {
            if (i == 0)
            {
                var newAttack = GameObject.Instantiate(fM.FamiliarData.AttackPrefab, fM.PSA.Pos, Quaternion.identity);
                attack = newAttack.GetComponent<ObjectAttackController>().ObjectAttackModel;
            }
            if (i == 3)
                fM.ChangeState(new FStateDead(fM));
            i++;
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
