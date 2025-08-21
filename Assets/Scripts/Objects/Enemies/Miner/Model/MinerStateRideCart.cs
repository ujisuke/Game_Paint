using System;
using System.Collections.Generic;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Controller;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Miner.Model
{
    public class MinerStateRideCart : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public MinerStateRideCart(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount = 0;
            if (eM.IsLatter)
                summonCount++;
            RideCart().Forget();
        }

        private async UniTask RideCart()
        {
            eC.PlayAnim("HideBegin");
            float rideCartHideSeconds = eM.GetUP("RideCartHideSeconds");
            eM.SetHurtBoxActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(rideCartHideSeconds), cancellationToken: eM.Token);
            eM.MoveIgnoringStage(new Vector2(-10f, StageData.Instance.StageCenterPos.y) - eM.Pos);

            float moveDistance = (StageData.Instance.StageCenterPos.x - eM.Pos.x) * 2f;
            Vector2 moveDir = new(moveDistance * 0.01f, 0f);
            float moveSecondsDelta = eM.GetUP("RideCartAttackSeconds") * 0.01f;
            eC.PlayAnim("Whistle");
            eM.SetHurtBoxActive(true);
            float intervalSeconds = eM.GetUP("RideCartIntervalSeconds");
            GameObject bigCartPrefab = eM.EnemyData.GetAttackPrefab("BigCart");
            await GameObject.InstantiateAsync(bigCartPrefab, eM.Pos + Vector2.up * 2f, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(intervalSeconds), cancellationToken: eM.Token);
            await GameObject.InstantiateAsync(bigCartPrefab, eM.Pos + Vector2.down * 2f, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(intervalSeconds), cancellationToken: eM.Token);

            GameObject[] bigCart = await GameObject.InstantiateAsync(bigCartPrefab, eM.Pos, Quaternion.identity);
            eM.MoveIgnoringStage(new Vector2(-2f, 1f));
            for (int i = 0; i < 100; i++)
            {
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eM.Token);
                if (bigCart[0] != null || i > 90)
                    eM.ChangeState(new MinerStateDown(eM, eC, attackCount, summonCount));
            }

            eC.PlayAnim("HideEnd");
            Vector2 playerPos = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos);
            float targetPosY = playerPos.y > StageData.Instance.StageCenterPos.y ? 2f : StageData.Instance.StageEdgePosMax.y - 2f;
            Vector2 targetPos = new(playerPos.x, targetPosY);
            eM.MoveIgnoringStage(targetPos - eM.Pos);
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("RideCartCoolDownSeconds")), cancellationToken: eM.Token);

            if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new MinerStateSummon(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new MinerStateMakeWall(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new MinerStateDown(eM, eC, attackCount, summonCount));
            eC.FlipX(ObjectStorageModel.Instance.GetPlayerPos(eM.Pos).x < eM.Pos.x);
        }

        public void OnStateExit()
        {

        }
    }
}
