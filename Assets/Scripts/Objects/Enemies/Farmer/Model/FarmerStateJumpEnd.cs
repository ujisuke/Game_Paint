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
        private float seconds;
        private float jumpEndSeconds;

        public FarmerStateJumpEnd(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
            seconds = 0f;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new FarmerStateJumpBegin(enemyModel, enemyController);

        public void OnStateEnter()
        {
            jumpEndSeconds = 0.1f;
            eC.FlipX(ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true).x < eM.PA.Pos.x);
            eC.PlayAnim("JumpEnd");
        }

        public void OnUpdate()
        {
            if (seconds >= jumpEndSeconds)
                eM.ChangeState(new FarmerStateThrow(eM, eC));
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            seconds += Time.deltaTime;
            eC.FlipX(ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true).x < eM.PA.Pos.x);
        }

        public void OnStateExit()
        {

        }
    }
}
