using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
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
            if(eM.IsLatter)
                summonCount++;
            ThrowPlant().Forget();
        }

        private async UniTask ThrowPlant()
        {
            float throwPlantSecondsHalf = eM.GetUP("ThrowPlantSeconds") * 0.5f;
            eC.PlayAnim("Throw", throwPlantSecondsHalf);
            await GameObject.InstantiateAsync(eM.EnemyData.GetAttackPrefab("Plant"), eM.Pos, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(throwPlantSecondsHalf), cancellationToken: eM.Token);
            eC.PlayAnim("ThrowEnd");
            await UniTask.Delay(TimeSpan.FromSeconds(throwPlantSecondsHalf), cancellationToken: eM.Token);

            if (attackCount >= eM.GetUP("AttackCountMax"))
                eM.ChangeState(new FarmerStateThrowScoop(eM, eC, attackCount, summonCount));
            else if (StageData.Instance.IsOnEdgeOfStage(eM.Pos))
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
            else if (summonCount >= eM.GetUP("SummonCountMax"))
                eM.ChangeState(new FarmerStateSummon(eM, eC, attackCount, summonCount));
            else if (Vector2.Distance(eM.Pos, ObjectStorageModel.Instance.GetPlayerPos(eM.Pos)) <= eM.GetUP("NearDistance"))
                eM.ChangeState(new FarmerStateSwingScythe(eM, eC, attackCount, summonCount));
            else
                eM.ChangeState(new FarmerStateJump(eM, eC, attackCount, summonCount));
        }

        public void OnUpdate()
        {
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (eM.DoesGetHPHalf)
                eM.ChangeState(new FarmerStateDown(eM, eC, attackCount, summonCount));
        }

        public void OnStateExit()
        {

        }
        
    }
}
