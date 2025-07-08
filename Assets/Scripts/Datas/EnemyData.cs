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
        private Dictionary<string, float> uniqueParameters;
        public string FamiliarName => enemyName;
        public HP MaxHP => new(maxHP);
        public HurtBox HurtBox => new(Vector2.zero, hurtBoxScale, true);
        public Vector2 Scale => scale;
        public TimeSpan InvincibleSecond => TimeSpan.FromSeconds(invincibleSecond);

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
