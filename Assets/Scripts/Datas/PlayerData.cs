using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int maxHP;
        [SerializeField] private float reduceInkPerSecond;
        [SerializeField] private float addInkPerSecond;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 viewScale;
        [SerializeField] private Vector2 scale;
        [SerializeField] private float downSeconds;
        [SerializeField] private float invincibleSeconds;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float reloadInkSeconds;
        public int MaxHP => maxHP;
        public float ReduceInkPerSecond => reduceInkPerSecond;
        public float AddInkPerSecond => addInkPerSecond;
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 ViewScale => viewScale;
        public Vector2 Scale => scale;
        public float DownSeconds => downSeconds;
        public float InvincibleSeconds => invincibleSeconds;
        public float MoveSpeed => moveSpeed;
        public float ReloadInkSeconds => reloadInkSeconds;
    }
}
