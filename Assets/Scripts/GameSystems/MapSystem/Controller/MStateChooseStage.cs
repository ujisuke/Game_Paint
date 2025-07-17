using Assets.Scripts.GameSystems.InputSystem.Controller;
using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MStateChooseStage : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MapSystemStateMachine mSM;

        public MStateChooseStage(MapSystemModel mapSystemModel, MapSystemStateMachine stateMachine)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
        }

        public void OnStateEnter()
        {
            Debug.Log("MStateChooseBattle");
        }

        public void HandleInput()
        {
            if (Input.GetKey(KeyCode.RightArrow))
                mM.ChangeStageToRight();
            else if (Input.GetKey(KeyCode.LeftArrow))
                mM.ChangeStageToLeft();
            else if (Input.GetKey(KeyCode.UpArrow))
                mM.ChangeStageToUp();
            else if (Input.GetKey(KeyCode.DownArrow))
                mM.ChangeStageToDown();
                
            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MStateSetParameter(mM, mSM));
        }

        public void OnStateExit()
        {

        }
    }
}
