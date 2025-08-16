using System;
using System.Collections.Generic;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Angler.Model
{
    public class AnglerStateBigCatch : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public AnglerStateBigCatch(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount = 0;
            if(eM.IsLatter)
                summonCount++;
            Fishing().Forget();
        }

        private async UniTask Fishing()
        {
            Vector2 moveVector;
            if (eM.Pos.x < StageData.Instance.StageCenterPos.x)
                moveVector = Vector2.left;
            else
                moveVector = Vector2.right;
            eC.FlipX(moveVector.x > 0);
            await MoveTurn(moveVector);
            await MoveStraightBeforeFishing(-moveVector);
            await Instantiate();

            if (summonCount == (int)eM.GetUP("SummonCountOfFish"))
                eM.ChangeState(new AnglerStateSummonFish(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfSquid"))
                eM.ChangeState(new AnglerStateSummonSquid(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new AnglerStateSetShark(eM, eC, attackCount, summonCount));
        }

        private async UniTask MoveStraightBeforeFishing(Vector2 moveVector)
        {
            float moveSecondsDelta = eM.GetUP("MoveStraightSeconds") * 0.01f;
            Vector2 moveDir = new(0.01f * (StageData.Instance.Width - 6f) * moveVector.x, 0f);
            for (int i = 0; i < 50; i++)
            {
                eM.MoveIgnoringStage(moveDir * (1f - i * 0.02f));
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eM.Token);
            }
        }

        private async UniTask MoveTurn(Vector2 moveVector)
        {
            Vector2 targetPos = StageData.Instance.StageCenterPos;
            float moveSecondsDelta = eM.GetUP("MoveStraightSeconds") * 0.005f;
            float moveDirY = targetPos.y - eM.Pos.y;
            for (int i = 0; i < 100; i++)
            {
                Vector2 moveDir = new(moveVector.x * math.cos(i * 0.01f * math.PI) * 0.08f, moveDirY * 0.01f);
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eM.Token);
            }
        }

        private async UniTask Instantiate()
        {
            float width = StageData.Instance.Width;
            float bigCatchSeconds = eM.GetUP("BigCatchSeconds");
            Vector2 leftPos, rightPos;
            if (eM.Pos.x < StageData.Instance.StageCenterPos.x)
            {
                leftPos = StageData.StageEdgePosMin + Vector2.up;
                rightPos = new(StageData.Instance.StageEdgePosMax.x, StageData.Instance.StageCenterPos.y);
            }
            else
            {
                leftPos = new(StageData.StageEdgePosMin.x, StageData.Instance.StageCenterPos.y);
                rightPos = new(StageData.Instance.StageEdgePosMax.x, StageData.StageEdgePosMin.y + 1f);
            }
            for (int i = 0; i < width; i++)
            {
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("BigCatchFish"), leftPos - new Vector2(i, 0), Quaternion.identity);
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("BigCatchFish"), rightPos + new Vector2(i, 0), Quaternion.identity);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(bigCatchSeconds - 0.1f * width - 2f), cancellationToken: eM.Token);
            Vector2 dir = (ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) - eM.Pos).normalized * 0.5f;
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SpecialFish"), eM.Pos + dir, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: eM.Token);
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
