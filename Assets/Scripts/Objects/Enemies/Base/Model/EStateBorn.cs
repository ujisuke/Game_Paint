using Assets.Scripts.Objects.Enemies.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EStateBorn : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private readonly IEStateAfterBorn eStateAfterBorn;

        public EStateBorn(EnemyModel eM, EnemyController eC, IEStateAfterBorn eStateAfterBorn)
        {
            this.eM = eM;
            this.eC = eC;
            this.eStateAfterBorn = eStateAfterBorn;
        }

        public void OnStateEnter()
        {
            Debug.Log("EStateBorn");
            eC.PlayAnim("Idle");
        }

        public void OnUpdate()
        {
            eM.ChangeState(eStateAfterBorn.Initialize(eM, eC));
        }

        public void OnStateExit()
        {

        }
    }
}