using UnityEngine;

namespace Assets.Scripts.CommonObject.Model
{
    public class HitBox
    {
        private readonly Vector2 size;
        public Vector2 Size => size;

        public HitBox(Vector2 size)
        {
            this.size = size;
        }
    }
}
