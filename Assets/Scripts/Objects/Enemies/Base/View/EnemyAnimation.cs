using UnityEngine;

namespace Assets.Scripts.Objects.Enemies.Base.View
{
    public class EnemyAnimation
    {
        private readonly Animator animator;
        private readonly SpriteRenderer spriteRenderer;

        public EnemyAnimation(Animator animator, SpriteRenderer spriteRenderer)
        {
            this.animator = animator;
            this.spriteRenderer = spriteRenderer;
        }

        public void Play(string animName)
        {
            animator.Play(animName);
        }

        public void FlipX(bool isLeft)
        {
            spriteRenderer.flipX = isLeft;
        }
    }
}