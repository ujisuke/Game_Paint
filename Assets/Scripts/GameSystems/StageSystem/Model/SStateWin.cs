using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public class SStateWin : ISState
    {
        private readonly StageSystemModel sSM;

        public SStateWin(StageSystemModel sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("SStateWin");
            SceneChangerModel.Instance.LoadSceneMap();
        }

        public void OnUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}