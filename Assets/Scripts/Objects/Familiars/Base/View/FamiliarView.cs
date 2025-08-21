using UnityEngine;
using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Common.Model.View;

namespace Assets.Scripts.Objects.Familiars.Base.View
{
    public class FamiliarView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ColorDataList colorDataList;
        [SerializeField] private Animator animator;
        private ObjectAnimation objectAnimation;

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
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.Play(animName, playSeconds);
        }

        public void FlipX(bool isLeft)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.FlipX(isLeft);
        }
    }
}
