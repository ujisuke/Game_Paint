using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Miner.Model
{
    public class MinerStateMakeWall : IEStateAfterBorn
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public MinerStateMakeWall(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public MinerStateMakeWall(EnemyController enemyController)
        {
            eM = null;
            eC = enemyController;
            attackCount = 0;
            summonCount = 0;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new MinerStateMakeWall(enemyModel, enemyController, 0, 0);

        public void OnStateEnter()
        {
            attackCount++;
            if (eM.IsLatter)
                summonCount++;
            MakeWall().Forget();
        }

        private async UniTask MakeWall()
        {
            eC.PlayAnim("MakeWall");
            float makeWallSeconds = eM.GetUP("MakeWallSeconds");
            MakeRocks();

            eM.SetHurtBoxActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(makeWallSeconds), cancellationToken: eM.Token);
            eM.SetHurtBoxActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("MakeWallCoolDownSeconds")), cancellationToken: eM.Token);

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new MinerStateRideCart(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new MinerStateSummon(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new MinerStateJam(eM, eC, attackCount, summonCount));
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

        private void MakeRocks()
        {
            Vector2 stageSize = StageData.Instance.StageEdgePosMax - StageData.StageEdgePosMin;
            for (int x = (int)StageData.StageEdgePosMin.x; x < stageSize.x; x++)
            {
                for (int y = (int)StageData.StageEdgePosMin.y; y < stageSize.y; y++)
                {
                    float fixedX = x + 0.5f;
                    float fixedY = y + 0.5f;
                    if (IsSkipedPos(fixedX, fixedY, stageSize))
                        continue;
                    GameObject.Instantiate(eM.EnemyData.GetAttackPrefab("Rock"), new Vector2(fixedX, fixedY), Quaternion.identity);
                }
            }
        }

        private bool IsSkipedPos(float fixedX, float fixedY, Vector2 stageSize)
        {
            if (eM.IsLatter)
            {
                float fx = FuncHard(fixedX, stageSize);
                return fx - 2f <= fixedY && fx + 2f >= fixedY;
            }
            else
            {
                (float fxA, float fxB) = FuncNormal(stageSize);
                return (fxA - 1f <= fixedY && fxA + 1f >= fixedY) || (fxB - 1f <= fixedY && fxB + 1f >= fixedY);
            }
        }

        private static (float, float) FuncNormal(Vector2 stageSize)
        {
            return (stageSize.y - 2f, 2f);
        }

        private static float FuncHard(float x, Vector2 stageSize)
        {
            float a = stageSize.y / stageSize.x * 2;
            return math.abs(a * (x - stageSize.x * 0.5f));
        }
    }
}
