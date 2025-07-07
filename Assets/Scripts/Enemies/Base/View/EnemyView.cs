using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Enemies.Base.View
{
    public class EnemyView : MonoBehaviour
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
