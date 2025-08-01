using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameSystems.MapSystem.Model;

namespace Assets.Scripts.GameSystems.MapSystem.View
{
    public class StageOnMapStorage
    {
        private readonly Dictionary<string, StageOnMap> stageOnMapDictionary;
        private StageOnMap currentStageOnMap;

        public StageOnMapStorage(List<StageOnMap> stageOnMapList)
        {
            stageOnMapDictionary = InitializeDictionary(stageOnMapList);
            SetAllStageOnMap(stageOnMapList);
        }

        private static Dictionary<string, StageOnMap> InitializeDictionary(List<StageOnMap> stageOnMapList)
        {
            Dictionary<string, StageOnMap> newDictionary = new();
            for (int i = 0; i < stageOnMapList.Count; i++)
            {
                StageOnMap stageOnMap = stageOnMapList[i];
                if (!newDictionary.ContainsKey(stageOnMap.StageSceneName))
                    newDictionary.Add(stageOnMap.StageSceneName, stageOnMap);
            }
            return newDictionary;
        }

        private void SetAllStageOnMap(List<StageOnMap> stageOnMapList)
        {
            for (int i = 0; i < stageOnMapList.Count; i++)
                stageOnMapList[i].Deselect();
            currentStageOnMap = stageOnMapDictionary[StageSelecter.CurrentStageSceneName];
            currentStageOnMap.Select();
        }

        public void IndicateCurrentStage(string newStageSceneName)
        {
            if (newStageSceneName == currentStageOnMap.StageSceneName)
                return;
            currentStageOnMap.Deselect();
            currentStageOnMap = stageOnMapDictionary[newStageSceneName];
            currentStageOnMap.Select();
        }
    }
}
