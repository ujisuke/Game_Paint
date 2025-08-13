using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string enemyName;
        [SerializeField] private float maxHP;
        [SerializeField] private float downSeconds;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 viewScale;
        [SerializeField] private float invincibleSecond;
        [SerializeField] private List<UniqueParameter> uniqueParameterList;
        [SerializeField] private List<GameObject> attackPrefabList;
        private Dictionary<string, float> uniqueParameters;
        public string EnemyName => enemyName;
        public float MaxHP => maxHP;
        public float DownSeconds => downSeconds;
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 ViewScale => viewScale;
        public TimeSpan InvincibleSecond => TimeSpan.FromSeconds(invincibleSecond);
        private Dictionary<string, GameObject> attackPrefabs;

        public float GetUP(string parameterName)
        {
            uniqueParameters ??= InitializeUniqueParameters();
            return uniqueParameters[parameterName];
        }

        private Dictionary<string, float> InitializeUniqueParameters()
        {
            var parameters = new Dictionary<string, float>();
            for (int i = 0; i < uniqueParameterList.Count; i++)
                parameters.Add(uniqueParameterList[i].ParameterName, uniqueParameterList[i].Value);
            return parameters;
        }

        public GameObject GetAttackPrefab(string attackName)
        {
            attackPrefabs ??= InitializeAttackPrefabs();
            return attackPrefabs[attackName];
        }

        private Dictionary<string, GameObject> InitializeAttackPrefabs()
        {
            var prefabs = new Dictionary<string, GameObject>();
            for (int i = 0; i < attackPrefabList.Count; i++)
                prefabs.Add(attackPrefabList[i].name, attackPrefabList[i]);
            return prefabs;
        }
    }
}
