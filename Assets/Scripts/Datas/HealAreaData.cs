using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "HealAreaData", menuName = "ScriptableObjects/HealAreaData")]
    public class HealAreaData : ScriptableObject
    {
        [SerializeField] private Vector2 hitBoxScale;
        [SerializeField] private int healAmount;
        public Vector2 HitBoxScale => hitBoxScale;
        public int HealAmount => healAmount;
    }
}
