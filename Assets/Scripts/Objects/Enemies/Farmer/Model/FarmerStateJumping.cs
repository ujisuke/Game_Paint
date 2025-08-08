using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateJumping : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private Vector2 targetPos;
        private float seconds;
        private float jumpSeconds;

        public FarmerStateJumping(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
            seconds = 0f;
        }

        public void OnStateEnter()
        {
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true);
            targetPos = StageData.Instance.CalcRandomPosFarFrom(playerPos);
            jumpSeconds = eM.GetUP("JumpSeconds");
            eC.FlipX(playerPos.x < eM.PA.Pos.x);
            eC.PlayAnim("Jumping");
        }

        public void OnUpdate()
        {
            if (seconds >= jumpSeconds)
                eM.ChangeState(new FarmerStateJumpEnd(eM, eC));
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            seconds += Time.deltaTime;
            eM.Move((targetPos - eM.PA.Pos) * Time.deltaTime / jumpSeconds);
            eC.FlipX(ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true).x < eM.PA.Pos.x);
        }

        public void OnStateExit()
        {

        }
    }
}
