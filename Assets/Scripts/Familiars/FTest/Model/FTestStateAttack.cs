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

        public FTestStateAttack(FamiliarModel familiarModel) => fM = familiarModel;
        
        public IFState Initialize(FamiliarModel familiarModel) => new FTestStateMove(familiarModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateAttack");
            var newAttack = GameObject.Instantiate(fM.FamiliarData.AttackPrefab, fM.PSA.Pos, Quaternion.identity);
            attack = newAttack.GetComponent<ObjectAttackController>().ObjectAttackModel;
        }

        public void OnStateFixedUpdate()
        {
            if(fM.IsDead())
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
