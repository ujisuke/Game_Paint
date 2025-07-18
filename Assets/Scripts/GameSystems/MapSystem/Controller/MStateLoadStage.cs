using Assets.Scripts.GameSystems.MapSystem.Model;
using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{

    public class MStateLoadStage : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MStateMachine mSM;
        private MapSystemController mC;

        public MStateLoadStage(MapSystemModel mapSystemModel, MStateMachine stateMachine, MapSystemController controller)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
            mC = controller;
        }

        public void OnStateEnter()
        {
            Debug.Log("MStateLoadStage");
            SceneChangerModel.Instance.LoadSceneStage();
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
