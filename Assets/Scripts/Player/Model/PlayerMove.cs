using Assets.Scripts.StageTiles.Model;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerMove
    {
        private readonly Vector2 directionVectorPrev;
        private Vector2 hurtBoxVertexPos;
        private readonly float moveSpeed;
        private const float wallCollisionOffset = 0.02f;
        private const float lerpFactor = 0.2f;
        public Vector2 DirectionVector => directionVectorPrev;


        public PlayerMove(float moveSpeed, Vector2 directionVectorPrev, Vector2 hurtBoxVertexPos)
        {
            this.moveSpeed = moveSpeed;
            this.directionVectorPrev = directionVectorPrev;
            this.hurtBoxVertexPos = hurtBoxVertexPos;
        }

        public static PlayerMove Initialize(float moveSpeed, Vector2 hurtBoxScale)
        {
            return new PlayerMove(moveSpeed, Vector2.zero, hurtBoxScale * 0.5f);
        }

        public PlayerMove Move(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight, Vector2 pos)
        {
            Vector2 inputVector = ApplyInputToDirectionVector(isDirectingUp, isDirectingDown, isDirectingLeft, isDirectingRight);
            Vector2 directionVector = Vector2.Lerp(directionVectorPrev, inputVector, lerpFactor);

            Vector2[] playerVertexPoses = {
                pos + hurtBoxVertexPos,
                pos + new Vector2(-hurtBoxVertexPos.x, hurtBoxVertexPos.y),
                pos - hurtBoxVertexPos,
                pos - new Vector2(-hurtBoxVertexPos.x, hurtBoxVertexPos.y)
            };

            Vector2 minimalDirectionVector = new(
                ApplyCollisionToDirectionVectorX(playerVertexPoses, directionVector.x),
                ApplyCollisionToDirectionVectorY(playerVertexPoses, directionVector.y));
            Vector2 minimumDirectionVector = AdjustDiagonalDirectionVector(playerVertexPoses, minimalDirectionVector);

            return new PlayerMove(moveSpeed, minimumDirectionVector, hurtBoxVertexPos);
        }

        private Vector2 ApplyInputToDirectionVector(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
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
