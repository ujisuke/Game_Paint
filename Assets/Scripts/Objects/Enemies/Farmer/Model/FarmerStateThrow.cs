using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Enemies.Base.Controller;
using Assets.Scripts.Objects.Enemies.Base.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Farmer.Model
{
    public class FarmerStateThrow : IEState
    {
        private readonly EnemyModel eM;
        private readonly EnemyController eC;
        private float seconds;

        public FarmerStateThrow(EnemyModel enemyModel, EnemyController enemyController)
        {
            eM = enemyModel;
            eC = enemyController;
            seconds = 0f;
        }

        public void OnStateEnter()
        {
            eC.PlayAnim("Walk");
        }

        public void OnUpdate()
        {
            if (seconds >= eM.GetUP("ThrowSeconds"))
                eM.ChangeState(new FarmerStateJumpBegin(eM, eC));
            if (eM.IsDead())
                eM.ChangeState(new EStateDead(eM, eC));
            Vector2 playerPos = ObjectsStorageModel.Instance.GetHostilePos(eM.PA.Pos, true);
            eC.FlipX(playerPos.x < eM.PA.Pos.x);
            seconds += Time.deltaTime;
            eM.Move((eM.PA.Pos - playerPos).normalized * Time.deltaTime);
        }

        public void OnStateExit()
        {

        }
    }
}
