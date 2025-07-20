using Assets.Scripts.Datas;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.Common
{
    public class Status
    {
        private readonly HP hP;
        private readonly float defendReducedSeconds;
        private readonly float poisonSeconds;
        private readonly float attackSpeedDecreaseSeconds;
        public HP HP => hP;
        public float DefendReducedSeconds => defendReducedSeconds;
        public float PoisonSeconds => poisonSeconds;
        public float AttackSpeedDecreaseSeconds => attackSpeedDecreaseSeconds;

        public Status(HP hP, float defendReducedSeconds, float poisonSeconds, float attackSpeedDecreaseSeconds)
        {
            this.hP = hP;
            this.defendReducedSeconds = math.max(defendReducedSeconds, 0f);
            this.poisonSeconds = math.max(poisonSeconds, 0f);
            this.attackSpeedDecreaseSeconds = math.max(attackSpeedDecreaseSeconds, 0f);
        }

        public Status TakeDamageFromFamiliar(FamiliarAttackModel familiarAttackModel)
        {
            ColorName colorName = familiarAttackModel.ColorName;
            FamiliarAttackData familiarAttackData = familiarAttackModel.FamiliarAttackData;
            float newDamageValue = familiarAttackData.Power;
            if (colorName == ColorName.red)
                newDamageValue *= 2f;
            if (defendReducedSeconds > 0f)
                newDamageValue *= 2f;
            HP newHP = hP.TakeDamage((int)newDamageValue);

            float newDefendReducedSeconds = defendReducedSeconds;
            if (colorName == ColorName.blue)
                newDefendReducedSeconds = familiarAttackData.DefendDecreaseSeconds;

            float newPoisonSeconds = poisonSeconds;
            if (colorName == ColorName.purple)
                newPoisonSeconds = familiarAttackData.PoisonSeconds;

            float newAttackSpeedDecreaseSeconds = attackSpeedDecreaseSeconds;
            if (colorName == ColorName.orange)
                newAttackSpeedDecreaseSeconds = familiarAttackData.AttackSpeedDecreaseSeconds;

            return new Status(newHP, newDefendReducedSeconds, newPoisonSeconds, newAttackSpeedDecreaseSeconds);
        }

        public Status TakeDamageFromEnemy(int damageValue)
        {
            HP newHP = hP.TakeDamage(damageValue);
            return new Status(newHP, defendReducedSeconds, poisonSeconds, attackSpeedDecreaseSeconds);
        }

        public Status Heal(int healValue)
        {
            HP newHP = hP.Heal(healValue);
            return new Status(newHP, defendReducedSeconds, poisonSeconds, attackSpeedDecreaseSeconds);
        }
        
        public Status OnUpdate()
        {
            float newDefendReducedSeconds = defendReducedSeconds - Time.deltaTime;
            float newPoisonSeconds = poisonSeconds - Time.deltaTime;
            float newAttackSpeedDecreaseSeconds = attackSpeedDecreaseSeconds - Time.deltaTime;
            return new Status(hP, newDefendReducedSeconds, newPoisonSeconds, newAttackSpeedDecreaseSeconds);
        }
    }
}
