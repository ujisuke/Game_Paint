using Assets.Scripts.Familiar.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Familiar.FTest.Model
{
    public class FTestStateMove : IFStateAfterBorn
    {
        private readonly FamiliarModel familiarModel;
        private readonly FStateMachine fStateMachine;

        public FTestStateMove(FStateMachine fStateMachine, FamiliarModel familiarModel)
        {
            this.fStateMachine = fStateMachine;
            this.familiarModel = familiarModel;
        }

        public IFState Initialize(FStateMachine fStateMachine, FamiliarModel familiarModel)
        {
            return new FTestStateMove(fStateMachine, familiarModel);
        }

        public void OnStateEnter()
        {
            Debug.Log("FTestStateMove");
        }

        public void OnStateFixedUpdate()
        {
            fStateMachine.ChangeState(new FStateDead(fStateMachine, familiarModel));
        }

        public void OnStateExit()
        {

        }
    }
}
