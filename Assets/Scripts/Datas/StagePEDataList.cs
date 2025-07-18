using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "StagePEDataList", menuName = "ScriptableObjects/StagePEDataList")]
    public class StagePEDataList : ScriptableObject
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<StagePEData> stagePEDataList;
        private Dictionary<string, StagePEData> stagePEDataDictionary;

        private void Initialize()
        {
            stagePEDataDictionary = new Dictionary<string, StagePEData>();

            for (int i = 0; i < stagePEDataList.Count; i++)
                stagePEDataDictionary = AddDictionary(playerPrefab, stagePEDataList[i], stagePEDataDictionary);
        }

        private static Dictionary<string, StagePEData> AddDictionary(GameObject playerPrefab, StagePEData stagePEData, Dictionary<string, StagePEData> battlePEDataDictionary)
        {
            Dictionary<string, StagePEData> updatedBattlePEDataDictionary = new(battlePEDataDictionary);

            stagePEData.AddPlayerPrefab(playerPrefab);
            if (!battlePEDataDictionary.ContainsKey(stagePEData.SceneName))
                updatedBattlePEDataDictionary.Add(stagePEData.SceneName, stagePEData);
            return updatedBattlePEDataDictionary;
        }

        public StagePEData GetStagePEData(string stageName)
        {
            if (stagePEDataDictionary == null)
                Initialize();
            return stagePEDataDictionary[stageName];
        }
    }
}