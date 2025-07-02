using System;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class UniqueParameter
    {
        [SerializeField] private string parameterName;
        public string ParameterName => parameterName;
        [SerializeField] private float value;
        public float Value => value;
    }
}
