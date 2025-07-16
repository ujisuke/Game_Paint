using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MStateSetParameter : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MapSystemStateMachine mSM;

        public MStateSetParameter(MapSystemModel mapSystemModel, MapSystemStateMachine stateMachine)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
        }

        public void OnStateEnter()
        {
            Debug.Log("MStateSetParameter");
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
