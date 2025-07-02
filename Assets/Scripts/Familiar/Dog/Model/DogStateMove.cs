using Assets.Scripts.Familiar.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Familiar.Dog.Model
{
    public class DogStateMove : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;

        public DogStateMove(FamiliarModel familiarModel)
        {
            fM = familiarModel;
        }

        public IFState Initialize(FamiliarModel familiarModel)
        {
            return new DogStateMove(fM);
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
