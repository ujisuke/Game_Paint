using Cysharp.Threading.Tasks;
using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using UnityEngine;
using System.Threading;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EnemyModel
    {
        private PA pA;
        private HP hp;
        private HurtBox hurtBox;
        private readonly EStateMachine eStateMachine;
        private readonly EnemyData enemyData;
        private readonly EnemyController enemyController;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        public PA PA => pA;
        public HurtBox HurtBox => hurtBox;
        public EnemyData EnemyData => enemyData;
        public CancellationToken Token => token;

        public EnemyModel(EnemyData enemyData, IEStateAfterBorn eStateAfterBorn, Vector2 pos, EnemyController enemyController, ColorEffectData colorEffectData)
        {
            this.enemyData = enemyData;
            pA = new PA(pos, 0f);
            hp = new HP(enemyData.MaxHP);
            hurtBox = new HurtBox(pA.Pos, enemyData.HurtBoxScale, true);
            eStateMachine = new EStateMachine(this, eStateAfterBorn, enemyController);
            this.enemyController = enemyController;
            ObjectsStorageModel.Instance.AddEnemy(this);
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        public void OnUpdate()
        {
            eStateMachine.OnUpdate();
        }
        
        public void Move(Vector2 dir)
        {
            pA = pA.Move(dir);
            hurtBox = hurtBox.Move(dir);
        }

        public void ChangeState(IEState state) => eStateMachine.ChangeState(state);

        public float GetUP(string key) => enemyData.GetUP(key);

        public async UniTask TakeDamage(float damageValue)
        {
            hp = hp.TakeDamage(damageValue);
            hurtBox = hurtBox.SetActive(false);
            await UniTask.Delay(enemyData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.SetActive(true);
        }

        public bool IsDead() => hp.IsDead();

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemoveEnemy();
            GameObject.Destroy(enemyController.gameObject);
            enemyController.OnDestroy();
        }
    }
}
