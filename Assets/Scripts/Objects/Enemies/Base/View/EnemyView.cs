using UnityEngine;
using Assets.Scripts.Objects.Common;

namespace Assets.Scripts.Objects.Enemies.Base.View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        private EnemyAnimation enemyAnimation;
        [SerializeField] private GameObject hurtBoxPrefab;
        private GameObject hurtBoxObject;

        public void SetPA(PA pA) => transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));

        public void SetViewScale(Vector2 viewScale) => transform.localScale = viewScale;

        public void PlayAnim(string animName, float animSeconds)
        {
            enemyAnimation ??= new EnemyAnimation(animator, spriteRenderer);
            enemyAnimation.Play(animName, animSeconds);
        }

        public void FlipX(bool isLeft)
        {
            enemyAnimation ??= new EnemyAnimation(animator, spriteRenderer);
            enemyAnimation.FlipX(isLeft);
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
