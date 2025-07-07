using UnityEngine;

namespace Assets.Scripts.Common
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
    }
}
