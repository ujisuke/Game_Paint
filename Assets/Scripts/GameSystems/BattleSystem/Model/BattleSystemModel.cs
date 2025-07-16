using Assets.Scripts.Datas;

namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public class BattleSystemModel
    {
        private readonly BStateMachine bStateMachine;
        private readonly string sceneNameBattle;
        private readonly bool isRetry;
        private readonly BattlePEData battlePEData;
        private static BattleSystemModel instance;

        public string SceneNameBattle => sceneNameBattle;
        public bool IsRetry => isRetry;
        public BattlePEData BattlePEData => battlePEData;
        public static BattleSystemModel Instance => instance;


        private BattleSystemModel(string sceneNameBattle, bool isRetry, BattlePEData battlePEData)
        {
            this.sceneNameBattle = sceneNameBattle;
            this.isRetry = isRetry;
            this.battlePEData = battlePEData;
            instance = this;
        }

        public BattleSystemModel()
        {
            sceneNameBattle = instance.sceneNameBattle;
            isRetry = instance.isRetry;
            battlePEData = instance.battlePEData;
            instance = this;
            bStateMachine = new BStateMachine(this);
        }

        public static void Initialize(string sceneNameBattle, bool isRetry, BattlePEData battlePEData)
        {
            instance = new BattleSystemModel(sceneNameBattle, isRetry, battlePEData);
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
