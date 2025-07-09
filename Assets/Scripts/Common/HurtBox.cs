using UnityEngine;

namespace Assets.Scripts.Common
{
    public class HurtBox
    {
        private readonly Vector2 pos;
        public Vector2 Pos => pos;
        private readonly Vector2 size;
        public Vector2 Size => size;
        private readonly bool isActive;
        public bool IsActive => isActive;

        public HurtBox(Vector2 pos, Vector2 size, bool isActive)
        {
            this.pos = pos;
            this.size = size;
            this.isActive = isActive;
        }

        public HurtBox Activate()
        {
            return new HurtBox(pos, size, true);
        }

        public HurtBox Inactivate()
        {
            return new HurtBox(pos, size, false);
        }
        
        public HurtBox Move(Vector2 pos)
        {
            return new HurtBox(pos, size, isActive);
        }
    }
}
