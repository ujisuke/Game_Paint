using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateSummon : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public FarmerStateSummon(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount++;
            summonCount = 0;
            Summon().Forget();
        }

        private async UniTask Summon()
        {
            float summonSeconds = eM.GetUP("SummonSeconds");
            eC.PlayAnim("Summon", summonSeconds * 0.5f);
            float birdCount = eM.GetUP("BirdCount");
            for (int i = 0; i < birdCount; i++)
            {
                await SummonDataList.Instance.SummonByEnemy("Bird", eM.PA.Pos, eM.Token);
                await UniTask.Delay(TimeSpan.FromSeconds(summonSeconds / birdCount), cancellationToken: eM.Token);
            }
            if (attackCount >= eM.GetUP("AttackCountMax"))
                    eM.ChangeState(new FarmerStateThrowScoop(eM, eC, attackCount, summonCount));
                else if (StageData.Instance.IsOnEdgeOfStage(eM.PA.Pos))
                    eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
                else if (Vector2.Distance(eM.PA.Pos, ObjectStorageModel.Instance.GetHostilePos(eM.PA.Pos, true)) <= eM.GetUP("NearDistance"))
                    if (UnityEngine.Random.Range(0, 2) == 0)
                        eM.ChangeState(new FarmerStateSwingScythe(eM, eC, attackCount, summonCount));
                    else
                        eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new FarmerStateThrowHoe(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new FarmerStateDown(eM, eC, attackCount, summonCount));
        }

        public void OnStateExit()
        {

        }
        
    }
}
