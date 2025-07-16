using UnityEngine;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;

namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public class BStateBattle : IBState
    {
        private readonly BattleSystemModel bSM;

        public BStateBattle(BattleSystemModel bSM)
        {
            this.bSM = bSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("BStateBattle");
        }

        public void OnStateFixedUpdate()
        {
            if (!ObjectsStorageModel.Instance.DoesEnemyExist() && ObjectsStorageModel.Instance.DoesPlayerExist())
                bSM.ChangeState(new BStateWin(bSM));
            else if (!ObjectsStorageModel.Instance.DoesPlayerExist())
                bSM.ChangeState(new BStateLose(bSM));
        }

        public void OnStateExit()
        {

        }
    }
}