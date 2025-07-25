using UnityEngine;

namespace Assets.Scripts.Objects.Common
{
    public class HitBox
    {
        private readonly Vector2 pos;
        public Vector2 Pos => pos;
        private readonly Vector2 size;
        public Vector2 Size => size;

        public HitBox(Vector2 pos, Vector2 size)
        {
            this.pos = pos;
            this.size = size;
        }

        public HitBox Move(Vector2 newPos)
        {
            return new HitBox(newPos, size);
        }
    }
}
