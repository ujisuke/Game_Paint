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
    public class NinjaStateMultiSlash : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public NinjaStateMultiSlash(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            Slash().Forget();
        }

        private async UniTask Slash()
        {
            eM.SetHurtBoxActive(false);
            eC.PlayAnim("Hide");
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f), cancellationToken: eM.Token);
            Vector2 minPos = StageData.StageEdgePosMin;
            Vector2 centerPos = StageData.Instance.StageCenterPos;
            eM.MoveIgnoringStage(new Vector2(minPos.x - 2f, centerPos.y) - eM.Pos);
            eM.SetHurtBoxActive(true);
            eC.PlayAnim("Appear");
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: eM.Token);
            eC.PlayAnim("SlashBefore");
            float slashIntervalSeconds = eM.GetUP("SlashIntervalSeconds");
            float multiSlashWaitSeconds = eM.GetUP("MultiSlashWaitSeconds");
            Vector2 maxPos = StageData.Instance.StageEdgePosMax;
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, minPos.y + 1.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, maxPos.y - 1.5f), Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(slashIntervalSeconds), cancellationToken: eM.Token);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(minPos.x, minPos.y + 1.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(minPos.x, maxPos.y - 1.5f), Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(multiSlashWaitSeconds), cancellationToken: eM.Token);

            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, centerPos.y + 0.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, centerPos.y - 0.5f), Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(slashIntervalSeconds), cancellationToken: eM.Token);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(maxPos.x, centerPos.y + 0.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(maxPos.x, centerPos.y - 0.5f), Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(multiSlashWaitSeconds), cancellationToken: eM.Token);

            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, minPos.y + 1.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, minPos.y + 2.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, maxPos.y - 1.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuidePurple"), new(centerPos.x, maxPos.y - 2.5f), Quaternion.identity);
            Vector2 playerPosOnGreen = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) + new Vector2(UnityEngine.Random.Range(-1, 2), 0f);
            Vector2 guidePosOnGreen = new((int)playerPosOnGreen.x, StageData.Instance.StageCenterPos.y);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGuideGreen"), guidePosOnGreen, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(slashIntervalSeconds), cancellationToken: eM.Token);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(minPos.x, minPos.y + 1.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(minPos.x, minPos.y + 2.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(minPos.x, maxPos.y - 1.5f), Quaternion.identity);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashPurple"), new(minPos.x, maxPos.y - 2.5f), Quaternion.identity);
            for (int i = (int)StageData.StageEdgePosMin.x; i <= StageData.Instance.StageEdgePosMax.x; i++)
            {
                if (math.abs(i - (int)playerPosOnGreen.x) <= 1)
                    continue;
                Vector2 slashPosOnGreen = new(i, StageData.Instance.StageEdgePosMax.y);
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("SlashGreen"), slashPosOnGreen, Quaternion.identity);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(multiSlashWaitSeconds), cancellationToken: eM.Token);

            eC.PlayAnim("SlashAfter");
            Vector2 moveVector = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) - eM.Pos;
            await Slash(ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) + moveVector.normalized * 5f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: eM.Token);

            if (eM.MaxAttackCount <= attackCount)
                eM.ChangeState(new NinjaStateMultiSlash(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfScorpion"))
                eM.ChangeState(new NinjaStateSummonScorpion(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfFrog"))
                eM.ChangeState(new NinjaStateSummonFrog(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new NinjaStateCut(eM, eC, attackCount, summonCount));
        }

        private async UniTask Slash(Vector2 pos)
        {
            GameObject[] slash = await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Slash"), eM.Pos, Quaternion.identity);
            float moveSpeed = eM.GetUP("SlashSpeed");
            Vector2 moveVector = pos - eM.Pos;
            Vector2 moveDir = 0.01f * moveSpeed * moveVector.normalized;
            float moveCount = moveVector.magnitude / moveSpeed * 100;
            for (int j = 0; j < moveCount; j++)
            {
                eM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: eM.Token);
                if (slash[0] == null)
                    eM.ChangeState(new NinjaStateDown(eM, eC, attackCount, summonCount));
            }
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
