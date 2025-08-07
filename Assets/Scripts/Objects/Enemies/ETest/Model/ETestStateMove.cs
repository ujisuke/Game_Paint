using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.ETest.Model
{
    public class ETestStateMove : IEStateAfterBorn
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;

        public ETestStateMove(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new ETestStateMove(enemyModel, enemyController);

        public void OnStateEnter()
        {

        }

        public void OnUpdate()
        {
            eM.ChangeState(new ETestStateAttack(eM, eC));
        }

        public void OnStateExit()
        {

        }
    }
}
