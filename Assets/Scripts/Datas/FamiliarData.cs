using Assets.Scripts.CommonObject.Model;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "FamiliarData", menuName = "ScriptableObjects/FamiliarData")]
    public class FamiliarData : ScriptableObject
    {
        [SerializeField] private string familiarName;
        [SerializeField] private int defaultPower;
        [SerializeField] private int maxHP;
        [SerializeField] private Vector2 hitBoxSize;

        public string FamiliarName => familiarName;
        public Power DefaultPower => new(defaultPower);
        public HP MaxHP => new(maxHP);
        public HitBox HitBox => new(hitBoxSize);
    }
}
