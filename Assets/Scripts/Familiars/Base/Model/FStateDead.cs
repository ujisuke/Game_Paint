namespace Assets.Scripts.Familiars.Base.Model
{
    public class FStateDead : IFState
    {
        private readonly FamiliarModel fM;

        public FStateDead(FamiliarModel familiarModel) => fM = familiarModel;

        public void OnStateEnter()
        {
            fM.Destroy();
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}