using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
    public class StageData : ScriptableObject
    {
        [SerializeField] private Vector2Int tileNumber;
        private static StageData instance;
        public static Vector2 StageEdgePosMin => new(1f, 1f);
        public Vector2 StageEdgePosMax => new(tileNumber.x - 1f, tileNumber.y - 1f);
        public Vector2 StageCenterPos => new(tileNumber.x * 0.5f, tileNumber.y * 0.5f);
        public static StageData Instance => instance;

        public void SetInstance() => instance = this;

        public Vector2 CalcRandomPosFarFrom(Vector2 posTarget)
        {
            List<Vector2> edgePosMinList = new()
            { StageEdgePosMin, new Vector2(StageEdgePosMin.x, StageCenterPos.y), new Vector2(StageCenterPos.x, StageEdgePosMin.y), StageCenterPos };
            for (int i = edgePosMinList.Count - 1; i >= 0; i--)
            {
                if (posTarget.x < edgePosMinList[i].x || posTarget.x >= edgePosMinList[i].x + tileNumber.x * 0.5f ||
                   posTarget.y < edgePosMinList[i].y || posTarget.y >= edgePosMinList[i].y + tileNumber.y * 0.5f)
                    continue;
                edgePosMinList.RemoveAt(i);
            }
            if (edgePosMinList.Count == 0)
                return StageCenterPos;
            int randomIndex = Random.Range(0, edgePosMinList.Count);
            Vector2 offsetPos = new(Random.Range(0f, tileNumber.x * 0.5f), Random.Range(0f, tileNumber.y * 0.5f));
            return edgePosMinList[randomIndex] + offsetPos;
        }

        public bool IsOutOfStage(Vector2 pos)
        {
            return pos.x < StageEdgePosMin.x || pos.x > StageEdgePosMax.x || pos.y < StageEdgePosMin.y || pos.y > StageEdgePosMax.y;
        }
    }
}