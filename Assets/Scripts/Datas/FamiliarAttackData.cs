using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "FamiliarAttackData", menuName = "ScriptableObjects/FamiliarAttackData")]
    public class FamiliarAttackData : ScriptableObject
    {
        [SerializeField] private int power;
        [SerializeField] private Vector2 hitBoxScale;
        [SerializeField] private float defenseDecreaseSeconds;
        [SerializeField] private float poisonSeconds;
        [SerializeField] private float attackSpeedDecreaseSeconds;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        private Dictionary<string, float> uniqueParameters;
        public int Power => power;
        public Vector2 HitBoxScale => hitBoxScale;
        public float DefenseDecreaseSeconds => defenseDecreaseSeconds;
        public float PoisonSeconds => poisonSeconds;
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
