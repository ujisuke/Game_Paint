using Unity.Mathematics;

namespace Assets.Scripts.Objects.Common.Model
{
    public class HP
    {
        private readonly float currentHP;
        private readonly float maxHP;
        public float CurrentHP => currentHP;
        public float MaxHP => maxHP;
        public float Ratio => currentHP / maxHP;

        public HP(float currentHP, float maxHP)
        {
            this.maxHP = maxHP;
            this.currentHP = math.clamp(currentHP, 0, maxHP);
        }

        public HP(float maxHP)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
        }

        public HP TakeDamage(float damageValue)
        {
            return new HP(currentHP - damageValue, maxHP);
        }

        public HP Heal(float healRate)
        {
            float healValue = maxHP * healRate;
            return new HP(currentHP + healValue, maxHP);
        }

        public bool IsDead()
        {
            return currentHP <= 0f;
        }

        public bool IsLessThanHalf()
        {
            return currentHP <= maxHP / 2f;
        }
    }
}
