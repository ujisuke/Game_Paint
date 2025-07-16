using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.BattleSystem.Model;

namespace Assets.Scripts.GameSystems.SceneChanger.Model
{
    public class SceneChangerModel
    {
        private readonly string sceneNameTitle;
        private readonly string sceneNameMap;
        private readonly BattlePEDataList battlePEDataList;
        private static SceneChangerModel instance;
        public static SceneChangerModel Instance => instance;

        private SceneChangerModel(string sceneNameTitle, string sceneNameMap, BattlePEDataList battlePEDataList)
        {
            this.sceneNameTitle = sceneNameTitle;
            this.sceneNameMap = sceneNameMap;
            this.battlePEDataList = battlePEDataList;
            instance = this;
        }

        public static void Initialize(string sceneNameTitle, string sceneNameMap, BattlePEDataList battlePEDataList)
        {
            instance = new SceneChangerModel(sceneNameTitle, sceneNameMap, battlePEDataList);
        }

        public void LoadSceneTitle()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameTitle);
        }

        public void LoadSceneMap()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameMap);
        }

        public void LoadSceneBattle(string sceneName, bool isRetry)
        {
            BattleSystemModel.Initialize(sceneName, isRetry, battlePEDataList.GetBattlePEData(sceneName));
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
