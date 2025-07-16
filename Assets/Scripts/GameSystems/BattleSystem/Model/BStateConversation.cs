using UnityEngine;

namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public class BStateConversation : IBState
    {
        private readonly BattleSystemModel bSM;

        public BStateConversation(BattleSystemModel bSM)
        {
            this.bSM = bSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("BStateConversation");
            for(int i = 0;  i < BattleSystemModel.Instance.BattlePEData.EDataList.Count; i++)
                GameObject.Instantiate(BattleSystemModel.Instance.BattlePEData.EDataList[i].EnemyPrefab, BattleSystemModel.Instance.BattlePEData.EDataList[i].EnemyInitPos, Quaternion.identity);
            bSM.ChangeState(new BStateBattle(bSM));
        }

        public void OnStateFixedUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}