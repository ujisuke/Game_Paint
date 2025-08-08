using System.Collections.Generic;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Assets.Scripts.Objects.Player.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Common.Model;

namespace Assets.Scripts.GameSystems.ObjectsStorage.Model
{
    public class ObjectsStorageModel
    {
        private EnemyModel enemy;
        private PlayerModel player;
        private readonly List<EnemyAttackModel> enemyAttacks;
        private readonly List<FamiliarAttackModel> eFamiliarAttacks;
        private readonly List<FamiliarAttackModel> pFamiliarAttacks;
        private static ObjectsStorageModel instance = new();
        public static ObjectsStorageModel Instance => instance;

        public ObjectsStorageModel()
        {
            enemy = null;
            player = null;
            enemyAttacks = new();
            eFamiliarAttacks = new();
            pFamiliarAttacks = new();
            instance = this;
        }

        public void AddEnemy(EnemyModel enemy) => this.enemy = enemy;
        public void RemoveEnemy() => enemy = null;
        public void AddPlayer(PlayerModel player) => this.player = player;
        public void RemovePlayer() => player = null;
        public void AddEnemyAttack(EnemyAttackModel enemyAttack) => enemyAttacks.Add(enemyAttack);
        public void RemoveEnemyAttack(EnemyAttackModel enemyAttack) => enemyAttacks.Remove(enemyAttack);

        public void AddFamiliarAttack(FamiliarAttackModel familiarAttack, bool isEnemy)
        {
            if (isEnemy)
                eFamiliarAttacks.Add(familiarAttack);
            else
                pFamiliarAttacks.Add(familiarAttack);
        }

        public void RemoveFamiliarAttack(FamiliarAttackModel familiarAttack)
        {
            if (eFamiliarAttacks.Contains(familiarAttack))
                eFamiliarAttacks.Remove(familiarAttack);
            else if (pFamiliarAttacks.Contains(familiarAttack))
                pFamiliarAttacks.Remove(familiarAttack);
        }

        public void DetectHit()
        {
            DetectHitPFAToE();
            DetectHitEAToP();
            DetectHitEFAToP();
            DetectHitPFAToEA();
        }

        private void DetectHitPFAToE()
        {
            if (!DoesEnemyExist())
                return;
            for (int i = 0; i < pFamiliarAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(pFamiliarAttacks[i].HitBox, enemy.HurtBox) && pFamiliarAttacks[i].ColorName != ColorName.blue)
                    enemy.TakeDamage(pFamiliarAttacks[i].Power).Forget();
        }

        private void DetectHitEAToP()
        {
            if (!DoesPlayerExist())
                return;
            for (int i = 0; i < enemyAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(enemyAttacks[i].HitBox, player.HurtBox))
                    player.TakeDamage(enemyAttacks[i].Power);
        }

        private void DetectHitEFAToP()
        {
            if (!DoesPlayerExist())
                return;
            for (int i = 0; i < eFamiliarAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(eFamiliarAttacks[i].HitBox, player.HurtBox))
                    player.TakeDamage(eFamiliarAttacks[i].Power);
        }

        private void DetectHitPFAToEA()
        {
            for (int i = 0; i < pFamiliarAttacks.Count; i++)
                for (int j = 0; j < enemyAttacks.Count; j++)
                    if (ObjectsHitDetector.IsHitting(pFamiliarAttacks[i].HitBox, enemyAttacks[j].HitBox) && pFamiliarAttacks[i].ColorName == ColorName.blue)
                        enemyAttacks[j].Break(pFamiliarAttacks[i]);
        }

        public Vector2 GetHostilePos(Vector2 pos, bool isEnemy)
        {
            if (isEnemy && DoesPlayerExist())
                return player.PA.Pos;
            else if (!isEnemy && DoesEnemyExist())
                return enemy.PA.Pos;
            return pos;
        }

        public bool DoesEnemyExist()
        {
            return enemy != null;
        }

        public bool DoesPlayerExist()
        {
            return player != null;
        }

        public bool IsPlayerTakingDamage()
        {
            if (!DoesPlayerExist())
                return false;

            for (int i = 0; i < enemyAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(enemyAttacks[i].HitBox, player.HurtBox))
                    return true;
            for (int i = 0; i < eFamiliarAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(eFamiliarAttacks[i].HitBox, player.HurtBox))
                    return true;

            return false;
        }

        public void TakeDamagePlayer()
        {
            if (!DoesPlayerExist())
                return;

            for (int i = 0; i < enemyAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(enemyAttacks[i].HitBox, player.HurtBox))
                {
                    player.TakeDamage(enemyAttacks[i].Power);
                    return;
                }
            for (int i = 0; i < eFamiliarAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(eFamiliarAttacks[i].HitBox, player.HurtBox))
                {
                    player.TakeDamage(eFamiliarAttacks[i].Power);
                    return;
                }
        }
    }
}
