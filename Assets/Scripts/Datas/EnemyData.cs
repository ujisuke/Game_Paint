using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string enemyName;
        [SerializeField] private int maxHP;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 scale;
        [SerializeField] private float invincibleSecond;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        [SerializeField] private GameObject attackPrefab;
        private Dictionary<string, float> uniqueParameters;
        public string EnemyName => enemyName;
        public HP MaxHP => new(maxHP);
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 Scale => scale;
        public TimeSpan InvincibleSecond => TimeSpan.FromSeconds(invincibleSecond);
        public GameObject AttackPrefab => attackPrefab;


        public float GetUP(string parameterName)
        {
            uniqueParameters ??= InitializeUniqueParameters();
            try
            { return uniqueParameters[parameterName]; }
            catch (KeyNotFoundException)
            { return 0f; }
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
