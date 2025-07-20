using UnityEngine;

namespace Assets.Scripts.Objects.Common
{

    public class PA
    {
        private readonly Vector2 pos;
        private readonly float angle;

        public Vector2 Pos => pos;
        public float Angle => angle;

        public PA(Vector2 pos, float angle)
        {
            this.pos = pos;
            this.angle = angle;
        }

        public PA Move(Vector2 directionVector) => new(pos + directionVector, angle);
    }
}
