using UnityEngine;
using Assets.Scripts.Objects.Common;

namespace Assets.Scripts.Objects.Enemies.Base.View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        private EnemyAnimation enemyAnimation;

        public void SetPA(PA pA) => transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));

        public void PlayAnim(string animName)
        {
            enemyAnimation ??= new EnemyAnimation(animator, spriteRenderer);
            enemyAnimation.Play(animName);
        }

        public void FlipX(bool isLeft)
        {
            enemyAnimation ??= new EnemyAnimation(animator, spriteRenderer);
            enemyAnimation.FlipX(isLeft);
        }
    }
}
