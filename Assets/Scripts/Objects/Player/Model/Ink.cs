using UnityEngine;

namespace Assets.Scripts.Objects.Player.Model
{
    public class Ink
    {
        private readonly float currentInk;
        public float Ratio => currentInk;
        public bool IsEmpty => currentInk <= 0;

        public Ink(float currentInk)
        {
            this.currentInk = Mathf.Clamp(currentInk, 0f, 1f);
        }

        public Ink()
        {
            currentInk = 1f;
        }

        public Ink Reduce(float amount)
        {
            return new Ink(currentInk - amount);
        }

        public Ink Add(float amount)
        {
            return new Ink(currentInk + amount);
        }
    }
}
