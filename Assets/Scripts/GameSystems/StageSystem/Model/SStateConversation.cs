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
                GameObject.Instantiate(sSM.StagePEData.EnemyPrefab, sSM.StagePEData.EnemyInitPos, Quaternion.identity);
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