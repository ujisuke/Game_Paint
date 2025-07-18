using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public class SStateLose : ISState
    {
        private readonly StageSystemModel sSM;

        public SStateLose(StageSystemModel sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("SStateLose");
            SceneChangerModel.Instance.LoadSceneRetry();
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
