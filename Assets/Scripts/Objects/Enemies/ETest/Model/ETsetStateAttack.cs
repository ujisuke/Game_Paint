using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.ObjectAttacks.Base.Controller;
using Assets.Scripts.Objects.ObjectAttacks.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.ETest.Model
{
    public class ETestStateAttack : IEState
    {
        private readonly EnemyModel eM;
        private ObjectAttackModel attack;
        public ETestStateAttack(EnemyModel enemyModel) => eM = enemyModel;

        public void OnStateEnter()
        {
            Debug.Log("ETestStateAttack");
            var newAttack = GameObject.Instantiate(eM.EnemyData.AttackPrefab, eM.PSA.Pos, Quaternion.identity);
            attack = newAttack.GetComponent<ObjectAttackController>().ObjectAttackModel;
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM));
        }

        public void OnStateExit()
        {
            attack?.Destroy();
        }
    }
}
