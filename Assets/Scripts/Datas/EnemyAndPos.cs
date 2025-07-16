using System;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]

    public class EnemyAndPosData
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Vector2 enemyInitPos;
        public GameObject EnemyPrefab => enemyPrefab;
        public Vector2 EnemyInitPos => enemyInitPos;
    }
}
