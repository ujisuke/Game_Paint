using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Angler.Model
{
    public class AnglerStateSummonSquid : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public AnglerStateSummonSquid(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            float summonSeconds = eM.GetUP("SummonSquidSeconds") - 0.5f;
            eC.PlayAnim("Hide");
            eM.SetHurtBoxActive(false);
            float squidCount = eM.GetUP("SquidCount");
            Vector2 playerPos = ObjectStorageModel.Instance.GetPlayerPos(eM.Pos);
            for (int i = 0; i < squidCount; i++)
            {
                Vector2 randomPos = StageData.Instance.CalcRandomPosFarFrom(playerPos);
                await SummonDataList.Instance.SummonByEnemy("Squid", randomPos, eM.Token);
                await UniTask.Delay(TimeSpan.FromSeconds(summonSeconds / squidCount), cancellationToken: eM.Token);
            }
            eC.PlayAnim("Appear");
            eM.SetHurtBoxActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: eM.Token);

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new AnglerStateBigCatch(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfFish"))
                eM.ChangeState(new AnglerStateSummonFish(eM, eC, attackCount, summonCount));
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
