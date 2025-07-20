using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.MapSystem.Model;

namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public class StageSystemModel
    {
        private readonly SStateMachine sStateMachine;
        private readonly StagePEData stagePEData;
        public StagePEData StagePEData => stagePEData;

        public StageSystemModel(StagePEDataList stagePEDataList)
        {
            stagePEData = stagePEDataList.GetStagePEData(StageSelecter.CurrentStageSceneName);
            sStateMachine = new SStateMachine(this);
        }

        public void OnUpdate()
        {
            sStateMachine.OnUpdate();
        }

        public void ChangeState(ISState newState)
        {
            sStateMachine.ChangeState(newState);
        }
    }
}
