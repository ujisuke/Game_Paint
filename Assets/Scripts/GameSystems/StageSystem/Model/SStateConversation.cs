using UnityEngine;

namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public class SStateConversation : ISState
    {
        private readonly StageSystemModel sSM;

        public SStateConversation(StageSystemModel sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            Debug.Log("SStateConversation");
            for(int i = 0;  i < sSM.StagePEData.EDataList.Count; i++)
                GameObject.Instantiate(sSM.StagePEData.EDataList[i].EnemyPrefab, sSM.StagePEData.EDataList[i].EnemyInitPos, Quaternion.identity);
            sSM.ChangeState(new SStateBattle(sSM));
        }

        public void OnUpdate()
        {

        }

        public void OnStateExit()
        {

        }
    }
}