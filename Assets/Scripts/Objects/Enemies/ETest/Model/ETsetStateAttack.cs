using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.ETest.Model
{
    public class ETestStateAttack : IEState
    {
        private readonly EnemyModel eM;
        private EnemyAttackModel eAM;
        private EnemyController eC;
        public ETestStateAttack(EnemyModel eM, EnemyController eC)
        {
            this.eM = eM;
            this.eC = eC;
        }

        public void OnStateEnter()
        {
            var newAttack = GameObject.Instantiate(eM.EnemyData.AttackPrefab, eM.PA.Pos, Quaternion.identity);
            newAttack.GetComponent<EnemyAttackController>().Initialize(eM.IsAttackSpeedDecreased);
            eAM = newAttack.GetComponent<EnemyAttackController>().EnemyAttackModel;
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
        }

        public void OnStateExit()
        {
            eAM.Destroy();
        }
    }
}
