using UnityEngine;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;

namespace Assets.Scripts.Objects.Familiars.Base.View
{
    public class FamiliarView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ColorDataList colorDataList;
        [SerializeField] private Animator animator;
        private FamiliarAnimation familiarAnimation;

        public void SetPA(PA pA)
        {
            transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));
        }

        public void SetViewScale(Vector2 viewScale)
        {
            transform.localScale = viewScale;
        }

        public void SetColor(ColorName colorNameInput)
        {
            spriteRenderer.color = colorDataList.GetColor(colorNameInput);
        }

        public void PlayAnim(string animName, float playSeconds)
        {
            familiarAnimation ??= new FamiliarAnimation(animator, spriteRenderer);
            familiarAnimation.Play(animName, playSeconds);
        }

        public void FlipX(bool isLeft)
        {
            familiarAnimation ??= new FamiliarAnimation(animator, spriteRenderer);
            familiarAnimation.FlipX(isLeft);
        }
    }
}
