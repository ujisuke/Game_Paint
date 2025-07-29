using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public class SStateInitialize : ISState
    {
        private readonly StageSystemModel sSM;

        public SStateInitialize(StageSystemModel sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("SStateInitialize");
            GameObject.Instantiate(sSM.StagePEData.PlayerPrefab, sSM.StagePEData.PlayerInitPos, Quaternion.identity);
        }

        public void OnUpdate()
        {
            if (!SceneChangerModel.Instance.IsRetry)
            {
                sSM.ChangeState(new SStateConversation(sSM));
                return;
            }
            GameObject.Instantiate(sSM.StagePEData.EnemyPrefab, sSM.StagePEData.EnemyInitPos, Quaternion.identity);
            Debug.Log("Retrying Stage");
            sSM.ChangeState(new SStateBattle(sSM));
        }

        public void OnStateExit()
        {

        }
    }
}