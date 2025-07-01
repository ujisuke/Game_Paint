using UnityEngine;

namespace Assets.Scripts.Familiar.Model
{
    public class FStateBorn : IFState
    {
        private readonly FStateMachine fStateMachine;
        private readonly FamiliarModel familiarModel;

        public FStateBorn(FStateMachine fStateMachine, FamiliarModel familiarModel)
        {
            this.fStateMachine = fStateMachine;
            this.familiarModel = familiarModel;
        }

        public void OnStateEnter()
        {
            Debug.Log("Familiar Born");
            fStateMachine.ChangeState(new FStateDead(fStateMachine, familiarModel));
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}