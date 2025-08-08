using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int maxHP;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 viewScale;
        [SerializeField] private Vector2 scale;
        [SerializeField] private float downSeconds;
        [SerializeField] private float invincibleSeconds;
        [SerializeField] private float moveSpeed;
        public int MaxHP => maxHP;
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 ViewScale => viewScale;
        public Vector2 Scale => scale;
        public float DownSeconds => downSeconds;
        public float InvincibleSeconds => invincibleSeconds;
        public float MoveSpeed => moveSpeed;
    }
}
