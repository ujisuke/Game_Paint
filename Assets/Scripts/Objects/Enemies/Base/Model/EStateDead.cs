namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EStateDead : IEState
    {
        private readonly EnemyModel eM;

        public EStateDead(EnemyModel eM) => this.eM = eM;

        public void OnStateEnter()
        {
            eM.Destroy();
        }

        public void OnUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}