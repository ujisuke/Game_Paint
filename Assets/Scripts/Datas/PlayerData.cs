using System;
using Assets.Scripts.Objects.Common;
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
        [SerializeField] private float invincibleSecond;
        [SerializeField] private float moveSpeed;
        public HP MaxHP => new(maxHP);
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 ViewScale => viewScale;
        public Vector2 Scale => scale;
        public TimeSpan InvincibleSecond => TimeSpan.FromSeconds(invincibleSecond);
        public float MoveSpeed => moveSpeed;
    }
}
