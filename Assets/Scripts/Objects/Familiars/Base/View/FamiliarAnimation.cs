using UnityEngine;

namespace Assets.Scripts.Objects.Familiars.Base.View
{
    public class FamiliarAnimation
    {
        private readonly Animator animator;
        private readonly SpriteRenderer spriteRenderer;

        public FamiliarAnimation(Animator animator, SpriteRenderer spriteRenderer)
        {
            this.animator = animator;
            this.spriteRenderer = spriteRenderer;
        }

        public void Play(string animName, float playSeconds)
        {
            animator.speed = 1f / playSeconds;
            animator.Play(animName);
        }

        public void FlipX(bool isLeft)
        {
            spriteRenderer.flipX = isLeft;
        }
    }
}
