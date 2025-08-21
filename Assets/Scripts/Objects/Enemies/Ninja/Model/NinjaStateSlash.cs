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

namespace Assets.Scripts.Objects.Enemies.Ninja.Model
{
    public class NinjaStateSlash : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public NinjaStateSlash(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount++;
            if (eM.IsLatter)
                summonCount++;
            Slash().Forget();
        }

        private async UniTask Slash()
        {
            eM.SetHurtBoxActive(false);
            eC.PlayAnim("Hide");
            float slashCount = eM.GetUP("SlashCount");
            float slashIntervalSeconds = eM.GetUP("SlashIntervalSeconds");
            for (int i = 0; i < slashCount; i++)
            {
                Vector2 playerPosOnPurple = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos);
                Vector2 guidePosOnPurple = new(StageData.Instance.StageCenterPos.x, playerPosOnPurple.y);
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), guidePosOnPurple, Quaternion.identity);
                await UniTask.Delay(TimeSpan.FromSeconds(slashIntervalSeconds), cancellationToken: eM.Token);
                Vector2 slashPosOnPurple = new(-1f, playerPosOnPurple.y);
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), slashPosOnPurple, Quaternion.identity);
            }

            Vector2 playerPosOnGreen = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) + new Vector2(UnityEngine.Random.Range(-1, 2), 0f);
            Vector2 guidePosOnGreen = new((int)playerPosOnGreen.x, StageData.Instance.StageCenterPos.y);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuideGreen"), guidePosOnGreen, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(slashIntervalSeconds), cancellationToken: eM.Token);
            for (int i = (int)StageData.StageEdgePosMin.x; i <= StageData.Instance.StageEdgePosMax.x; i++)
            {
                if (math.abs(i - (int)playerPosOnGreen.x) <= 1)
                    continue;
                Vector2 slashPosOnGreen = new(i, StageData.Instance.StageEdgePosMax.y);
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGreen"), slashPosOnGreen, Quaternion.identity);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(slashIntervalSeconds), cancellationToken: eM.Token);


            eM.SetHurtBoxActive(true);
            eC.PlayAnim("Appear");

            if (eM.MaxAttackCount <= attackCount)
                eM.ChangeState(new NinjaStateMultiSlash(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfScorpion"))
                eM.ChangeState(new NinjaStateSummonScorpion(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfFrog"))
                eM.ChangeState(new NinjaStateSummonFrog(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new NinjaStateCut(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new NinjaStateDown(eM, eC, attackCount, summonCount));
        }

        public void OnStateExit()
        {

        }
    }
}
