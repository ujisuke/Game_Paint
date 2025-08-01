using System.Collections.Generic;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.MapSystem.Model;
using Assets.Scripts.GameSystems.MapSystem.View;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MapSystemController : MonoBehaviour
    {
        [SerializeField] private StageOnMapDataList mapDataList;
        private MStateMachine mStateMachine;
        private MapSystemModel mapSystemModel;
        [SerializeField] private List<StageOnMap> stageOnMapList;
        private StageOnMapStorage stageOnMapStorage;
        public StageOnMapStorage StageOnMapStorage => stageOnMapStorage;

        private void Awake()
        {
            mapSystemModel = new MapSystemModel(mapDataList);
            mStateMachine = new MStateMachine(mapSystemModel, this);
            stageOnMapStorage = new StageOnMapStorage(stageOnMapList);
        }

        private void Update()
        {
            mStateMachine.HandleInput();
        }
    }
}
