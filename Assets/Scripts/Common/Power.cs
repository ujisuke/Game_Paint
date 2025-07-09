using Unity.Mathematics;

namespace Assets.Scripts.Common
{
    public class Power
    {
        private readonly int currentPower;
        public int CurrentPower => currentPower;
        private readonly int defaultPower;
        public int DefaultPower => defaultPower;

        public Power(int currentPower, int defaultPower)
        {
            this.defaultPower = defaultPower;
            this.currentPower = math.max(currentPower, 0);
        }

        public Power(int defaultPower)
        {
            this.defaultPower = defaultPower;
            currentPower = defaultPower;
        }

        public Power Reset()
        {
            return new Power(defaultPower, defaultPower);
        }

        public Power PowerUp(float ratio)
        {
            return new Power((int)(currentPower * ratio), defaultPower);
        }
    }
}
