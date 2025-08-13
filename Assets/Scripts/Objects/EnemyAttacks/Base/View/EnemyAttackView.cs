using UnityEngine;
using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Objects.Common.Model.View;
using Assets.Scripts.Datas;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.View
{
    public class EnemyAttackView : MonoBehaviour
    {
        [SerializeField] private GameObject hitBoxPrefab;
        private GameObject hitBoxObject;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        private ObjectAnimation objectAnimation;

        public void SetPA(Vector2 pos, float angle) => transform.SetPositionAndRotation(pos, Quaternion.Euler(0f, 0f, angle));

        public void SetViewScale(Vector2 viewScale) => transform.localScale = viewScale;

        public void InstantiateHitBox(HitBox hitBox)
        {
            hitBoxObject = Instantiate(hitBoxPrefab, hitBox.Pos, Quaternion.identity);
            hitBoxObject.transform.localScale = hitBox.Size;
            hitBoxObject.SetActive(hitBox.IsActive);
        }

        public void SetPHitBox(HitBox hitBox)
        {
            hitBoxObject.transform.position = hitBox.Pos;
            hitBoxObject.SetActive(hitBox.IsActive);
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

        public void OnDestroy()
        {
            Destroy(hitBoxObject);
        }

        public void OnBreak()
        {
            spriteRenderer.color = ColorDataList.Instance.GetColor(ColorName.blue);
            objectAnimation.Stop();
        }
    }
}
