using Assets.Scripts.GameSystems.MapSystem.Model;
using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{

    public class MStateLoadStage : IMState
    {
        private readonly MapSystemModel mM;
        private readonly MapSystemStateMachine mSM;

        public MStateLoadStage(MapSystemModel mapSystemModel, MapSystemStateMachine stateMachine)
        {
            mM = mapSystemModel;
            mSM = stateMachine;
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
