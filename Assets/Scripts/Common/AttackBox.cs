using UnityEngine;

namespace Assets.Scripts.Common
{
    public class AttackBox
    {
        private readonly Vector2 pos;
        private readonly Vector2 size;

        public Vector2 Pos => pos;
        public Vector2 Size => size;

        public AttackBox(Vector2 pos, Vector2 size)
        {
            this.pos = pos;
            this.size = size;
        }
    }
}
