using UnityEngine;

namespace Assets.Scripts.Objects.Common.Model
{
    public class HurtBox
    {
        private readonly Vector2 pos;
        private readonly Vector2 size;
        private readonly bool isActive;
        public Vector2 Pos => pos;
        public Vector2 Size => size;
        public bool IsActive => isActive;

        public HurtBox(Vector2 pos, Vector2 size, bool isActive)
        {
            this.pos = pos;
            this.size = size;
            this.isActive = isActive;
        }

        public HurtBox SetActive(bool isActive)
        {
            return new HurtBox(pos, size, isActive);
        }
        
        public HurtBox Move(Vector2 dir)
        {
            return new HurtBox(pos + dir, size, isActive);
        }
    }
}
