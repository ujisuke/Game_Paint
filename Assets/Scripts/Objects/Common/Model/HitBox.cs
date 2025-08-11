using UnityEngine;

namespace Assets.Scripts.Objects.Common.Model
{
    public class HitBox
    {
        private readonly Vector2 pos;
        private readonly Vector2 size;
        private readonly bool isActive;
        public Vector2 Pos => pos;
        public Vector2 Size => size;
        public bool IsActive => isActive;

        public HitBox(Vector2 pos, Vector2 size, bool isActive)
        {
            this.pos = pos;
            this.size = size;
            this.isActive = isActive;
        }

        public HitBox SetActive(bool isActive)
        {
            return new HitBox(pos, size, isActive);
        }

        public HitBox SetPos(Vector2 newPos)
        {
            return new HitBox(newPos, size, isActive);
        }
    }
}
