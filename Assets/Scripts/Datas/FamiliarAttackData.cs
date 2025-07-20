using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "FamiliarAttackData", menuName = "ScriptableObjects/FamiliarAttackData")]
    public class FamiliarAttackData : ScriptableObject
    {
        [SerializeField] private int power;
        [SerializeField] private Vector2 hitBoxScale;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        [SerializeField] private float defendDecreaseSeconds;
        [SerializeField] private float poisonSeconds;
        [SerializeField] private float healValue;
        [SerializeField] private float attackSpeedDecreaseSeconds;
        private Dictionary<string, float> uniqueParameters;
        public int Power => power;
        public Vector2 HitBoxScale => hitBoxScale;
        public float DefendDecreaseSeconds => defendDecreaseSeconds;
        public float PoisonSeconds => poisonSeconds;
        public float HealValue => healValue;
        public float AttackSpeedDecreaseSeconds => attackSpeedDecreaseSeconds;

        public float GetUniqueParameter(string parameterName)
        {
            uniqueParameters ??= InitializeUniqueParameters();
            return uniqueParameters[parameterName];
        }

        private Dictionary<string, float> InitializeUniqueParameters()
        {
            var parameters = new Dictionary<string, float>();
            for (int i = 0; i < uniqueParametersList.Count; i++)
                parameters.Add(uniqueParametersList[i].ParameterName, uniqueParametersList[i].Value);
            return parameters;
        }
    }
}
