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
    public class NinjaStateMakeMud: IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public NinjaStateMakeMud(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            MakeMud().Forget();
        }

        private async UniTask MakeMud()
        {
            for (int i = 0; i < 4; i++)
            {
                await MoveTo(ObjectStorageModel.Instance.GetPlayerPos(eM.Pos));
                List<Vector2> mudPosList = StageData.CalcRandomNearPosList(eM.Pos, 1, 6);
                for(int j = 0; j < mudPosList.Count; j++)
                    await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("MudPurple"), mudPosList[j], Quaternion.identity);
            }

            if (eM.MaxAttackCount <= attackCount)
                eM.ChangeState(new NinjaStateMultiSlash(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfScorpion"))
                eM.ChangeState(new NinjaStateSummonScorpion(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfFrog"))
                eM.ChangeState(new NinjaStateSummonFrog(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new NinjaStateCut(eM, eC, attackCount, summonCount));
        }

        private async UniTask MoveTo(Vector2 pos)
        {
            float moveSpeed = eM.GetUP("MoveSpeed");
            eC.PlayAnim("Walk");
            Vector2 moveVector = pos - eM.Pos;
            Vector2 moveDirY = 0.01f * moveSpeed * new Vector2(0f, moveVector.y).normalized;
            float moveYCount = math.abs(moveVector.y) / moveSpeed * 100;
            for (int j = 0; j < moveYCount; j++)
            {
                eM.MoveIgnoringStage(moveDirY);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: eM.Token);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: eM.Token);
            Vector2 moveDirX = 0.01f * moveSpeed * new Vector2(moveVector.x, 0f).normalized;
            float moveXCount = math.abs(moveVector.x) / moveSpeed * 100;
            for (int j = 0; j < moveXCount; j++)
            {
                eM.MoveIgnoringStage(moveDirX);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: eM.Token);
            }
            eC.PlayAnim("Idle");
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("MoveIntervalSeconds")), cancellationToken: eM.Token);
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
