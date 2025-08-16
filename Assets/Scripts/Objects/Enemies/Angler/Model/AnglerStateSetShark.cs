using System;
using System.Collections.Generic;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Angler.Model
{
    public class AnglerStateSetShark : IEStateAfterBorn
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public AnglerStateSetShark(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public AnglerStateSetShark(EnemyController enemyController)
        {
            eM = null;
            eC = enemyController;
            attackCount = 0;
            summonCount = 0;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new AnglerStateSetShark(enemyModel, enemyController, 0, 0);

        public void OnStateEnter()
        {
            attackCount++;
            if(eM.IsLatter)
                summonCount++;
            GameObject.Instantiate(eM.EnemyData.GetAttackPrefab("Shark"), StageData.StageEdgePosMin, Quaternion.identity);

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new AnglerStateBigCatch(eM, eC, attackCount, summonCount));
            else if (summonCount == (int)eM.GetUP("SummonCountOfFish"))
                eM.ChangeState(new AnglerStateSummonFish(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountOfSquid"))
                eM.ChangeState(new AnglerStateSummonSquid(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new AnglerStateMakeBarrier(eM, eC, attackCount, summonCount));
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
