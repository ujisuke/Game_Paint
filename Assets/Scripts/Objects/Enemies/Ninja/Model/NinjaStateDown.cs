using System;
using System.Collections.Generic;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Objects.Enemies.Ninja.Model
{
    public class NinjaStateDown : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private readonly int attackCount;
        private readonly int summonCount;

        public NinjaStateDown(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            if (eM.IsLatter)
                eM.ChangeState(new NinjaStateSummonFrog(eM, eC, attackCount, summonCount));
            else eM.ChangeState(new NinjaStateSlash(eM, eC, attackCount, summonCount));
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
