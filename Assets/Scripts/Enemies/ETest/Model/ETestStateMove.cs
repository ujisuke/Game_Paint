using Assets.Scripts.Enemies.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Enemies.FTest.Model
{
    public class ETestStateMove : IEStateAfterBorn
    {
        private readonly EnemyModel eM;

        public ETestStateMove(EnemyModel enemyModel) => eM = enemyModel;
        
        public IEState Initialize(EnemyModel enemyModel) => new ETestStateMove(enemyModel);

        public void OnStateEnter()
        {
            Debug.Log("FTestStateMove");
        }

        public void OnStateFixedUpdate()
        {
            if (Input.GetKey(KeyCode.K))
                eM.ChangeState(new EStateDead(eM));
        }

        public void OnStateExit()
        {

        }
    }
}
