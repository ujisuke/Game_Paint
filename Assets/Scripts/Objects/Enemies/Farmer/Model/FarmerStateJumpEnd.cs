using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateJumpEnd : IEStateAfterBorn
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private float restSeconds;

        public FarmerStateJumpEnd(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new FarmerStateJumpBegin(enemyModel, enemyController);

        public void OnStateEnter()
        {
            restSeconds = 0.1f;
            eC.FlipX(ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true).x < eM.PA.Pos.x);
            eC.PlayAnim("JumpEnd");
        }

        public void OnUpdate()
        {
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true);
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            else if (restSeconds <= 0)
                if (Random.Range(0, 2) == 0)
                    eM.ChangeState(new FarmerStateThrowPlant(eM, eC));
                else
                    eM.ChangeState(new FarmerStateThrowHoe(eM, eC));
            restSeconds -= Time.deltaTime;
            eC.FlipX(playerPos.x < eM.PA.Pos.x);
        }

        public void OnStateExit()
        {

        }
    }
}
