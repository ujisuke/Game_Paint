using Assets.Scripts.StageTiles.Model;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerMove
    {
        private readonly Vector2 pos;
        public Vector2 Pos => pos;
        private readonly Vector2 directionVectorPrev;
        private Vector2 hitBoxVertexPos = new(0.3f, 0.3f);
        private const float moveSpeed = 0.1f;
        private const float wallCollisionOffset = 0.02f;
        private const float lerpFactor = 0.2f;


        public PlayerMove(Vector2 pos, Vector2 directionVectorPrev)
        {
            this.pos = pos;
            this.directionVectorPrev = directionVectorPrev;
        }

        public static PlayerMove Initialize()
        {
            return new PlayerMove(new Vector2(10.5f, 6.5f), Vector2.zero);
        }

        public PlayerMove Move(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            Vector2 inputVector = ApplyInputToDirectionVector(isDirectingUp, isDirectingDown, isDirectingLeft, isDirectingRight);
            Vector2 directionVector = Vector2.Lerp(directionVectorPrev, inputVector, lerpFactor);

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

            return new PlayerMove(pos + minimumDirectionVector, minimumDirectionVector);
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
                fixedDirectionXs[i] =
                    IsWall(playerVertexTargetPosInt)
                    ? ClampDirectionVector(playerVertexPoses[i].x, playerDirectionVectorX)
                    : playerDirectionVectorX;
            }

            return PickShortestNumber(fixedDirectionXs);
        }

        private static float ApplyCollisionToDirectionVectorY(Vector2[] playerVertexPoses, float playerDirectionVectorY)
        {
            float[] fixedDirectionYs = new float[playerVertexPoses.Length];
            for (int i = 0; i < playerVertexPoses.Length; i++)
            {
                Vector2Int playerVertexTargetPosInt = Vector2Int.FloorToInt(playerVertexPoses[i] + new Vector2(0f, playerDirectionVectorY));
                fixedDirectionYs[i] =
                    IsWall(playerVertexTargetPosInt)
                    ? ClampDirectionVector(playerVertexPoses[i].y, playerDirectionVectorY)
                    : playerDirectionVectorY;
            }

            return PickShortestNumber(fixedDirectionYs);
        }

        private static float PickShortestNumber(float[] numbers)
        {
            float pickedNumber = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
                if (math.abs(numbers[i]) < math.abs(pickedNumber))
                    pickedNumber = numbers[i];

            return math.abs(pickedNumber) <= wallCollisionOffset ? 0f : pickedNumber;
        }

        private static Vector2 AdjustDiagonalDirectionVector(Vector2[] playerVertexPoses, Vector2 playerDirectionVector)
        {
            for (int i = 0; i < playerVertexPoses.Length; i++)
            {
                Vector2 playerVertexTargetPos = playerVertexPoses[i] + playerDirectionVector;
                Vector2Int playerVertexTargetPosInt = Vector2Int.FloorToInt(playerVertexTargetPos);
                if (!IsWall(playerVertexTargetPosInt)
                    || (int)playerVertexPoses[i].x == playerVertexTargetPosInt.x
                    || (int)playerVertexPoses[i].y == playerVertexTargetPosInt.y)
                    continue;
                Vector2 wallVertexPos = new(
                    math.clamp(playerVertexTargetPos.x, (int)playerVertexPoses[i].x, (int)playerVertexPoses[i].x + 1),
                    math.clamp(playerVertexTargetPos.y, (int)playerVertexPoses[i].y, (int)playerVertexPoses[i].y + 1));
                Vector2 vertexToVertexVector = wallVertexPos - playerVertexPoses[i];
                Vector2 updatedPlayerDirectionVector = playerDirectionVector;

                if (vertexToVertexVector.x * playerDirectionVector.y < vertexToVertexVector.y * playerDirectionVector.x)
                    updatedPlayerDirectionVector.y = ClampDirectionVector(playerVertexPoses[i].y, playerDirectionVector.y);
                else
                    updatedPlayerDirectionVector.x = ClampDirectionVector(playerVertexPoses[i].x, playerDirectionVector.x);

                return updatedPlayerDirectionVector;
            }

            return playerDirectionVector;
        }

        private static bool IsWall(Vector2Int pos)
        {
            return StageTilesModel.Instance.StageTiles[pos.x, pos.y].IsWall();
        }

        private static float ClampDirectionVector(float startPosXorY, float directionVectorXorY)
        {
            return math.clamp(startPosXorY + directionVectorXorY, (int)startPosXorY + wallCollisionOffset, (int)startPosXorY + 1f - wallCollisionOffset) - startPosXorY;
        }
    }
}
