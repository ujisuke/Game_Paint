using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateThrowPlant : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private int attackCount;
        private int summonCount;

        public FarmerStateThrowPlant(EnemyModel enemyModel, EnemyController enemyController, int attackCount, int summonCount)
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
            ThrowPlant().Forget();
        }

        private async UniTask ThrowPlant()
        {
            float throwPlantSecondsHalf = eM.GetUP("ThrowPlantSeconds") * 0.5f;
            eC.PlayAnim("Throw", throwPlantSecondsHalf);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Plant"), eM.PA.Pos, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(throwPlantSecondsHalf), cancellationToken: eM.Token);
            eC.PlayAnim("ThrowEnd");
            await UniTask.Delay(TimeSpan.FromSeconds(throwPlantSecondsHalf), cancellationToken: eM.Token);
            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new FarmerStateThrowScoop(eM, eC, attackCount, summonCount));
            else if (StageData.Instance.IsOutOfStage(eM.PA.Pos))
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new FarmerStateSummon(eM, eC, attackCount, summonCount));
            else if (Vector2.Distance(eM.PA.Pos, ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true)) <= eM.GetUP("NearDistance"))
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
        }

        public void OnStateExit()
        {

        }
        
    }
}
