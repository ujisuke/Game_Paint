using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ColorEffectData", menuName = "ScriptableObjects/ColorEffectData")]
    public class ColorEffectData : ScriptableObject
    {
        [SerializeField] private float powerMultiplier;
        [SerializeField] private float defenseMultiplier;
        [SerializeField] private float healRate;
        [SerializeField] private float poisonDamageRate;
        [SerializeField] private float attackSpeedMultiplier;
        public float PowerMultiplier => powerMultiplier;
        public float DefenseMultiplier => defenseMultiplier;
        public float HealRate => healRate;
        public float PoisonDamageRate => poisonDamageRate;
        public float AttackSpeedMultiplier => attackSpeedMultiplier;
    }
}
