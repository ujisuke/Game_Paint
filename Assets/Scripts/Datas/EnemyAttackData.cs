using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "EnemyAttackData", menuName = "ScriptableObjects/EnemyAttackData")]
    public class EnemyAttackData : ScriptableObject
    {
        [SerializeField] private int power;
        [SerializeField] private Vector2 hitBoxScale;
        [SerializeField] private Vector2 viewScale;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        private Dictionary<string, float> uniqueParameters;
        public int Power => power;
        public Vector2 HitBoxScale => hitBoxScale;
        public Vector2 ViewScale => viewScale;

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
