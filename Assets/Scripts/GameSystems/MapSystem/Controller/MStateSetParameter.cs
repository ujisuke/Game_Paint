using Assets.Scripts.GameSystems.InputSystem.Controller;
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
            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MStateLoadStage(mM, mSM));
            else if (CustomInputSystem.Instance.DoesBackKeyUp())
                mSM.ChangeState(new MStateChooseStage(mM, mSM));
        }

        public void OnStateExit()
        {

        }
    }
}
