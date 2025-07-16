using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "StageOnMapDataList", menuName = "ScriptableObjects/StageOnMapDataList")]
    public class StageOnMapDataList : ScriptableObject
    {
        [SerializeField] private List<StageOnMapData> stageOnMapDataList;
        private Dictionary<string, StageOnMapData> stageOnMapDataDictionary;

        private void Initialize()
        {
            stageOnMapDataDictionary = new Dictionary<string, StageOnMapData>();
            for(int i = 0; i < stageOnMapDataList.Count; i++)
            {
                StageOnMapData stageOnMapData = stageOnMapDataList[i];
                stageOnMapData.PrevStageData = i == 0 ? null : stageOnMapDataList[i - 1];
                stageOnMapData.NextStageData = i == stageOnMapDataList.Count - 1 ? null : stageOnMapDataList[i + 1];
                if (stageOnMapData != null)
                    stageOnMapDataDictionary = AddDictionary(stageOnMapData, stageOnMapDataDictionary);
            }
        }

        private static Dictionary<string, StageOnMapData> AddDictionary(StageOnMapData stageOnMapData, Dictionary<string, StageOnMapData> mapDataDictionary)
        {
            Dictionary<string, StageOnMapData> updatedMapDataDictionary = new(mapDataDictionary);

            if (!mapDataDictionary.ContainsKey(stageOnMapData.StageSceneName))
                updatedMapDataDictionary.Add(stageOnMapData.StageSceneName, stageOnMapData);
            return updatedMapDataDictionary;
        }

        public StageOnMapData GetMapData(string stageSceneName)
        {
            if (stageOnMapDataDictionary == null)
                Initialize();
            return stageOnMapDataDictionary[stageSceneName];
        }
    }
}