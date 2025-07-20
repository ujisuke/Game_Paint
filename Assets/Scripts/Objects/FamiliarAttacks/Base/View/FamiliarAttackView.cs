using Assets.Scripts.Objects.Common;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.View
{
    public class FamiliarAttackView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetPSA(PSA pSA)
        {
            transform.position = pSA.Pos;
            transform.localScale = pSA.Scale;
            transform.rotation = Quaternion.Euler(0f, 0f, pSA.Angle);
        }
    }
}
