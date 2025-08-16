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
    public class AnglerStateFishing : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public AnglerStateFishing(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount++;
            if(eM.IsLatter)
                summonCount++;
            Fishing().Forget();
        }

        private async UniTask Fishing()
        {
            Vector2 moveVector;
            if (eM.PA.Pos.x < StageData.Instance.StageCenterPos.x)
                moveVector = Vector2.left;
            else
                moveVector = Vector2.right;
            eC.FlipX(moveVector.x > 0);
            await MoveTurn(moveVector);
            await MoveStraightBeforeFishing(-moveVector);
            await Instantiate();
            await MoveStraightAfterFishing(-moveVector);
            
            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new AnglerStateBigCatch(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfFish"))
                eM.ChangeState(new AnglerStateSummonFish(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfSquid"))
                eM.ChangeState(new AnglerStateSummonSquid(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new AnglerStateFishing(eM, eC, attackCount, summonCount));
        }

        private async UniTask MoveTurn(Vector2 moveVector)
        {
            Vector2 targetPos = StageData.Instance.CalcRandomPosInStage();
            float moveSecondsDelta = eM.GetUP("MoveStraightSeconds") * 0.005f;
            float moveDirY = targetPos.y - eM.PA.Pos.y;
            for (int i = 0; i < 100; i++)
            {
                Vector2 moveDir = new(moveVector.x * math.cos(i * 0.01f * math.PI) * 0.08f, moveDirY * 0.01f);
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eM.Token);
            }
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

        private async UniTask Instantiate()
        {
            Vector2 dir = (ObjectStorageModel.Instance.GetHostilePos(eM.PA.Pos, true) - eM.PA.Pos).normalized * 0.5f;
            int attackFishCount = (int)eM.GetUP("AttackFishCount");
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("FishingSeconds") - 0.1f * attackFishCount), cancellationToken: eM.Token);
            float angle = 360f / attackFishCount * 3f;
            for (int i = 0; i < attackFishCount; i++)
            {
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("AttackFish"), eM.PA.Pos + (Vector2)(Quaternion.Euler(0, 0, angle * i) * dir), Quaternion.identity);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            }
        }

        private async UniTask MoveStraightAfterFishing(Vector2 moveVector)
        {
            float moveSecondsDelta = eM.GetUP("MoveStraightSeconds") * 0.01f;
            Vector2 moveDir = new(0.01f * (StageData.Instance.Width - 6f) * moveVector.x, 0f);
            for (int i = 0; i < 75; i++)
            {
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eM.Token);
            }
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new AnglerStateDown(eM, eC, attackCount, summonCount));
        }

        public void OnStateExit()
        {

        }
    }
}
