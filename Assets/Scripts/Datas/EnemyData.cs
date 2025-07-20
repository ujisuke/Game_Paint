using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string enemyName;
        [SerializeField] private int maxHP;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private float invincibleSecond;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        [SerializeField] private GameObject attackPrefab;
        private Dictionary<string, float> uniqueParameters;
        public string EnemyName => enemyName;
        public int MaxHP => maxHP;
        public Vector2 HurtBoxScale => hurtBoxScale;
        public TimeSpan InvincibleSecond => TimeSpan.FromSeconds(invincibleSecond);
        public GameObject AttackPrefab => attackPrefab;


        public float GetUP(string parameterName)
        {
            uniqueParameters ??= InitializeUniqueParameters();
            return uniqueParameters[parameterName];
        }

        private Dictionary<string, float> InitializeUniqueParameters()
        {
            var parameters = new Dictionary<string, float>();
            for(int i = 0; i < uniqueParametersList.Count; i++)
                parameters.Add(uniqueParametersList[i].ParameterName, uniqueParametersList[i].Value);
            return parameters;
        }
    }
}
