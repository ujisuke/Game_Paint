using System;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class StagePEData
    {
        [SerializeField] private string sceneName;
        private GameObject playerPrefab;
        [SerializeField] private Vector2 playerInitPos;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Vector2 enemyInitPos;
        public string SceneName => sceneName;
        public GameObject PlayerPrefab => playerPrefab;
        public Vector2 PlayerInitPos => playerInitPos;
        public GameObject EnemyPrefab => enemyPrefab;
        public Vector2 EnemyInitPos => enemyInitPos;

        public void AddPlayerPrefab(GameObject playerPrefab)
        {
            this.playerPrefab = playerPrefab;
        }
    }
}
