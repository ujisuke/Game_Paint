using UnityEngine;
using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameSystems.SceneChanger.Model
{
    public class SceneChangerModel
    {
        private readonly string sceneNameTitle;
        private readonly string sceneNameMap;
        private readonly string sceneNameUI;
        private bool isRetry;
        private static SceneChangerModel instance;
        public bool IsRetry => isRetry;
        public static SceneChangerModel Instance => instance;

        private SceneChangerModel(string sceneNameTitle, string sceneNameMap, string sceneNameUI, bool isRetry)
        {
            this.sceneNameTitle = sceneNameTitle;
            this.sceneNameMap = sceneNameMap;
            this.sceneNameUI = sceneNameUI;
            this.isRetry = isRetry;
        }

        public static void Initialize(string sceneNameTitle, string sceneNameMap, string sceneNameUI)
        {
            instance = new SceneChangerModel(sceneNameTitle, sceneNameMap, sceneNameUI, false);
        }

        public void LoadSceneTitle()
        {
            isRetry = false;
            SceneManager.LoadScene(sceneNameTitle);
        }

        public void LoadSceneMap()
        {
            isRetry = false; 
            SceneManager.LoadScene(sceneNameMap);
        }

        public void LoadSceneStage()
        {
            isRetry = false;
            SceneManager.LoadScene(StageSelecter.CurrentStageSceneName);
            SceneManager.LoadScene(sceneNameUI, LoadSceneMode.Additive);
        }

        public void LoadSceneRetry()
        {
            isRetry = true;
            SceneManager.LoadScene(StageSelecter.CurrentStageSceneName);
            SceneManager.LoadScene(sceneNameUI, LoadSceneMode.Additive);
        }
    }
}
