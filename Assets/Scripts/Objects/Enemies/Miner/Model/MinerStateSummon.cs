using System;
using System.Collections.Generic;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Objects.Enemies.Miner.Model
{
    public class MinerStateSummon : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public MinerStateSummon(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            float moleCount = eM.GetUP("MoleCount");
            for (int i = 0; i < moleCount; i++)
            {
                await SummonDataList.Instance.SummonByEnemy("Mole", eM.Pos, eM.Token);
                await UniTask.Delay(TimeSpan.FromSeconds(summonSeconds / moleCount), cancellationToken: eM.Token);
            }
            
            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new MinerStateRideCart(eM, eC, attackCount, summonCount));
            else if (attackCount == 0)
                eM.ChangeState(new MinerStateMakeWall(eM, eC, attackCount, summonCount));
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
    }
}
