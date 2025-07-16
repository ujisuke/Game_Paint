using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MStateInitial : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MapSystemStateMachine mSM;

        public MStateInitial(MapSystemModel mapSystemModel, MapSystemStateMachine stateMachine)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
        }

        public void OnStateEnter()
        {
            Debug.Log("MStateInitial");
        }

        public void HandleInput()
        {
            if (Input.GetKey(KeyCode.Q))
                mSM.ChangeState(new MStateChooseBattle(mM, mSM));
        }

        public void OnStateExit()
        {

        }
    }
}
