using UnityEngine;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;

namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public class SStateBattle : ISState
    {
        private readonly StageSystemModel sSM;

        public SStateBattle(StageSystemModel sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("SStateBattle");
        }

        public void OnUpdate()
        {
            if (!ObjectsStorageModel.Instance.DoesEnemyExist() && ObjectsStorageModel.Instance.DoesPlayerExist())
                sSM.ChangeState(new SStateWin(sSM));
            else if (!ObjectsStorageModel.Instance.DoesPlayerExist())
                sSM.ChangeState(new SStateLose(sSM));
        }

        public void OnStateExit()
        {

        }
    }
}