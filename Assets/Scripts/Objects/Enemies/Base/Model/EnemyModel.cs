using Cysharp.Threading.Tasks;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;
using System.Threading;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EnemyModel
    {
        private PSA pSA;
        private HP hP;
        private HurtBox hurtBox;
        private readonly EStateMachine eStateMachine;
        private readonly EnemyData enemyData;
        private readonly EnemyController enemyController;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        public PSA PSA => pSA;
        public HurtBox HurtBox => hurtBox;
        public EnemyData EnemyData => enemyData;

        public EnemyModel(EnemyData enemyData, IEStateAfterBorn eStateAfterBorn, Vector2 position, EnemyController enemyController)
        {
            this.enemyData = enemyData;
            pSA = new PSA(position, enemyData.Scale, 0f);
            hP = enemyData.MaxHP;
            hurtBox = new HurtBox(pSA.Pos, enemyData.HurtBoxScale, true);
            eStateMachine = new EStateMachine(this, eStateAfterBorn);
            this.enemyController = enemyController;
            ObjectsStorageModel.Instance.AddEnemy(this);
            cts = new CancellationTokenSource();
            token = cts.Token;
        }


        public void OnUpdate() => eStateMachine.OnUpdate();

        public void Move(Vector2 dir)
        {
            pSA = pSA.Move(dir);
            hurtBox = hurtBox.Move(pSA.Pos);
        }

        public void ChangeState(IEState state) => eStateMachine.ChangeState(state);

        public float GetUP(string key) => enemyData.GetUP(key);

        public async UniTask TakeDamage(int damageValue)
        {
            hP = hP.TakeDamage(damageValue);
            hurtBox = hurtBox.Inactivate();
            await UniTask.Delay(enemyData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.Activate();
        }

        public bool IsDead() => hP.IsDead();

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemoveEnemy(this);
            GameObject.Destroy(enemyController.gameObject);
        }
    }
}
