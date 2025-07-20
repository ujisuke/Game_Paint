using Assets.Scripts.Objects.Common;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.View
{
    public class FamiliarAttackView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetPA(PA pA) => transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));
    }
}
