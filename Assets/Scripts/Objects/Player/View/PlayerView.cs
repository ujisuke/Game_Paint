using UnityEngine;
using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using System;
using Assets.Scripts.Objects.Common.Model.View;
using Assets.Scripts.UI.PlayerStatus.View;

namespace Assets.Scripts.Objects.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [NonSerialized] public ColorDataList ColorDataList;
        [SerializeField] private Animator animator;
        private ObjectAnimation objectAnimation;
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
            PlayerStatusView.Instance?.SetColor(colorNameInput);
        }

        public void PlayAnim(string animName, float animSeconds)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.Play(animName, animSeconds);
        }

        public void FlipX(bool isLeft)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.FlipX(isLeft);
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
