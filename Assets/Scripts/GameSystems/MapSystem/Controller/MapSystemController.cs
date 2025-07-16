using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.MapSystem.Model;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MapSystemController : MonoBehaviour
    {
        [SerializeField] private StageOnMapDataList mapDataList;
        private MapSystemStateMachine stateMachine;
        private MapSystemModel mapSystemModel;

        private void Awake()
        {
            //テスト用
            StageSelecter.CurrentStageSceneName = "1";
            //
            mapSystemModel = new MapSystemModel(mapDataList);
            stateMachine = new MapSystemStateMachine(mapSystemModel);
        }

        private void FixedUpdate()
        {
            stateMachine.HandleInput();
        }
    }
}
