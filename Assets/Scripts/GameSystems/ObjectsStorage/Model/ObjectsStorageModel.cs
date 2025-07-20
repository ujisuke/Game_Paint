using System.Collections.Generic;
using Assets.Scripts.Objects.Enemies.Base.Model;
using Assets.Scripts.Objects.Familiars.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Assets.Scripts.Objects.Player.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Assets.Scripts.Objects.HealArea.Model;

namespace Assets.Scripts.GameSystems.ObjectsStorage.Model
{
    public class ObjectsStorageModel
    {
        private readonly List<EnemyModel> enemies;
        private PlayerModel player;
        private readonly List<FamiliarModel> eFamiliars;
        private readonly List<FamiliarModel> pFamiliars;
        private readonly List<EnemyAttackModel> enemyAttacks;
        private readonly List<FamiliarAttackModel> eFamiliarAttacks;
        private readonly List<FamiliarAttackModel> pFamiliarAttacks;
        private readonly List<HealAreaModel> healAreas;
        private static ObjectsStorageModel instance = new();
        public static ObjectsStorageModel Instance => instance;

        public ObjectsStorageModel()
        {
            enemies = new();
            player = null;
            eFamiliars = new();
            pFamiliars = new();
            enemyAttacks = new();
            eFamiliarAttacks = new();
            pFamiliarAttacks = new();
            healAreas = new();
            instance = this;
        }

        public void AddEnemy(EnemyModel enemy) => enemies.Add(enemy);
        public void RemoveEnemy(EnemyModel enemy) => enemies.Remove(enemy);
        public void AddPlayer(PlayerModel player) => this.player = player;
        public void RemovePlayer() => player = null;
        public void AddEnemyAttack(EnemyAttackModel enemyAttack) => enemyAttacks.Add(enemyAttack);
        public void RemoveEnemyAttack(EnemyAttackModel enemyAttack) => enemyAttacks.Remove(enemyAttack);
        public void AddHealArea(HealAreaModel healArea) => healAreas.Add(healArea);
        public void RemoveHealArea(HealAreaModel healArea) => healAreas.Remove(healArea);

        public void AddFamiliar(FamiliarModel familiar, bool isEnemy)
        {
            if (isEnemy)
                eFamiliars.Add(familiar);
            else
                pFamiliars.Add(familiar);
        }

        public void RemoveFamiliar(FamiliarModel familiar)
        {
            if (eFamiliars.Contains(familiar))
                eFamiliars.Remove(familiar);
            else if (pFamiliars.Contains(familiar))
                pFamiliars.Remove(familiar);
        }

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
            DetectHitPFAToEF();
            DetectHitEAToPF();
            DetectHitEAToP();
            DetectHitEFAToPF();
            DetectHitEFAToP();
            DetectHitPFAToEA();
            DetectHitPToHA();
        }

        private void DetectHitPFAToE()
        {
            for (int i = 0; i < pFamiliarAttacks.Count; i++)
                for (int j = 0; j < enemies.Count; j++)
                    if (ObjectsHitDetector.IsAttacking(pFamiliarAttacks[i].HitBox, enemies[j].HurtBox))
                        enemies[j].TakeDamageFromFamiliar(pFamiliarAttacks[i]).Forget();
        }

        private void DetectHitPFAToEF()
        {
            for (int i = 0; i < pFamiliarAttacks.Count; i++)
                for (int j = 0; j < eFamiliars.Count; j++)
                    if (ObjectsHitDetector.IsAttacking(pFamiliarAttacks[i].HitBox, eFamiliars[j].HurtBox))
                        eFamiliars[j].TakeDamageFromFamiliar(pFamiliarAttacks[i]).Forget();
        }

        private void DetectHitEAToPF()
        {
            for (int i = 0; i < enemyAttacks.Count; i++)
                for (int j = 0; j < pFamiliars.Count; j++)
                    if (ObjectsHitDetector.IsAttacking(enemyAttacks[i].HitBox, pFamiliars[j].HurtBox))
                        pFamiliars[j].TakeDamageFromEnemy(enemyAttacks[i].Power).Forget();
        }

        private void DetectHitEAToP()
        {
            if (player == null)
                return;
            for (int i = 0; i < enemyAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(enemyAttacks[i].HitBox, player.HurtBox))
                    player.TakeDamage(enemyAttacks[i].Power).Forget();
        }

        private void DetectHitEFAToPF()
        {
            for (int i = 0; i < eFamiliarAttacks.Count; i++)
                for (int j = 0; j < pFamiliars.Count; j++)
                    if (ObjectsHitDetector.IsAttacking(eFamiliarAttacks[i].HitBox, pFamiliars[j].HurtBox))
                        pFamiliars[j].TakeDamageFromEnemy(eFamiliarAttacks[i].Power).Forget();
        }

        private void DetectHitEFAToP()
        {
            if (player == null)
                return;
            for (int i = 0; i < eFamiliarAttacks.Count; i++)
                if (ObjectsHitDetector.IsAttacking(eFamiliarAttacks[i].HitBox, player.HurtBox))
                    player.TakeDamage(eFamiliarAttacks[i].Power).Forget();
        }

        private void DetectHitPFAToEA()
        {
            for (int i = 0; i < pFamiliarAttacks.Count; i++)
                for (int j = 0; j < enemyAttacks.Count; j++)
                    if (ObjectsHitDetector.IsHitting(pFamiliarAttacks[i].HitBox, enemyAttacks[j].HitBox))
                        enemyAttacks[j].Break(pFamiliarAttacks[i]);
        }

        private void DetectHitPToHA()
        {
            if (player == null)
                return;
            for (int i = 0; i < healAreas.Count; i++)
                if (ObjectsHitDetector.IsAttacking(healAreas[i].HitBox, player.HurtBox))
                {
                    player.Heal(healAreas[i].HealAmount);
                    healAreas[i].Destroy();
                }
        }

        public Vector2 GetNearestEnemyPos(Vector2 pos)
        {
            Vector2 nearestPos = pos;
            float minDistance = float.MaxValue;

            for (int i = 0; i < enemies.Count; i++)
            {
                float distance = Vector2.Distance(pos, enemies[i].PA.Pos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPos = enemies[i].PA.Pos;
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
