using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EStateBorn : IEState
    {
        private readonly EnemyModel eM;
        private readonly IEStateAfterBorn eStateAfterBorn;

        public EStateBorn(EnemyModel enemyModel, IEStateAfterBorn eStateAfterBorn)
        {
            eM = enemyModel;
            this.eStateAfterBorn = eStateAfterBorn;
        }

        public void OnStateEnter()
        {
            Debug.Log("EStateBorn");
        }

        public void OnStateFixedUpdate()
        {
            eM.ChangeState(eStateAfterBorn.Initialize(eM));
        }

        public void OnStateExit()
        {

        }
    }
}