using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateSwingScythe : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public FarmerStateSwingScythe(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
        {
            eM = enemyModel;
            eC = enemyController;
            this.attackCount = attackCount;
            this.summonCount = summonCount;
        }

        public void OnStateEnter()
        {
            attackCount++;
            if(eM.IsLessThanHalfHP())
                summonCount++;
            SwingScythe().Forget();
        }

        private async UniTask SwingScythe()
        {
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Scythe"), eM.PA.Pos, Quaternion.identity);
            eC.PlayAnim("SwingBegin");
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("SwingScytheDelaySeconds")), cancellationToken: eM.Token);
            float swingScytheSeconds = eM.GetUP("SwingScytheSeconds");
            eC.PlayAnim("Swing", swingScytheSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(swingScytheSeconds), cancellationToken: eM.Token);
            await UniTask.Delay(TimeSpan.FromSeconds(eM.GetUP("SwingScytheCoolDownSeconds")), cancellationToken: eM.Token);
            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new FarmerStateThrowScoop(eM, eC, attackCount, summonCount));
            else if (StageData.Instance.IsOnEdgeOfStage(eM.PA.Pos))
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new FarmerStateSummon(eM, eC, attackCount, summonCount));
            else if (Vector2.Distance(eM.PA.Pos, ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true)) <= eM.GetUP("NearDistance"))
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
            else
                if (UnityEngine.Random.Range(0, 2) == 0)
                    eM.ChangeState(new FarmerStateThrowHoe(eM, eC, attackCount, summonCount));
                else
                    eM.ChangeState(new FarmerStateThrowPlant(eM, eC, attackCount, summonCount));
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
