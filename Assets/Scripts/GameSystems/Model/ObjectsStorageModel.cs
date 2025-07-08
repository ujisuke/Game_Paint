using System.Collections.Generic;
using Assets.Scripts.Enemies.Base.Model;
using Assets.Scripts.Familiars.Base.Model;
using Assets.Scripts.ObjectAttacks.Base.Model;
using Assets.Scripts.Player.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameSystems.Model
{
    public class ObjectStorageModel
    {
        private List<EnemyModel> enemies;
        private PlayerModel player;
        private List<FamiliarModel> familiars;
        private List<ObjectAttackModel> enemyAttacks;
        private List<ObjectAttackModel> familiarAttacks;
        private static ObjectStorageModel instance = new();
        public static ObjectStorageModel Instance => instance;

        public ObjectStorageModel()
        {
            enemies = new List<EnemyModel>();
            player = null;
            familiars = new List<FamiliarModel>();
            enemyAttacks = new List<ObjectAttackModel>();
            familiarAttacks = new List<ObjectAttackModel>();
            instance = this;
        }

        public void AddEnemy(EnemyModel enemy) => enemies.Add(enemy);
        public void RemoveEnemy(EnemyModel enemy) => enemies.Remove(enemy);
        public void AddPlayer(PlayerModel player) => this.player = player;
        public void RemovePlayer(PlayerModel player) => this.player = null;
        public void AddFamiliar(FamiliarModel familiar) => familiars.Add(familiar);
        public void RemoveFamiliar(FamiliarModel familiar) => familiars.Remove(familiar);

        public void AddObjectAttack(ObjectAttackModel objectAttack, bool isEnemyAttack)
        {
            if (isEnemyAttack)
                enemyAttacks.Add(objectAttack);
            else
                familiarAttacks.Add(objectAttack);
        }

        public void RemoveObjectAttack(ObjectAttackModel objectAttack)
        {
            if (enemyAttacks.Contains(objectAttack))
                enemyAttacks.Remove(objectAttack);
            else if (familiarAttacks.Contains(objectAttack))
                familiarAttacks.Remove(objectAttack);
        }

        public void DetectHit()
        {
            for (int i = 0; i < familiarAttacks.Count; i++)
                for (int j = 0; j < enemies.Count; j++)
                    if (ObjectsHitDetector.IsAttacking(familiarAttacks[i].HitBox, enemies[j].HurtBox))
                        enemies[j].TakeDamage(familiarAttacks[i].PowerValue).Forget();
        }

        public Vector2 GetNearestEnemyPos(Vector2 pos)
        {
            Vector2 nearestPos = pos;
            float minDistance = float.MaxValue;

            for(int i = 0; i < enemies.Count; i++)
            {
                float distance = Vector2.Distance(pos, enemies[i].PSA.Pos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPos = enemies[i].PSA.Pos;
                }
            }

            return nearestPos;
        }
    }
}
