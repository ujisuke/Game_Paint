using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
    public class StageData : ScriptableObject
    {
        [SerializeField] private Vector2Int tileNumber;
        private static StageData instance;
        public static Vector2 StageEdgePosMin => new(0f, 0f);
        public Vector2 StageEdgePosMax => new(tileNumber.x, tileNumber.y);
        public Vector2 StageCenterPos => new(tileNumber.x * 0.5f, tileNumber.y * 0.5f);
        public int Width => tileNumber.x;
        public int Height => tileNumber.y;
        public static StageData Instance => instance;

        public void SetInstance() => instance = this;

        public Vector2 CalcRandomPosFarFrom(Vector2 pos)
        {
            List<Vector2> edgePosMinList = new()
            { new Vector2(StageEdgePosMin.x + 1f, StageEdgePosMin.y + 1f), new Vector2(StageEdgePosMin.x + 1f, StageCenterPos.y), new Vector2(StageCenterPos.x, StageEdgePosMin.y + 1f), StageCenterPos };
            Vector2 areaSize = 0.5f * (StageEdgePosMax - StageEdgePosMin);
            for (int i = edgePosMinList.Count - 1; i >= 0; i--)
                if (edgePosMinList[i].x < pos.x && pos.x < edgePosMinList[i].x + areaSize.x && edgePosMinList[i].y < pos.y && pos.y < edgePosMinList[i].y + areaSize.y)
                    edgePosMinList.RemoveAt(i);
            if (edgePosMinList.Count == 0)
                return StageCenterPos;
            int randomIndex = Random.Range(0, edgePosMinList.Count);
            Vector2 offsetPos = new(Random.Range(0f, tileNumber.x * 0.5f - 1f), Random.Range(0f, tileNumber.y * 0.5f - 1f));
            return edgePosMinList[randomIndex] + offsetPos;
        }

        public Vector2 CalcRandomPosInStage() => new(Random.Range(StageEdgePosMin.x + 1f, StageEdgePosMax.x - 1f), Random.Range(StageEdgePosMin.y + 1f, StageEdgePosMax.y - 1f));

        public Vector2 CalcRandomPosOnEdgeOfStage()
        {
            Vector2 randomPos = CalcRandomPosInStage();
            if (randomPos.y < StageCenterPos.y)
                randomPos.y = StageEdgePosMin.y;
            else
                randomPos.y = StageEdgePosMax.y;
            return randomPos;
        }

        public bool IsOnEdgeOfStage(Vector2 pos) => pos.x <= StageEdgePosMin.x + 1f || pos.x >= StageEdgePosMax.x - 1f || pos.y <= StageEdgePosMin.y + 1f || pos.y >= StageEdgePosMax.y - 1f;

        public bool IsOutOfStage(Vector2 pos) => pos.x < StageEdgePosMin.x || pos.x > StageEdgePosMax.x || pos.y < StageEdgePosMin.y || pos.y > StageEdgePosMax.y;

        public bool IsOutOfStageX(Vector2 pos) => pos.x < StageEdgePosMin.x || pos.x > StageEdgePosMax.x;

        public bool IsUpperOfStage(Vector2 pos) => pos.y > StageCenterPos.y;

        public Vector2 ClampPos(Vector2 pos) => new(Mathf.Clamp(pos.x, StageEdgePosMin.x, StageEdgePosMax.x), Mathf.Clamp(pos.y, StageEdgePosMin.y, StageEdgePosMax.y));
    }
}