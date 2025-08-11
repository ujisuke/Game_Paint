using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.Objects.Common.Model
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

        public PA MoveIgnoringStage(Vector2 directionVector) => new(pos + directionVector, angle);

        public PA MoveInStage(Vector2 directionVector) => new(StageData.Instance.ClampPos(pos + directionVector), angle);

        public PA Rotate(float angle) => new(pos, this.angle + angle);
    }
}
