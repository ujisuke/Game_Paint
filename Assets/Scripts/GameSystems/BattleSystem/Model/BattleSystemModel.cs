using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.MapSystem.Model;

namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public class BattleSystemModel
    {
        private readonly BStateMachine bStateMachine;
        private readonly BattlePEData battlePEData;
        public BattlePEData BattlePEData => battlePEData;

        public BattleSystemModel(BattlePEDataList battlePEDataList)
        {
            battlePEData = battlePEDataList.GetBattlePEData(StageSelecter.CurrentStageSceneName);
            bStateMachine = new BStateMachine(this);
        }

        public void FixedUpdate()
        {
            bStateMachine.FixedUpdate();
        }

        public void ChangeState(IBState newState)
        {
            bStateMachine.ChangeState(newState);
        }
    }
}
