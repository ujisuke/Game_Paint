using Cysharp.Threading.Tasks;
using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using UnityEngine;
using System.Threading;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EnemyModel
    {
        private PA pA;
        private HP hP;
        private HurtBox hurtBox;
        private readonly EStateMachine eStateMachine;
        private readonly EnemyData enemyData;
        private readonly EnemyController enemyController;
        private CancellationTokenSource cts;
        private CancellationToken token;
        private bool isLatter;
        public PA PA => pA;
        public HurtBox HurtBox => hurtBox;
        public EnemyData EnemyData => enemyData;
        public CancellationToken Token => token;
        public float HPRatio => hP.Ratio;
        public bool DoesGetHPHalf => !isLatter && hP.IsLessThanHalf();
        public bool IsLatter => isLatter;

        public EnemyModel(EnemyData enemyData, IEStateAfterBorn eStateAfterBorn, Vector2 pos, EnemyController enemyController)
        {
            this.enemyData = enemyData;
            pA = new PA(pos, 0f);
            hP = new HP(enemyData.MaxHP);
            hurtBox = new HurtBox(pA.Pos, enemyData.HurtBoxScale, true);
            eStateMachine = new EStateMachine(this, eStateAfterBorn, enemyController);
            this.enemyController = enemyController;
            ObjectStorageModel.Instance.AddEnemy(this);
            cts = new CancellationTokenSource();
            token = cts.Token;
            isLatter = false;
        }

        public void OnUpdate()
        {
            eStateMachine.OnUpdate();
        }

        public void MoveIgnoringStage(Vector2 dir)
        {
            pA = pA.MoveIgnoringStage(dir);
            hurtBox = hurtBox.SetPos(pA.Pos);
        }

        public void MoveInStage(Vector2 dir)
        {
            pA = pA.MoveInStage(dir);
            hurtBox = hurtBox.SetPos(pA.Pos);
        }

        public async UniTask OnDown()
        {
            cts?.Cancel();
            cts?.Dispose();
            await UniTask.DelayFrame(1);
            cts = new CancellationTokenSource();
            token = cts.Token;
            Invinciblize().Forget();
            isLatter = hP.IsLessThanHalf();
        }

        public void ChangeState(IEState state) => eStateMachine.ChangeState(state);

        public float GetUP(string key) => enemyData.GetUP(key);

        public void TakeDamage(float damageValue)
        {
            hP = hP.TakeDamage(damageValue);
            enemyController.OnTakeDamage();
            Invinciblize().Forget();
        }

        private async UniTask Invinciblize()
        {
            hurtBox = hurtBox.SetActive(false);
            await UniTask.Delay(enemyData.InvincibleSecond, cancellationToken: token);
            hurtBox = hurtBox.SetActive(true);
        }

        public void SetHurtBoxActive(bool isActive) => hurtBox = hurtBox.SetActive(isActive);

        public bool IsDead() => hP.IsDead();

        public void Destroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            ObjectStorageModel.Instance.RemoveEnemy();
            GameObject.Destroy(enemyController.gameObject);
            enemyController.OnDestroy();
        }
    }
}
