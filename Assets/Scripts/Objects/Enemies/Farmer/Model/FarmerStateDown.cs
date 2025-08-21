using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateDown : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private readonly int attackCount;
        private readonly int summonCount;

        public FarmerStateDown(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            Down().Forget();
        }

        private async UniTask Down()
        {
            eC.PlayAnim("Down");
            await eM.OnDown();
            
            await UniTask.Delay(TimeSpan.FromSeconds(eM.EnemyData.DownSeconds), cancellationToken: eM.Token);
            eM.ChangeState(new FarmerStateSummon(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
        }

        public void OnStateExit()
        {

        }
        
    }
}
