using UnityEngine;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using System;

namespace Assets.Scripts.Objects.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [NonSerialized] public ColorDataList ColorDataList;
        [SerializeField] private Animator animator;
        private PlayerAnimation playerAnimation;
        [SerializeField] private GameObject hurtBoxPrefab;
        private GameObject hurtBoxObject;


        public void SetPA(PA pA)
        {
            transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));
        }

        public void SetViewScale(Vector2 viewScale) => transform.localScale = viewScale;

        public void SetColor(ColorName colorNameInput)
        {
            spriteRenderer.color = ColorDataList.GetColor(colorNameInput);
            PlayerColorIndicator.Instance?.SetColor(colorNameInput);
        }

        public void PlayAnim(string animName)
        {
            playerAnimation ??= new PlayerAnimation(animator, spriteRenderer);
            playerAnimation.Play(animName);
        }

        public void FlipX(bool isLeft)
        {
            playerAnimation ??= new PlayerAnimation(animator, spriteRenderer);
            playerAnimation.FlipX(isLeft);
        }

        public void InstantiateHurtBox(HurtBox hurtBox)
        {
            hurtBoxObject = Instantiate(hurtBoxPrefab, hurtBox.Pos, Quaternion.identity);
            hurtBoxObject.transform.localScale = hurtBox.Size;
        }

        public void SetPHurtBox(HurtBox hurtBox)
        {
            hurtBoxObject.transform.position = hurtBox.Pos;
        }

        public void OnDestroy()
        {
            Destroy(hurtBoxObject);
        }
    }
}
