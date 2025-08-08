using Unity.Mathematics;

namespace Assets.Scripts.Objects.Common.Model
{
    public class HP
    {
        private readonly int currentHP;
        private readonly int maxHP;
        public int CurrentHP => currentHP;
        public int MaxHP => maxHP;

        public HP(int currentHP, int maxHP)
        {
            this.maxHP = maxHP;
            this.currentHP = math.clamp(currentHP, 0, maxHP);
        }

        public HP(int maxHP)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
        }

        public HP TakeDamage(float damageValue)
        {
            return new HP(currentHP - (int)damageValue, maxHP);
        }

        public HP Heal(float healRate)
        {
            int healValue = (int)(maxHP * healRate);
            return new HP(currentHP + healValue, maxHP);
        }

        public bool IsDead()
        {
            return currentHP == 0;
        }
    }
}
