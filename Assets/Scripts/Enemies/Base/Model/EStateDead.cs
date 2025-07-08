namespace Assets.Scripts.Enemies.Base.Model
{
    public class EStateDead : IEState
    {
        private readonly EnemyModel eM;

        public EStateDead(EnemyModel enemyModel) => eM = enemyModel;

        public void OnStateEnter()
        {
            eM.Destroy();
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}