using UnityEngine;

namespace Assets.Scripts.Objects.Player.View
{
    public class PlayerAnimation
    {
        private readonly Animator animator;
        private readonly SpriteRenderer spriteRenderer;

        public PlayerAnimation(Animator animator, SpriteRenderer spriteRenderer)
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
