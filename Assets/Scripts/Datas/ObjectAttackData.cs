using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ObjectAttackData", menuName = "ScriptableObjects/ObjectAttackData")]
    public class ObjectAttackData : ScriptableObject
    {
        [SerializeField] private int defaultPower;
        public Power DefaultPower => new(defaultPower);
        [SerializeField] private Vector2 hitBoxScale;
        [SerializeField] private List<UniqueParameter> uniqueParametersList;
        private Dictionary<string, float> uniqueParameters;
        public Vector2 HitBoxScale => hitBoxScale;
        [SerializeField] private bool isEnemyAttack;
        public bool IsEnemyAttack => isEnemyAttack;

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
            for (int i = 0; i < uniqueParametersList.Count; i++)
                parameters.Add(uniqueParametersList[i].ParameterName, uniqueParametersList[i].Value);
            return parameters;
        }
    }
}
