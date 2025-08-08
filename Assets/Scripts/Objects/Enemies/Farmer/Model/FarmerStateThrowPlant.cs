using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateThrowPlant : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private float restSeconds;

        public FarmerStateThrowPlant(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
        }

        public void OnStateEnter()
        {
            eC.PlayAnim("Idle");
            restSeconds = eM.GetUP("ThrowPlantSeconds");
            GameObject.Instantiate(eM.EnemyData.GetAttackPrefab("Plant"), eM.PA.Pos, Quaternion.identity);
        }

        public void OnUpdate()
        {
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true);
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            if (restSeconds <= 0f)
                if (Vector2.Distance(eM.PA.Pos, playerPos) <= eM.GetUP("NearDistance") || StageData.Instance.IsOutOfStage(eM.PA.Pos))
                    eM.ChangeState(new FarmerStateJumpBegin(eM, eC));
                else
                    eM.ChangeState(new FarmerStateThrowHoe(eM, eC));
            restSeconds -= Time.deltaTime;
        }

        public void OnStateExit()
        {

        }
        
    }
}
