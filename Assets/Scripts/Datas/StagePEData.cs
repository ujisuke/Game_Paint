using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class StagePEData
    {
        [SerializeField] private string sceneName;
        private GameObject playerPrefab;
        [SerializeField] private Vector2 playerInitPos;
        [SerializeField] private List<EnemyAndPosData> eDataList;
        public string SceneName => sceneName;
        public GameObject PlayerPrefab => playerPrefab;
        public Vector2 PlayerInitPos => playerInitPos;
        public List<EnemyAndPosData> EDataList => eDataList;

        public void AddPlayerPrefab(GameObject playerPrefab)
        {
            this.playerPrefab = playerPrefab;
        }
    }
}
