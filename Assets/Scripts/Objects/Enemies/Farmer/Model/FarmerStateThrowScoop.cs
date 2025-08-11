using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateThrowScoop : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public FarmerStateThrowScoop(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount = 0;
            if(eM.IsLessThanHalfHP())
                summonCount++;
            ThrowScoop().Forget();
        }

        private async UniTask ThrowScoop()
        {
            await Jump();
            eC.FlipX(false);
            eC.PlayAnim("LookDown");
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("ThrowScoopDelayFirstSeconds")), cancellationToken: eM.Token);
            eC.PlayAnim("LookUp");
            float count = eM.GetUP("ThrowScoopCount");
            float throwScoopIntervalSeconds = eM.GetUP("ThrowScoopIntervalSeconds");
            float angle = 360f / count;
            float throwScoopSet = eM.GetUP("ThrowScoopSet");
            float rotateOffset = angle / throwScoopSet;
            for (int i = (int)throwScoopSet; i > 0; i--)
                await ThrowScoopOneSet(count, throwScoopIntervalSeconds, angle, rotateOffset, i);
            eC.FlipX(true);
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("ThrowScoopDelaySecondSeconds")), cancellationToken: eM.Token);
            for (int i = 0; i < throwScoopSet; i++)
                await ThrowScoopOneSet(count, throwScoopIntervalSeconds, angle, rotateOffset, i);
            eC.FlipX(false);
            eC.PlayAnim("LookDown");
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("ThrowScoopDelayThirdSeconds")), cancellationToken: eM.Token);
            eC.PlayAnim("LookUp");
            for (int i = 0; i < throwScoopSet * 2; i++)
                await ThrowScoopOneSet(count * 2, throwScoopIntervalSeconds, angle / 2f, rotateOffset, i);
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("ThrowScoopCoolDownSeconds")), cancellationToken: eM.Token);

            if (StageData.Instance.IsOnEdgeOfStage(eM.PA.Pos))
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new FarmerStateSummon(eM, eC, attackCount, summonCount));
            else if (Vector2.Distance(eM.PA.Pos, ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true)) <= eM.GetUP("NearDistance"))
                if (UnityEngine.Random.Range(0, 2) == 0)
                    eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new FarmerStateSwingScythe(eM, eC, attackCount, summonCount));
            else
                if (UnityEngine.Random.Range(0, 2) == 0)
                    eM.ChangeState(new FarmerStateThrowPlant(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new FarmerStateThrowHoe(eM, eC, attackCount, summonCount));
        }
        
        private async UniTask Jump()
        {
            eC.PlayAnim("JumpBegin");
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            eC.PlayAnim("Jumping");
            Vector2 targetPos = StageData.Instance.StageCenterPos;
            Vector2 moveDir = (targetPos - eM.PA.Pos) / 100f;
            float jumpSecondsDelta = eM.GetUP("JumpSeconds") / 100f;
            for (int i = 0; i < 100; i++)
            {
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(jumpSecondsDelta), cancellationToken: eM.Token);
            }
        }

        private async UniTask ThrowScoopOneSet(float count, float throwScoopIntervalSeconds, float angle, float rotateOffset, int i)
        {
            for (int j = 0; j < count; j++)
            {
                Vector2 offset = Quaternion.Euler(0, 0, j * angle + i * rotateOffset) * Vector2.right;
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Scoop"), eM.PA.Pos + offset, Quaternion.identity);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(throwScoopIntervalSeconds), cancellationToken: eM.Token);
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
        }

        public void OnStateExit()
        {

        }
        
    }
}
