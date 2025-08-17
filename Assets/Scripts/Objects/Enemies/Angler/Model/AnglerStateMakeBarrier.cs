using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Assets.Scripts.Datas;
using Unity.Mathematics;

namespace Assets.Scripts.Objects.Enemies.Angler.Model
{
    public class AnglerStateMakeBarrier : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public AnglerStateMakeBarrier(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            MakeBarrier().Forget();
        }

        private async UniTask MakeBarrier()
        {
            await Jump();

            eC.PlayAnim("ShakeHands");
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("MakeBarrierDelaySeconds")), cancellationToken: eM.Token);

            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("BarrierFish"), eM.Pos + Vector2.up, Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("BarrierFish"), eM.Pos + Vector2.right, Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("BarrierFish"), eM.Pos + Vector2.down, Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("BarrierFish"), eM.Pos + Vector2.left, Quaternion.identity);

            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("MakeBarrierSeconds")), cancellationToken: eM.Token);
            Vector2 moveVector;
            if(UnityEngine.Random.Range(0, 2) == 0)
                moveVector = Vector2.left;
            else
                moveVector = Vector2.right;
            eC.FlipX(moveVector.x < 0);
            await MoveStraightFromCenter(moveVector);
            eC.FlipX(moveVector.x > 0);
            await MoveTurn(moveVector);
            await MoveStraight(-moveVector);

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new AnglerStateBigCatch(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfFish"))
                eM.ChangeState(new AnglerStateSummonFish(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfSquid"))
                eM.ChangeState(new AnglerStateSummonSquid(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new AnglerStateFishing(eM, eC, attackCount, summonCount));
        }

        private async UniTask Jump()
        {
            eC.PlayAnim("Hide");
            eM.SetHurtBoxActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: eM.Token);
            eC.PlayAnim("Appear");
            eM.SetHurtBoxActive(true);
            eM.MoveIgnoringStage(StageData.Instance.StageCenterPos - eM.Pos + Vector2.down * 2f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: eM.Token);            
        }

        private async UniTask MoveStraightFromCenter(Vector2 moveVector)
        {
            eC.PlayAnim("Walk");
            float moveSecondsDelta = eM.GetUP("MoveStraightSeconds") * 0.005f;
            Vector2 moveDir = 0.01f * (StageData.Instance.Width * 0.5f - 3f) * moveVector;
            for (int i = 0; i < 100; i++)
            {
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eM.Token);
            }
        }

        private async UniTask MoveStraight(Vector2 moveVector)
        {
            float moveSecondsDelta = eM.GetUP("MoveStraightSeconds") * 0.01f;
            Vector2 moveDir = 0.01f * (StageData.Instance.Width - 6f) * moveVector;
            for (int i = 0; i < 100; i++)
            {
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eM.Token);
            }
        }

        private async UniTask MoveTurn(Vector2 moveVector)
        {
            float moveSecondsDelta = eM.GetUP("MoveStraightSeconds") * 0.005f;
            float heightHalf = 0.5f * (StageData.Instance.Height - 4f);
            for (int i = 0; i < 100; i++)
            {
                Vector2 moveDir = new(moveVector.x * math.cos(i * 0.01f * math.PI) * 0.08f, heightHalf * 0.01f);
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
