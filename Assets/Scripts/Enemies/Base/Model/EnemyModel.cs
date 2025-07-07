using Assets.Scripts.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.Enemies.Base.Controller;
using Assets.Scripts.GameSystems.Model;
using UnityEngine;

namespace Assets.Scripts.Enemies.Base.Model
{
    public class EnemyModel
    {
        private PSA pSA;
        public PSA PSA => pSA;
        private HP hP;
        private HurtBox hurtBox;
        public HurtBox HurtBox => hurtBox;
        private readonly EStateMachine eStateMachine;
        private readonly EnemyData enemyData;
        private readonly EnemyController enemyController;

        public EnemyModel(EnemyData enemyData, IEStateAfterBorn eStateAfterBorn, Vector2 position, EnemyController enemyController)
        {
            this.enemyData = enemyData;
            pSA = new PSA(position, enemyData.Scale, 0f);
            hP = enemyData.MaxHP;
            hurtBox = enemyData.HurtBox;
            eStateMachine = new EStateMachine(this, eStateAfterBorn);
            this.enemyController = enemyController;
            ObjectStorageModel.Instance.AddEnemy(this);
        }

        public void FixedUpdate() => eStateMachine.FixedUpdate();

        public void Move(Vector2 dir) => pSA = pSA.Move(dir);

        public void ChangeState(IEState state) => eStateMachine.ChangeState(state);

        public float GetUP(string key) => enemyData.GetUP(key);

        public void Destroy() => Object.Destroy(enemyController.gameObject);
    }
}
