using Assets.Scripts.Objects.Common;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.View
{
    public class FamiliarAttackView : MonoBehaviour
    {
        [SerializeField] private GameObject hitBoxPrefab;
        private GameObject hitBoxObject;

        public void SetPA(PA pA) => transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));

        public void SetViewScale(Vector2 viewScale) => transform.localScale = viewScale;

        public void InstantiateHitBox(HitBox hitBox)
        {
            hitBoxObject = Instantiate(hitBoxPrefab, hitBox.Pos, Quaternion.identity);
            hitBoxObject.transform.localScale = hitBox.Size;
        }

        public void SetPHitBox(HitBox hitBox)
        {
            hitBoxObject.transform.position = hitBox.Pos;
        }

        public void OnDestroy()
        {
            Destroy(hitBoxObject);
        }
    }
}
