using UnityEngine;

namespace Assets.Scripts.Familiar.Base.Model
{
    public class FStateBorn : IFState
    {
        private readonly FStateMachine fStateMachine;
        private readonly FamiliarModel familiarModel;
        private readonly IFStateAfterBorn fStateAfterBorn;

        public FStateBorn(FStateMachine fStateMachine, FamiliarModel familiarModel, IFStateAfterBorn fStateAfterBorn)
        {
            this.fStateMachine = fStateMachine;
            this.familiarModel = familiarModel;
            this.fStateAfterBorn = fStateAfterBorn;
        }

        public void OnStateEnter()
        {
            Debug.Log("FStateBorn");
        }

        public void OnStateFixedUpdate()
        {
            fStateMachine.ChangeState(fStateAfterBorn.Initialize(fStateMachine, familiarModel));
        }

        public void OnStateExit()
        {

        }
    }
}