using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.SceneChanger.Controller
{
    public class SceneChangerController : MonoBehaviour
    {
        [SerializeField] private string sceneNameTitle;
        [SerializeField] private string sceneNameMap;

        private void Awake()
        {
            SceneChangerModel.Initialize(sceneNameTitle, sceneNameMap);
        }
    }
}
