using Assets.Scripts.GameSystems.ObjectStorage.Model;
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
            ObjectStorageModel.Instance.Clear();
            SceneChangerModel.Instance.LoadSceneRetry();
        }

        public void OnUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
