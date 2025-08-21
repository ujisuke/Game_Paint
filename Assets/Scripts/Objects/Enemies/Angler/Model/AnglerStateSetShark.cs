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
        private readonly int attackCount;
        private readonly int summonCount;

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
            GameObject.Instantiate(eM.EnemyData.GetAttackPrefab("Shark"), StageData.StageEdgePosMin, Quaternion.identity);

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
