using UnityEngine;

namespace Assets.Scripts.Objects.Common.Model.View
{
    public class ObjectAnimation
    {
        private readonly Animator animator;
        private readonly SpriteRenderer spriteRenderer;

        public ObjectAnimation(Animator animator, SpriteRenderer spriteRenderer)
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