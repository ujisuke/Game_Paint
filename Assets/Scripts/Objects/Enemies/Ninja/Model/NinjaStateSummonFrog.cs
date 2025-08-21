using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Ninja.Model
{
    public class NinjaStateSummonFrog : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public NinjaStateSummonFrog(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            float summonSeconds = eM.GetUP("SummonFrogSeconds") - 0.5f;
            eC.PlayAnim("Hide");
            eM.SetHurtBoxActive(false);

            await SummonDataList.Instance.SummonByEnemy("Frog", eM.Pos, eM.Token);
            await UniTask.Delay(TimeSpan.FromSeconds(summonSeconds), cancellationToken: eM.Token);

            eC.PlayAnim("Appear");
            eM.SetHurtBoxActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: eM.Token);

            if (eM.MaxAttackCount <= attackCount)
                eM.ChangeState(new NinjaStateMultiSlash(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfScorpion"))
                eM.ChangeState(new NinjaStateSummonScorpion(eM, eC, attackCount, summonCount));
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
