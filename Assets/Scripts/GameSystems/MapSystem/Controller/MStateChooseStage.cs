using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.InputSystem.Controller;
using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MStateChooseStage : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MStateMachine mSM;
        private MapSystemController mC;

        public MStateChooseStage(MapSystemModel mapSystemModel, MStateMachine stateMachine, MapSystemController controller)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
            mC = controller;
        }

        public void OnStateEnter()
        {
            Debug.Log("MStateChooseBattle");
        }

        public void HandleInput()
        {
            if (Input.GetKey(KeyCode.D))
                mM.ChangeStageTo(MoveDirOnMap.Right);
            else if (Input.GetKey(KeyCode.A))
                mM.ChangeStageTo(MoveDirOnMap.Left);
            else if (Input.GetKey(KeyCode.W))
                mM.ChangeStageTo(MoveDirOnMap.Up);
            else if (Input.GetKey(KeyCode.S))
                mM.ChangeStageTo(MoveDirOnMap.Down);
            mC.StageOnMapStorage.IndicateCurrentStage(StageSelecter.CurrentStageSceneName);
                
            if (CustomInputSystem.Instance.DoesSelectKeyUp())
                mSM.ChangeState(new MStateSetParameter(mM, mSM, mC));
        }

        public void OnStateExit()
        {

        }
    }
}
