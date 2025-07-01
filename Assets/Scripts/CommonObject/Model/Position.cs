using UnityEngine;

namespace Assets.Scripts.CommonObject.Model
{

    public class Position
    {
        private readonly Vector2 pos;

        public Vector2 Pos => pos;

        public Position(Vector2 pos)
        {
            this.pos = pos;
        }

        public Position Move(Vector2 directionVector)
        {
            return new Position(pos + directionVector);
        }
    }
}
