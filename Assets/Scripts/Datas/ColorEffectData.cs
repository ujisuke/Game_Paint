using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ColorEffectData", menuName = "ScriptableObjects/ColorEffectData")]
    public class ColorEffectData : ScriptableObject
    {
        [SerializeField] private float powerMultiplier;
        public float PowerMultiplier => powerMultiplier;
    }
}
