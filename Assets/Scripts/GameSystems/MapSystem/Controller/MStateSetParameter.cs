using Assets.Scripts.GameSystems.InputSystem.Controller;
using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MStateSetParameter : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MapSystemStateMachine mSM;
        private MapSystemController mC;

        public MStateSetParameter(MapSystemModel mapSystemModel, MapSystemStateMachine stateMachine, MapSystemController controller)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
            mC = controller;
        }

        public void OnStateEnter()
        {
            Debug.Log("MStateSetParameter");
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MStateLoadStage(mM, mSM, mC));
            else if (CustomInputSystem.Instance.DoesBackKeyUp())
                mSM.ChangeState(new MStateChooseStage(mM, mSM, mC));
        }

        public void OnStateExit()
        {

        }
    }
}
