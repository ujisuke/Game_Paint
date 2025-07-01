using Unity.Mathematics;

namespace Assets.Scripts.CommonObject.Model
{
    public class Power
    {
        private readonly float currentPower;
        public float CurrentPower => currentPower;
        private readonly float defaultPower;
        public float DefaultPower => defaultPower;

        public Power(float currentPower, float defaultPower)
        {
            this.defaultPower = defaultPower;
            this.currentPower = math.max(currentPower, 0);
        }

        public Power(float defaultPower)
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
            return new Power(currentPower * ratio, defaultPower);
        }
    }
}
