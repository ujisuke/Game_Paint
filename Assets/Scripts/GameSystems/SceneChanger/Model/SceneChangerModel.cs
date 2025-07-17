using Assets.Scripts.GameSystems.MapSystem.Model;

namespace Assets.Scripts.GameSystems.SceneChanger.Model
{
    public class SceneChangerModel
    {
        private readonly string sceneNameTitle;
        private readonly string sceneNameMap;
        private bool isRetry;
        private static SceneChangerModel instance;
        public bool IsRetry => isRetry;
        public static SceneChangerModel Instance => instance;

        private SceneChangerModel(string sceneNameTitle, string sceneNameMap, bool isRetry)
        {
            this.sceneNameTitle = sceneNameTitle;
            this.sceneNameMap = sceneNameMap;
            this.isRetry = isRetry;
        }

        public static void Initialize(string sceneNameTitle, string sceneNameMap)
        {
            instance = new SceneChangerModel(sceneNameTitle, sceneNameMap, false);
        }

        public void LoadSceneTitle()
        {
            isRetry = false;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameTitle);
        }

        public void LoadSceneMap()
        {
            isRetry = false; 
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameMap);
        }

        public void LoadSceneStage()
        {
            isRetry = false;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(StageSelecter.CurrentStageSceneName);
        }

        public void LoadSceneRetry()
        {
            isRetry = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(StageSelecter.CurrentStageSceneName);
        }
    }
}
