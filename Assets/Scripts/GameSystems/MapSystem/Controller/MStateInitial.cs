using Assets.Scripts.GameSystems.InputSystem.Controller;
using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MStateInitial : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MStateMachine mSM;
        private MapSystemController mC;

        public MStateInitial(MapSystemModel mapSystemModel, MStateMachine stateMachine, MapSystemController controller)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
            mC = controller;
        }

        public void OnStateEnter()
        {
            Debug.Log("MStateInitial");
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MStateChooseStage(mM, mSM, mC));
        }

        public void OnStateExit()
        {

        }
    }
}
