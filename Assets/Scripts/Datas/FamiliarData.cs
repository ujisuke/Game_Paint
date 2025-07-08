using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.ObjectAttacks.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "FamiliarData", menuName = "ScriptableObjects/FamiliarData")]
    public class FamiliarData : ScriptableObject
    {
        [SerializeField] private string familiarName;
        [SerializeField] private int defaultPower;
        [SerializeField] private int maxHP;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 scale;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        [SerializeField] private GameObject attackPrefab;
        private Dictionary<string, float> uniqueParameters;

        public string FamiliarName => familiarName;
        public Power DefaultPower => new(defaultPower);
        public HP MaxHP => new(maxHP);
        public HurtBox HurtBox => new(Vector2.zero, hurtBoxScale, false);
        public Vector2 Scale => scale;
        public GameObject AttackPrefab => attackPrefab;

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
