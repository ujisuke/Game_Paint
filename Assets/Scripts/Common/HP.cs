using Unity.Mathematics;

namespace Assets.Scripts.Common
{
    public class HP
    {
        private readonly int currentHP;
        private readonly int maxHP;

        public HP(int currentHP, int maxHP)
        {
            this.maxHP = maxHP;
            math.clamp(currentHP, 0, maxHP);
        }

        public HP(int maxHP)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
        }

        public HP TakeDamage(int damageValue)
        {
            return new HP(currentHP - damageValue, maxHP);
        }

        public HP Heal(int healValue)
        {
            return new HP(currentHP + healValue, maxHP);
        }

        public bool IsDead()
        {
            return currentHP <= 0;
        }
    }
}
