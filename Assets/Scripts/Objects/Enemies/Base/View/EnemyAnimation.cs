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

        public void Play(string animName, float animSeconds)
        {
            animator.Play(animName);
            if (animSeconds > 0f)
                animator.speed = 1f / animSeconds;
        }

        public void FlipX(bool isLeft)
        {
            spriteRenderer.flipX = isLeft;
        }
    }
}