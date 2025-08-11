using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.StageTiles.Model;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Model
{
    public class PlayerMove
    {
        private Vector2 directionVectorPrev;
        private Vector2 hurtBoxVertexPos;
        private readonly float moveSpeed;
        private const float wallCollisionOffset = 0.001f;
        private const float lerpFactor = 0.1f;
        private PA pA;
        public Vector2 DirectionVector => directionVectorPrev;
        public PA PA => pA;

        public PlayerMove(float moveSpeed, Vector2 hurtBoxScale, Vector2 pos, float angle)
        {
            this.moveSpeed = moveSpeed;
            directionVectorPrev = Vector2.zero;
            hurtBoxVertexPos = hurtBoxScale * 0.5f;
            pA = new PA(pos, angle);
        }

        public void Move(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            Vector2 inputVector = ApplyInputToDirectionVector(isDirectingUp, isDirectingDown, isDirectingLeft, isDirectingRight);
            Vector2 directionVector = Vector2.Lerp(directionVectorPrev, inputVector, lerpFactor);

            Vector2[] playerVertexPoses = {
                pA.Pos + hurtBoxVertexPos,
                pA.Pos + new Vector2(-hurtBoxVertexPos.x, hurtBoxVertexPos.y),
                pA.Pos - hurtBoxVertexPos,
                pA.Pos - new Vector2(-hurtBoxVertexPos.x, hurtBoxVertexPos.y)
            };

            Vector2 minimalDirectionVector = new(
                ApplyCollisionToDirectionVectorX(playerVertexPoses, directionVector.x),
                ApplyCollisionToDirectionVectorY(playerVertexPoses, directionVector.y));

            directionVectorPrev = AdjustDiagonalDirectionVector(playerVertexPoses, minimalDirectionVector);
            pA = pA.MoveIgnoringStage(directionVectorPrev);
        }

        private Vector2 ApplyInputToDirectionVector(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            Vector2 directionVector = Vector2.zero;

            if (isDirectingUp) directionVector += Vector2.up;
            if (isDirectingDown) directionVector += Vector2.down;
            if (isDirectingLeft) directionVector += Vector2.left;
            if (isDirectingRight) directionVector += Vector2.right;
            return moveSpeed * Time.deltaTime * directionVector;
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
            return StageTilesModel.Instance.StageTiles[pos.x, pos.y].IsWall;
        }

        private static float ClampDirectionVector(float startPosXorY, float directionVectorXorY)
        {
            return math.clamp(startPosXorY + directionVectorXorY, (int)startPosXorY + wallCollisionOffset, (int)startPosXorY + 1f - wallCollisionOffset) - startPosXorY;
        }
    }
}
