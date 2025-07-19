using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EStateBorn : IEState
    {
        private readonly EnemyModel eM;
        private readonly IEStateAfterBorn eStateAfterBorn;

        public EStateBorn(EnemyModel eM, IEStateAfterBorn eStateAfterBorn)
        {
            this.eM = eM;
            this.eStateAfterBorn = eStateAfterBorn;
        }

        public void OnStateEnter()
        {
            Debug.Log("EStateBorn");
        }

        public void OnUpdate()
        {
            eM.ChangeState(eStateAfterBorn.Initialize(eM));
        }

        public void OnStateExit()
        {

        }
    }
}