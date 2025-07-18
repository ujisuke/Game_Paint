using System.Collections.Generic;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.Familiars.Base.Model;
using Assets.Scripts.Objects.ObjectAttacks.Base.Model;
using Assets.Scripts.Objects.Player.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameSystems.ObjectsStorage.Model
{
    public class ObjectsStorageModel
    {
        private readonly List<EnemyModel> enemies;
        private PlayerModel player;
        private readonly List<FamiliarModel> familiars;
        private readonly List<ObjectAttackModel> enemyAttacks;
        private readonly List<ObjectAttackModel> familiarAttacks;
        private static ObjectsStorageModel instance = new();
        public static ObjectsStorageModel Instance => instance;

        public ObjectsStorageModel()
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
            DetectHitFToE();
            DetectHitEToF();
            DetectHitEToP();
        }

        private void DetectHitFToE()
        {
            for (int i = 0; i < familiarAttacks.Count; i++)
                for (int j = 0; j < enemies.Count; j++)
                    if (ObjectsHitDetector.IsAttacking(familiarAttacks[i].HitBox, enemies[j].HurtBox))
                        enemies[j].TakeDamage(familiarAttacks[i].PowerValue).Forget();
        }

        private void DetectHitEToF()
        {
            for (int i = 0; i < enemyAttacks.Count; i++)
                for (int j = 0; j < familiars.Count; j++)
                    if (ObjectsHitDetector.IsAttacking(enemyAttacks[i].HitBox, familiars[j].HurtBox))
                        familiars[j].TakeDamage(enemyAttacks[i].PowerValue).Forget();
        }

        private void DetectHitEToP()
        {
            if (player == null)
                return;
            for (int i = 0; i < enemyAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(enemyAttacks[i].HitBox, player.HurtBox))
                    player.TakeDamage(enemyAttacks[i].PowerValue).Forget();
        }

        public Vector2 GetNearestEnemyPos(Vector2 pos)
        {
            Vector2 nearestPos = pos;
            float minDistance = float.MaxValue;

            for (int i = 0; i < enemies.Count; i++)
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

        public bool DoesEnemyExist()
        {
            return enemies.Count > 0;
        }

        public bool DoesPlayerExist()
        {
            return player != null;
        }
    }
}
