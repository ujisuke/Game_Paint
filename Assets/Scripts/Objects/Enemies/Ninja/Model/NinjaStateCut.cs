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
    public class NinjaStateCut : IEStateAfterBorn
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private readonly int attackCount;
        private readonly int summonCount;

        public NinjaStateCut(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public NinjaStateCut(EnemyController enemyController)
        {
            eM = null;
            eC = enemyController;
            attackCount = 0;
            summonCount = 0;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new NinjaStateCut(enemyModel, enemyController, 0, 0);

        public void OnStateEnter()
        {
           Cut().Forget();
        }

        private async UniTask Cut()
        {
            float cutCount = eM.GetUP("CutCount");
            float cutDelaySeconds = eM.GetUP("CutDelaySeconds");
            float cutSecondsPerSet = eM.GetUP("CutSeconds") / cutCount - cutDelaySeconds;
            int r = UnityEngine.Random.Range(0, 3);
            for (int j = 0; j < cutCount; j++)
            {
                for (int i = 0; i < 1; i++)
                    await MoveTo(CalcRandomPosNearPlayer());
                
                await InstantiateCut(j, r);
                eC.PlayAnim("CutBefore");
                await UniTask.Delay(TimeSpan.FromSeconds(cutDelaySeconds), cancellationToken: eM.Token);
                eC.PlayAnim("CutAfter");
                await UniTask.Delay(TimeSpan.FromSeconds(cutSecondsPerSet), cancellationToken: eM.Token);
            }

            if (eM.MaxAttackCount <= attackCount)
                eM.ChangeState(new NinjaStateMultiSlash(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfScorpion"))
                eM.ChangeState(new NinjaStateSummonScorpion(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfFrog"))
                eM.ChangeState(new NinjaStateSummonFrog(eM, eC, attackCount, summonCount));
            else
            {
                if (UnityEngine.Random.Range(0, 3) < 2)
                    eM.ChangeState(new NinjaStateSlash(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new NinjaStateMakeMud(eM, eC, attackCount, summonCount));
            }
        }

        private async UniTask InstantiateCut(int j, int r)
        {
            Vector2 cutVector = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) - eM.Pos;

            Vector2 cutDir;
            if (cutVector.x > 0)
                cutDir.x = 2f;
            else
                cutDir.x = -2f;
            if (cutVector.y > 0)
                cutDir.y = 2f;
            else
                cutDir.y = -2f;

            if (j == r)
            {
                cutDir *= 1.5f;
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("CutGreenNotice"), cutDir + eM.Pos, Quaternion.identity);
                for (int i = 1; i < 4; i++)
                {
                    Vector2 tmpCutPos = (Vector2)(Quaternion.Euler(0f, 0f, i * 90f) * cutDir) + eM.Pos;
                    await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("CutGreen"), tmpCutPos, Quaternion.identity);
                }
            }
            else
                await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("CutPurple"), cutDir + eM.Pos, Quaternion.identity);
        }

        private Vector2 CalcRandomPosNearPlayer() => ObjectStorageModel.Instance.GetPlayerPos(eM.Pos) + UnityEngine.Random.insideUnitCircle.normalized * 2f;
        

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
