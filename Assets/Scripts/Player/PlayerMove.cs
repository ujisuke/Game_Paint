using Assets.Scripts.Tiles;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMove
    {
        private readonly Vector2 pos;
        public Vector2 Pos => pos;
        private Vector2 hitBoxVertexPos = new(0.3f, 0.3f);
        private const float moveSpeed = 0.1f;

        public PlayerMove(Vector2 pos)
        {
            this.pos = pos;
        }

        public PlayerMove Move(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            Vector2 directionVector = Vector2.zero;

            if (isDirectingUp) directionVector += Vector2.up;
            if (isDirectingDown) directionVector += Vector2.down;
            if (isDirectingLeft) directionVector += Vector2.left;
            if (isDirectingRight) directionVector += Vector2.right;
            directionVector *= moveSpeed;

            Vector2 fixedDirectionVector = Vector2.zero;

            float[] fixedDirectionXs = {
                ApplyCollisionToDirectionVectorX(pos + hitBoxVertexPos, directionVector.x),
                ApplyCollisionToDirectionVectorX(pos + new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y), directionVector.x),
                ApplyCollisionToDirectionVectorX(pos - hitBoxVertexPos, directionVector.x),
                ApplyCollisionToDirectionVectorX(pos - new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y), directionVector.x)
                };
            fixedDirectionVector.x = PickShortestNumber(fixedDirectionXs);

            float[] fixedDirectionYs = {
                ApplyCollisionToDirectionVectorY(pos + hitBoxVertexPos, directionVector.y),
                ApplyCollisionToDirectionVectorY(pos + new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y), directionVector.y),
                ApplyCollisionToDirectionVectorY(pos - hitBoxVertexPos, directionVector.y),
                ApplyCollisionToDirectionVectorY(pos - new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y), directionVector.y)
                };
            fixedDirectionVector.y = PickShortestNumber(fixedDirectionYs);

            fixedDirectionVector = AdjustDiagonalDirectionVector(fixedDirectionVector);

            return new PlayerMove(pos + fixedDirectionVector);
        }

        private static float ApplyCollisionToDirectionVectorX(Vector2 playerVertexPos, float playerDirectionVectorX)
        {
            Debug.Log(math.clamp(0, 3, 1));
            if (TilesFacade.Instance.Tiles[(int)(playerVertexPos.x + playerDirectionVectorX), (int)playerVertexPos.y].IsWall())
                return math.clamp(playerVertexPos.x + playerDirectionVectorX, (int)playerVertexPos.x + 0.05f, (int)playerVertexPos.x + 1f - 0.05f) - playerVertexPos.x;
            return playerDirectionVectorX;
        }

        private static float ApplyCollisionToDirectionVectorY(Vector2 playerVertexPos, float playerDirectionVectorY)
        {
            if (TilesFacade.Instance.Tiles[(int)playerVertexPos.x, (int)(playerVertexPos.y + playerDirectionVectorY)].IsWall())
                return math.clamp(playerVertexPos.y + playerDirectionVectorY, (int)playerVertexPos.y + 0.05f, (int)playerVertexPos.y + 1f - 0.05f) - playerVertexPos.y;
            return playerDirectionVectorY;
        }

        private static float PickShortestNumber(float[] numbers)
        {
            float pickedNumber = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
                if (math.abs(numbers[i]) < math.abs(pickedNumber))
                    pickedNumber = numbers[i];
            return pickedNumber;
        }

        private Vector2 AdjustDiagonalDirectionVector(Vector2 playerDirectionVector)
        {
            Vector2[] playerVertexPoses = {
                pos + hitBoxVertexPos,
                pos + new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y),
                pos - hitBoxVertexPos,
                pos - new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y)
            };
            for (int i = 0; i < playerVertexPoses.Length; i++)
            {
                Vector2 playerVertexTargetPos = playerVertexPoses[i] + playerDirectionVector;
                if (!TilesFacade.Instance.Tiles[(int)playerVertexTargetPos.x, (int)playerVertexTargetPos.y].IsWall())
                    continue;
                if ((int)playerVertexPoses[i].x == (int)playerVertexTargetPos.x ||
                (int)playerVertexPoses[i].y == (int)playerVertexTargetPos.y)
                    continue;
                Vector2 wallVertexPos = new(
                    math.clamp(playerVertexTargetPos.x, (int)playerVertexPoses[i].x, (int)playerVertexPoses[i].x + 1),
                    math.clamp(playerVertexTargetPos.y, (int)playerVertexPoses[i].y, (int)playerVertexPoses[i].y + 1));
                Vector2 vertexToVertexVector = wallVertexPos - playerVertexPoses[i];
                Vector2 updatedPlayerDirectionVector = playerDirectionVector;
                if (vertexToVertexVector.x * playerDirectionVector.y < vertexToVertexVector.y * playerDirectionVector.x)
                    updatedPlayerDirectionVector.y = math.clamp(playerVertexPoses[i].y + playerDirectionVector.y, (int)playerVertexPoses[i].y + 0.05f, (int)playerVertexPoses[i].y + 1f - 0.05f) - playerVertexPoses[i].y;
                else
                    updatedPlayerDirectionVector.x = math.clamp(playerVertexPoses[i].x + playerDirectionVector.x, (int)playerVertexPoses[i].x + 0.05f, (int)playerVertexPoses[i].x + 1f - 0.05f) - playerVertexPoses[i].x;
                return updatedPlayerDirectionVector;
            }
            return playerDirectionVector;
        }
    }
}
