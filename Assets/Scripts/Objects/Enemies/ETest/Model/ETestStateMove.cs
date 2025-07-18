using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.Enemies.ETest.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.FTest.Model
{
    public class ETestStateMove : IEStateAfterBorn
    {
        private readonly EnemyModel eM;

        public ETestStateMove(EnemyModel enemyModel) => eM = enemyModel;
        
        public IEState Initialize(EnemyModel enemyModel) => new ETestStateMove(enemyModel);

        public void OnStateEnter()
        {
            Debug.Log("ETestStateMove");
        }

        public void OnStateFixedUpdate()
        {
            eM.ChangeState(new ETestStateAttack(eM));
        }

        public void OnStateExit()
        {

        }
    }
}
