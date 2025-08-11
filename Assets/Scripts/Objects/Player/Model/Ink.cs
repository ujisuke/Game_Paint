using UnityEngine;

namespace Assets.Scripts.Objects.Player.Model
{
    public class Ink
    {
        private readonly float currentInk;
        private readonly bool isReloading;
        public float Ratio => currentInk;
        public bool IsEmpty => currentInk <= 0;
        public bool IsFull => currentInk >= 1f;
        public bool IsReloading => isReloading;

        public Ink(float currentInk, bool isReloading)
        {
            this.currentInk = Mathf.Clamp(currentInk, 0f, 1f);
            this.isReloading = isReloading;
        }

        public Ink()
        {
            currentInk = 1f;
            isReloading = false;
        }

        public Ink Reduce(float amount)
        {
            return new Ink(currentInk - amount, isReloading);
        }

        public Ink Add(float amount)
        {
            return new Ink(currentInk + amount, isReloading);
        }

        public static Ink BeginReload()
        {
            return new Ink(0f, true);
        }

        public static Ink EndReload()
        {
            return new Ink(1f, false);
        }
    }
}
