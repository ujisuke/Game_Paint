using Assets.Scripts.Objects.Enemies.Base.Controller;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EStateDead : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;

        public EStateDead(EnemyModel eM, EnemyController eC)
        {
            this.eM = eM;
            this.eC = eC;
        }

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