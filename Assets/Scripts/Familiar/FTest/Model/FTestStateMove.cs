using Assets.Scripts.Familiar.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Familiar.FTest.Model
{
    public class FTestStateMove : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;

        public FTestStateMove(FamiliarModel familiarModel) => fM = familiarModel;
        
        public IFState Initialize(FamiliarModel familiarModel) => new FTestStateMove(familiarModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateMove");
        }

        public void OnStateFixedUpdate()
        {
            fM.Move(fM.GetUP("Speed") * Vector2.right);
            if (Input.GetKey(KeyCode.Space))
                fM.ChangeState(new FStateDead(fM));
        }

        public void OnStateExit()
        {

        }
    }
}
