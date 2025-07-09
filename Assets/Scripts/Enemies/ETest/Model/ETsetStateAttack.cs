using Assets.Scripts.Enemies.Base.Model;
using Assets.Scripts.ObjectAttacks.Base.Controller;
using Assets.Scripts.ObjectAttacks.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Enemies.ETest.Model
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

        public void OnStateFixedUpdate()
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
