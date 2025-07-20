using UnityEngine;
using Assets.Scripts.Objects.Common;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.View
{
    public class EnemyAttackView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetPAS(PA pA, Vector2 hitBoxScale)
        {
            transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));
            transform.localScale = hitBoxScale;
        }
    }
}
