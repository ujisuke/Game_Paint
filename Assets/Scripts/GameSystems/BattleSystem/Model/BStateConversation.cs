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
            for(int i = 0;  i < bSM.BattlePEData.EDataList.Count; i++)
                GameObject.Instantiate(bSM.BattlePEData.EDataList[i].EnemyPrefab, bSM.BattlePEData.EDataList[i].EnemyInitPos, Quaternion.identity);
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