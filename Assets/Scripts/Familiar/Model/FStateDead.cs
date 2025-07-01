using UnityEngine;

namespace Assets.Scripts.Familiar.Model
{
    public class FStateDead : IFState
    {
        private readonly FStateMachine fStateMachine;
        private readonly FamiliarModel familiarModel;

        public FStateDead(FStateMachine fStateMachine, FamiliarModel familiarModel)
        {
            this.fStateMachine = fStateMachine;
            this.familiarModel = familiarModel;
        }

        public void OnStateEnter()
        {
            Debug.Log("Familiar Dead");
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}