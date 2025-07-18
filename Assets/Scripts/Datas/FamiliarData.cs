using System;
using System.Collections.Generic;
using Assets.Scripts.Objects.Common;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "FamiliarData", menuName = "ScriptableObjects/FamiliarData")]
    public class FamiliarData : ScriptableObject
    {
        [SerializeField] private string familiarName;
        [SerializeField] private int maxHP;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 scale;
        [SerializeField] private float invincibleSecond;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        [SerializeField] private GameObject attackPrefab;
        private Dictionary<string, float> uniqueParameters;

        public string FamiliarName => familiarName;
        public HP MaxHP => new(maxHP);
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 Scale => scale;
        public GameObject AttackPrefab => attackPrefab;
        public TimeSpan InvincibleSecond => TimeSpan.FromSeconds(invincibleSecond);

        public float GetUniqueParameter(string parameterName)
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
