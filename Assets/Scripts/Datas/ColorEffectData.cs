using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ColorEffectData", menuName = "ScriptableObjects/ColorEffectData")]
    public class ColorEffectData : ScriptableObject
    {
        [SerializeField] private float powerMultiplier;
        [SerializeField] private float defenseMultiplier;
        [SerializeField] private float poisonDamageRate;
        [SerializeField] private Vector2 healAreaScale;
        [SerializeField] private float attackSpeedMultiplier;
        public float PowerMultiplier => powerMultiplier;
        public float DefenseMultiplier => defenseMultiplier;
        public float PoisonDamageRate => poisonDamageRate;
        public Vector2 HealAreaScale => healAreaScale;
        public float AttackSpeedMultiplier => attackSpeedMultiplier;
    }
}
