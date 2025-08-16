using System.Collections.Generic;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Assets.Scripts.Objects.Player.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.Datas;
using System.Linq;

namespace Assets.Scripts.GameSystems.ObjectStorage.Model
{
    public class ObjectStorageModel
    {
        private EnemyModel enemy;
        private PlayerModel player;
        private readonly List<EnemyAttackModel> enemyAttackList;
        private readonly List<EnemyAttackModel> enemyAttackBreakableList;
        private readonly List<FamiliarAttackModel> eFamiliarAttackList;
        private readonly List<FamiliarAttackModel> pFamiliarAttackList;
        private static ObjectStorageModel instance = new();
        public static ObjectStorageModel Instance => instance;

        public ObjectStorageModel()
        {
            enemy = null;
            player = null;
            enemyAttackList = new();
            enemyAttackBreakableList = new();
            eFamiliarAttackList = new();
            pFamiliarAttackList = new();
            instance = this;
        }

        public void AddEnemy(EnemyModel enemy) => this.enemy = enemy;
        public void RemoveEnemy() => enemy = null;
        public void AddPlayer(PlayerModel player) => this.player = player;
        public void RemovePlayer() => player = null;
        public void AddEnemyAttack(EnemyAttackModel enemyAttack, bool isBreakable)
        {
            if(isBreakable)
                enemyAttackBreakableList.Add(enemyAttack);
            else
                enemyAttackList.Add(enemyAttack);
        }

        public void RemoveEnemyAttack(EnemyAttackModel enemyAttack)
        {
            if (enemyAttackBreakableList.Contains(enemyAttack))
                enemyAttackBreakableList.Remove(enemyAttack);
            else if (enemyAttackList.Contains(enemyAttack))
                enemyAttackList.Remove(enemyAttack);
        }

        public void AddFamiliarAttack(FamiliarAttackModel familiarAttack, bool isEnemy)
        {
            if (isEnemy)
                eFamiliarAttackList.Add(familiarAttack);
            else
                pFamiliarAttackList.Add(familiarAttack);
        }

        public void RemoveFamiliarAttack(FamiliarAttackModel familiarAttack)
        {
            if (eFamiliarAttackList.Contains(familiarAttack))
                eFamiliarAttackList.Remove(familiarAttack);
            else if (pFamiliarAttackList.Contains(familiarAttack))
                pFamiliarAttackList.Remove(familiarAttack);
        }

        public void Clear()
        {
            enemy?.Destroy();
            player?.Destroy();
            for (int i = enemyAttackList.Count - 1; i >= 0; i--)
                enemyAttackList[i].Destroy();
            for (int i = eFamiliarAttackList.Count - 1; i >= 0; i--)
                eFamiliarAttackList[i].Destroy();
            for (int i = pFamiliarAttackList.Count - 1; i >= 0; i--)
                pFamiliarAttackList[i].Destroy();
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
            for (int i = 0; i < pFamiliarAttackList.Count; i++)
                if (ObjectHitDetector.IsAttacking(pFamiliarAttackList[i].HitBox, enemy.HurtBox) && pFamiliarAttackList[i].ColorName != ColorName.blue)
                    enemy.TakeDamage(pFamiliarAttackList[i].Power);
        }

        private void DetectHitEAToP()
        {
            if (!DoesPlayerExist())
                return;
            for (int i = 0; i < enemyAttackList.Count; i++)
                if (ObjectHitDetector.IsAttacking(enemyAttackList[i].HitBox, player.HurtBox))
                    player.TakeDamage(enemyAttackList[i].Power);
        }

        private void DetectHitEFAToP()
        {
            if (!DoesPlayerExist())
                return;
            for (int i = 0; i < eFamiliarAttackList.Count; i++)
                if (ObjectHitDetector.IsAttacking(eFamiliarAttackList[i].HitBox, player.HurtBox))
                    player.TakeDamage(eFamiliarAttackList[i].Power);
        }

        private void DetectHitPFAToEA()
        {
            for (int i = 0; i < pFamiliarAttackList.Count; i++)
                for (int j = 0; j < enemyAttackBreakableList.Count; j++)
                    if (ObjectHitDetector.IsAttacking(pFamiliarAttackList[i].HitBox, enemyAttackBreakableList[j].HurtBox))
                        enemyAttackBreakableList[j].TakeDamage(pFamiliarAttackList[i].Power);
        }

        public Vector2 GetHostilePos(Vector2 pos, bool isEnemy)
        {
            if (isEnemy && DoesPlayerExist())
                return player.Pos;
            else if (!isEnemy && DoesEnemyExist())
                return enemy.Pos;
            return pos;
        }

        public Vector2 GetPlayerPos(Vector2 pos) => DoesPlayerExist() ? player.Pos : pos;

        public Vector2 GetEnemyPos(Vector2 pos) => DoesEnemyExist() ? enemy.Pos : pos;

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

            for (int i = 0; i < enemyAttackList.Count; i++)
                if (ObjectHitDetector.IsAttacking(enemyAttackList[i].HitBox, player.HurtBox))
                    return true;
            for (int i = 0; i < eFamiliarAttackList.Count; i++)
                if (ObjectHitDetector.IsAttacking(eFamiliarAttackList[i].HitBox, player.HurtBox))
                    return true;

            return false;
        }

        public void TakeDamagePlayer()
        {
            if (!DoesPlayerExist())
                return;

            for (int i = 0; i < enemyAttackList.Count; i++)
                if (ObjectHitDetector.IsAttacking(enemyAttackList[i].HitBox, player.HurtBox))
                {
                    player.TakeDamage(enemyAttackList[i].Power);
                    return;
                }
            for (int i = 0; i < eFamiliarAttackList.Count; i++)
                if (ObjectHitDetector.IsAttacking(eFamiliarAttackList[i].HitBox, player.HurtBox))
                {
                    player.TakeDamage(eFamiliarAttackList[i].Power);
                    return;
                }
        }
    }
}
