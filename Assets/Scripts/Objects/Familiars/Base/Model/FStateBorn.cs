using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public class FStateBorn : IFState
    {
        private readonly FamiliarModel fM;
        private readonly IFStateAfterBorn fStateAfterBorn;

        public FStateBorn(FamiliarModel familiarModel, IFStateAfterBorn fStateAfterBorn)
        {
            fM = familiarModel;
            this.fStateAfterBorn = fStateAfterBorn;
        }

        public void OnStateEnter()
        {
            Debug.Log("FStateBorn");
        }

        public void OnStateFixedUpdate()
        {
            fM.ChangeState(fStateAfterBorn.Initialize(fM));
        }

        public void OnStateExit()
        {

        }
    }
}