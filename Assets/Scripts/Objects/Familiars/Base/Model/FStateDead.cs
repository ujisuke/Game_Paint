namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public class FStateDead : IFState
    {
        private readonly FamiliarModel fM;

        public FStateDead(FamiliarModel fM) => this.fM = fM;

        public void OnStateEnter()
        {
            fM.Destroy();
        }

        public void OnUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}