using UnityEngine;
using Assets.Scripts.Objects.Common;

namespace Assets.Scripts.Objects.Enemies.Base.View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetPA(PA pA) => transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));
    }
}
