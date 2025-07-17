namespace Assets.Scripts.GameSystems.SceneChanger.Model
{
    public class SceneChangerModel
    {
        private readonly string sceneNameTitle;
        private readonly string sceneNameMap;
        private bool isRetry;
        private string sceneNameCurrent;
        private static SceneChangerModel instance;
        public string SceneNameCurrent => sceneNameCurrent;
        public bool IsRetry => isRetry;
        public static SceneChangerModel Instance => instance;

        private SceneChangerModel(string sceneNameTitle, string sceneNameMap, bool isRetry, string sceneNameCurrent)
        {
            this.sceneNameTitle = sceneNameTitle;
            this.sceneNameMap = sceneNameMap;
            this.isRetry = isRetry;
            this.sceneNameCurrent = sceneNameCurrent;
        }

        public static void Initialize(string sceneNameTitle, string sceneNameMap)
        {
            instance = new SceneChangerModel(sceneNameTitle, sceneNameMap, false, sceneNameTitle);
        }

        public void LoadSceneTitle()
        {
            isRetry = false;
            sceneNameCurrent = sceneNameTitle;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameTitle);
        }

        public void LoadSceneMap()
        {
            isRetry = false; 
            sceneNameCurrent = sceneNameMap;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameMap);
        }

        public void LoadSceneStage(string sceneNameStage)
        {
            isRetry = false;
            sceneNameCurrent = sceneNameStage;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameStage);
        }

        public void LoadSceneRetry()
        {
            isRetry = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameCurrent);
        }
    }
}
