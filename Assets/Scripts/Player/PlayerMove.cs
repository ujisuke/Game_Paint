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
        private const float wallCollisionOffset = 0.02f;

        public PlayerMove(Vector2 pos)
        {
            this.pos = pos;
        }

        public PlayerMove Move(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            Vector2 directionVector = ApplyInputToDirectionVector(isDirectingUp, isDirectingDown, isDirectingLeft, isDirectingRight);

            Vector2[] playerVertexPoses = {
                pos + hitBoxVertexPos,
                pos + new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y),
                pos - hitBoxVertexPos,
                pos - new Vector2(-hitBoxVertexPos.x, hitBoxVertexPos.y)
            };

            Vector2 minimalDirectionVector = new(
                ApplyCollisionToDirectionVectorX(playerVertexPoses, directionVector.x),
                ApplyCollisionToDirectionVectorY(playerVertexPoses, directionVector.y));
            Vector2 minimumDirectionVector = AdjustDiagonalDirectionVector(playerVertexPoses, minimalDirectionVector);

            return new PlayerMove(pos + minimumDirectionVector);
        }

        private static Vector2 ApplyInputToDirectionVector(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            Vector2 directionVector = Vector2.zero;

            if (isDirectingUp) directionVector += Vector2.up;
            if (isDirectingDown) directionVector += Vector2.down;
            if (isDirectingLeft) directionVector += Vector2.left;
            if (isDirectingRight) directionVector += Vector2.right;

            return directionVector * moveSpeed;
        }

        private static float ApplyCollisionToDirectionVectorX(Vector2[] playerVertexPoses, float playerDirectionVectorX)
        {
            float[] fixedDirectionXs = new float[playerVertexPoses.Length];
            for (int i = 0; i < playerVertexPoses.Length; i++)
            {
                Vector2Int playerVertexTargetPosInt = Vector2Int.FloorToInt(playerVertexPoses[i] + new Vector2(playerDirectionVectorX, 0f));
                if (TilesFacade.Instance.Tiles[playerVertexTargetPosInt.x, playerVertexTargetPosInt.y].IsWall())
                    fixedDirectionXs[i] = math.clamp(playerVertexPoses[i].x + playerDirectionVectorX, (int)playerVertexPoses[i].x + wallCollisionOffset, (int)playerVertexPoses[i].x + 1f - wallCollisionOffset) - playerVertexPoses[i].x;
                else
                    fixedDirectionXs[i] = playerDirectionVectorX;
            }

            return PickShortestNumber(fixedDirectionXs);
        }

        private static float ApplyCollisionToDirectionVectorY(Vector2[] playerVertexPoses, float playerDirectionVectorY)
        {
            float[] fixedDirectionYs = new float[playerVertexPoses.Length];
            for (int i = 0; i < playerVertexPoses.Length; i++)
            {
                Vector2Int playerVertexTargetPosInt = Vector2Int.FloorToInt(playerVertexPoses[i] + new Vector2(0f, playerDirectionVectorY));
                if (TilesFacade.Instance.Tiles[playerVertexTargetPosInt.x, playerVertexTargetPosInt.y].IsWall())
                    fixedDirectionYs[i] = math.clamp(playerVertexPoses[i].y + playerDirectionVectorY, (int)playerVertexPoses[i].y + wallCollisionOffset, (int)playerVertexPoses[i].y + 1f - wallCollisionOffset) - playerVertexPoses[i].y;
                else
                    fixedDirectionYs[i] = playerDirectionVectorY;
            }

            return PickShortestNumber(fixedDirectionYs);
        }

        private static float PickShortestNumber(float[] numbers)
        {
            float pickedNumber = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
                if (math.abs(numbers[i]) < math.abs(pickedNumber))
                    pickedNumber = numbers[i];

            return pickedNumber;
        }

        private static Vector2 AdjustDiagonalDirectionVector(Vector2[] playerVertexPoses, Vector2 playerDirectionVector)
        {
            for (int i = 0; i < playerVertexPoses.Length; i++)
            {
                Vector2 playerVertexTargetPos = playerVertexPoses[i] + playerDirectionVector;
                Vector2Int playerVertexTargetPosInt = Vector2Int.FloorToInt(playerVertexTargetPos);
                if (!TilesFacade.Instance.Tiles[playerVertexTargetPosInt.x, playerVertexTargetPosInt.y].IsWall())
                    continue;
                if ((int)playerVertexPoses[i].x == playerVertexTargetPosInt.x ||
                (int)playerVertexPoses[i].y == playerVertexTargetPosInt.y)
                    continue;
                Vector2 wallVertexPos = new(
                    math.clamp(playerVertexTargetPos.x, (int)playerVertexPoses[i].x, (int)playerVertexPoses[i].x + 1),
                    math.clamp(playerVertexTargetPos.y, (int)playerVertexPoses[i].y, (int)playerVertexPoses[i].y + 1));
                Vector2 vertexToVertexVector = wallVertexPos - playerVertexPoses[i];
                Vector2 updatedPlayerDirectionVector = playerDirectionVector;
                if (vertexToVertexVector.x * playerDirectionVector.y < vertexToVertexVector.y * playerDirectionVector.x)
                    updatedPlayerDirectionVector.y = math.clamp(playerVertexPoses[i].y + playerDirectionVector.y, (int)playerVertexPoses[i].y + wallCollisionOffset, (int)playerVertexPoses[i].y + 1f - wallCollisionOffset) - playerVertexPoses[i].y;
                else
                    updatedPlayerDirectionVector.x = math.clamp(playerVertexPoses[i].x + playerDirectionVector.x, (int)playerVertexPoses[i].x + wallCollisionOffset, (int)playerVertexPoses[i].x + 1f - wallCollisionOffset) - playerVertexPoses[i].x;
                return updatedPlayerDirectionVector;
            }
            
            return playerDirectionVector;
        }
    }
}
