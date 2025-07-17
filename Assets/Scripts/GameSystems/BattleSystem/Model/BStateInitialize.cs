using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public class BStateInitialize : IBState
    {
        private readonly BattleSystemModel bSM;

        public BStateInitialize(BattleSystemModel bSM)
        {
            this.bSM = bSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("BStateInitialize");
            GameObject.Instantiate(bSM.BattlePEData.PlayerPrefab, bSM.BattlePEData.PlayerInitPos, Quaternion.identity);
        }

        public void OnStateFixedUpdate()
        {
            if (!SceneChangerModel.Instance.IsRetry)
                bSM.ChangeState(new BStateConversation(bSM));
            for (int i = 0; i < bSM.BattlePEData.EDataList.Count; i++)
                GameObject.Instantiate(bSM.BattlePEData.EDataList[i].EnemyPrefab, bSM.BattlePEData.EDataList[i].EnemyInitPos, Quaternion.identity);
            Debug.Log("Retrying Battle");
            bSM.ChangeState(new BStateBattle(bSM));
        }

        public void OnStateExit()
        {

        }
    }
}