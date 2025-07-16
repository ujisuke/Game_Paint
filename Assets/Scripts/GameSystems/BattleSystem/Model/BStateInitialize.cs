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
            GameObject.Instantiate(BattleSystemModel.Instance.BattlePEData.PlayerPrefab, BattleSystemModel.Instance.BattlePEData.PlayerInitPos, Quaternion.identity);
        }

        public void OnStateFixedUpdate()
        {
            if (!BattleSystemModel.Instance.IsRetry)
                bSM.ChangeState(new BStateConversation(bSM));
            for (int i = 0; i < BattleSystemModel.Instance.BattlePEData.EDataList.Count; i++)
                GameObject.Instantiate(BattleSystemModel.Instance.BattlePEData.EDataList[i].EnemyPrefab, BattleSystemModel.Instance.BattlePEData.EDataList[i].EnemyInitPos, Quaternion.identity);
            Debug.Log("Retrying Battle");
            bSM.ChangeState(new BStateBattle(bSM));
        }

        public void OnStateExit()
        {

        }
    }
}