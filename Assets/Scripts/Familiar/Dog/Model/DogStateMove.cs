using Assets.Scripts.Familiar.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Familiar.Dog.Model
{
    public class DogStateMove : IFStateAfterBorn
    {
        private readonly FamiliarModel familiarModel;
        private readonly FStateMachine fStateMachine;

        public DogStateMove(FStateMachine fStateMachine, FamiliarModel familiarModel)
        {
            this.fStateMachine = fStateMachine;
            this.familiarModel = familiarModel;
        }

        public IFState Initialize(FStateMachine fStateMachine, FamiliarModel familiarModel)
        {
            return new DogStateMove(fStateMachine, familiarModel);
        }

        public void OnStateEnter()
        {
            Debug.Log("Dog is moving");
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
