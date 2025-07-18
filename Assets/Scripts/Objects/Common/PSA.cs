using UnityEngine;

namespace Assets.Scripts.Objects.Common
{

    public class PSA
    {
        private readonly Vector2 pos;
        private readonly Vector2 scale;
        private readonly float angle;

        public Vector2 Pos => pos;
        public Vector2 Scale => scale;
        public float Angle => angle;

        public PSA(Vector2 pos, Vector2 scale, float angle)
        {
            this.pos = pos;
            this.scale = scale;
            this.angle = angle;
        }

        public PSA Move(Vector2 directionVector) => new(pos + directionVector, scale, angle);
    }
}
