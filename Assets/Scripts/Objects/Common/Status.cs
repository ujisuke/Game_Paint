using Assets.Scripts.Datas;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.Common
{
    public class Status
    {
        private readonly HP hP;
        private readonly float defenseDecreasedSeconds;
        private readonly float poisonedSeconds;
        private readonly float attackSpeedDecreasedSeconds;
        private readonly float poisonedElapsedSeconds;
        private readonly ColorEffectData colorEffectData;
        public HP HP => hP;
        public float DefenseDecreasedSeconds => defenseDecreasedSeconds;
        public float PoisonedSeconds => poisonedSeconds;
        public float AttackSpeedDecreaseSeconds => attackSpeedDecreasedSeconds;
        public bool IsDefenseDecreased => defenseDecreasedSeconds > 0f;
        public bool IsPoisoned => poisonedSeconds > 0f;
        public bool IsAttackSpeedDecreased => attackSpeedDecreasedSeconds > 0f;

        public Status(HP hP, float defenseDecreasedSeconds, float poisonedSeconds, float attackSpeedDecreasedSeconds, float poisonedElapsedSeconds, ColorEffectData colorEffectData)
        {
            this.hP = hP;
            this.defenseDecreasedSeconds = math.max(defenseDecreasedSeconds, 0f);
            this.poisonedSeconds = math.max(poisonedSeconds, 0f);
            this.attackSpeedDecreasedSeconds = math.max(attackSpeedDecreasedSeconds, 0f);
            this.poisonedElapsedSeconds = poisonedElapsedSeconds;
            this.colorEffectData = colorEffectData;
        }

        public Status TakeDamageFromFamiliar(FamiliarAttackModel familiarAttackModel)
        {
            ColorName colorName = familiarAttackModel.ColorName;
            FamiliarAttackData familiarAttackData = familiarAttackModel.FamiliarAttackData;
            float newDamageValue = familiarAttackData.Power;
            if (colorName == ColorName.red)
                newDamageValue *= colorEffectData.PowerMultiplier;
            if (IsDefenseDecreased)
                newDamageValue /= colorEffectData.DefenseMultiplier;
            HP newHP = hP.TakeDamage((int)newDamageValue);

            float newDefenseDecreasedSeconds = defenseDecreasedSeconds;
            if (colorName == ColorName.blue)
                newDefenseDecreasedSeconds = familiarAttackData.DefenseDecreaseSeconds;

            float newPoisonedSeconds = poisonedSeconds;
            if (colorName == ColorName.purple)
                newPoisonedSeconds = familiarAttackData.PoisonSeconds;

            float newAttackSpeedDecreaseSeconds = attackSpeedDecreasedSeconds;
            if (colorName == ColorName.orange)
                newAttackSpeedDecreaseSeconds = familiarAttackData.AttackSpeedDecreaseSeconds;

            return new Status(newHP, newDefenseDecreasedSeconds, newPoisonedSeconds, newAttackSpeedDecreaseSeconds, poisonedElapsedSeconds, colorEffectData);
        }

        public Status TakeDamageFromEnemy(int damageValue)
        {
            HP newHP = hP.TakeDamage(damageValue);
            return new Status(newHP, defenseDecreasedSeconds, poisonedSeconds, attackSpeedDecreasedSeconds, poisonedElapsedSeconds, colorEffectData);
        }
        
        public Status CountDown()
        {
            float newDefenseDecreasedSeconds = defenseDecreasedSeconds - Time.deltaTime;
            float newPoisonedSeconds = poisonedSeconds - Time.deltaTime;
            float newAttackSpeedDecreasedSeconds = attackSpeedDecreasedSeconds - Time.deltaTime;
            if(!IsPoisoned)
                return new Status(hP, newDefenseDecreasedSeconds, newPoisonedSeconds, newAttackSpeedDecreasedSeconds, poisonedElapsedSeconds, colorEffectData);

            float newPoisonedElapsedSeconds = poisonedElapsedSeconds + Time.deltaTime;
            if (newPoisonedElapsedSeconds < 1f)
                return new Status(hP, newDefenseDecreasedSeconds, newPoisonedSeconds, newAttackSpeedDecreasedSeconds, newPoisonedElapsedSeconds, colorEffectData);

            newPoisonedElapsedSeconds -= 1f;
            HP newHP = hP.TakeDamage((int)(hP.MaxHP * colorEffectData.PoisonDamageRate));
            return new Status(newHP, newDefenseDecreasedSeconds, newPoisonedSeconds, newAttackSpeedDecreasedSeconds, newPoisonedElapsedSeconds, colorEffectData);
        }
    }
}
