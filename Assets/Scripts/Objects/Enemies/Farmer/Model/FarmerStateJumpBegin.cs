using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateJumpBegin : IEStateAfterBorn
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private float seconds;
        private float jumpAwakeSeconds;

        public FarmerStateJumpBegin(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
            seconds = 0f;
        }

        public IEState Initialize(EnemyModel enemyModel, EnemyController enemyController) => new FarmerStateJumpBegin(enemyModel, enemyController);

        public void OnStateEnter()
        {
            jumpAwakeSeconds = 0.1f;
            eC.FlipX(ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true).x < eM.PA.Pos.x);
            eC.PlayAnim("JumpBegin");
        }

        public void OnUpdate()
        {
            if (seconds >= jumpAwakeSeconds)
                eM.ChangeState(new FarmerStateJumping(eM, eC));
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
