using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Angler.Model
{
    public class AnglerStateSummonFish : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public AnglerStateSummonFish(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount++;
            summonCount++;
            Summon().Forget();
        }

        private async UniTask Summon()
        {
            float summonSeconds = eM.GetUP("SummonFishSeconds") - 0.5f;
            eC.PlayAnim("Hide");
            eM.SetHurtBoxActive(false);
            float fishCount = eM.GetUP("FishCount");
            for (int i = 0; i < fishCount; i++)
            {
                Vector2 randomPos = StageData.Instance.CalcRandomPosInStage();
                await SummonDataList.Instance.SummonByEnemy("Fish", randomPos, eM.Token);
                await UniTask.Delay(TimeSpan.FromSeconds(summonSeconds / fishCount), cancellationToken: eM.Token);
            }
            eC.PlayAnim("Appear");
            eM.SetHurtBoxActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: eM.Token);

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new AnglerStateBigCatch(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfSquid"))
                eM.ChangeState(new AnglerStateSummonSquid(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new AnglerStateFishing(eM, eC, attackCount, summonCount));
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
