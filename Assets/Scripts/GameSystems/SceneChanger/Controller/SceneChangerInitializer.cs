using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.SceneChanger.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.SceneChanger.Controller
{
    public class SceneChangerInitializer : MonoBehaviour
    {
        [SerializeField] private string sceneNameTitle;
        [SerializeField] private string sceneNameMap;
        [SerializeField] private BattlePEDataList battlePEDataList;

        private void Awake()
        {
            SceneChangerModel.Initialize(sceneNameTitle, sceneNameMap, battlePEDataList);
        }
    }
}
