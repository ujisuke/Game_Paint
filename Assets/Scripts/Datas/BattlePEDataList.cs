using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "BattlePEDataList", menuName = "ScriptableObjects/BattlePEDataList")]
    public class BattlePEDataList : ScriptableObject
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<BattlePEData> battlePEDataList;
        private Dictionary<string, BattlePEData> battlePEDataDictionary;

        private void Initialize()
        {
            battlePEDataDictionary = new Dictionary<string, BattlePEData>();

            for (int i = 0; i < battlePEDataList.Count; i++)
                battlePEDataDictionary = AddDictionary(playerPrefab, battlePEDataList[i], battlePEDataDictionary);
        }

        private static Dictionary<string, BattlePEData> AddDictionary(GameObject playerPrefab, BattlePEData battlePEData, Dictionary<string, BattlePEData> battlePEDataDictionary)
        {
            Dictionary<string, BattlePEData> updatedBattlePEDataDictionary = new(battlePEDataDictionary);

            battlePEData.AddPlayerPrefab(playerPrefab);
            if (!battlePEDataDictionary.ContainsKey(battlePEData.SceneName))
                updatedBattlePEDataDictionary.Add(battlePEData.SceneName, battlePEData);
            return updatedBattlePEDataDictionary;
        }

        public BattlePEData GetBattlePEData(string stageName)
        {
            if (battlePEDataDictionary == null)
                Initialize();
            return battlePEDataDictionary[stageName];
        }
    }
}