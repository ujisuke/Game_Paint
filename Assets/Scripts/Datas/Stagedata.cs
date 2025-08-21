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

        public List<Vector2> CalcRandomPosListInStage(int posCount)
        {
            List<Vector2> posList = new();
            for (float x = StageEdgePosMin.x + 0.5f; x < StageEdgePosMax.x; x++)
                for (float y = StageEdgePosMin.y + 0.5f; y < StageEdgePosMax.y; y++)
                    posList.Add(new Vector2(x, y));

            List<Vector2> pickedPosList = new();
            for (int i = 0; i < posCount; i++)
            {
                int randomIndex = Random.Range(0, posList.Count);
                pickedPosList.Add(posList[randomIndex]);
                posList.RemoveAt(randomIndex);
            }
            return pickedPosList;
        }

        public Vector2 CalcRandomPosOnEdgeOfStage()
        {
            Vector2 randomPos = CalcRandomPosInStage();
            if (randomPos.y < StageCenterPos.y)
                randomPos.y = StageEdgePosMin.y;
            else
                randomPos.y = StageEdgePosMax.y;
            return randomPos;
        }

        public List<Vector2> CalcEdgePosList(int offset) =>
            new(){
                new(StageEdgePosMin.x + offset, StageEdgePosMin.y + offset),
                new(StageEdgePosMax.x - offset, StageEdgePosMin.y + offset),
                new(StageEdgePosMax.x - offset, StageEdgePosMax.y - offset),
                new(StageEdgePosMin.x + offset, StageEdgePosMax.y - offset),
            };

        public static List<Vector2> CalcRandomNearPosList(Vector2 pos, int radius, int count)
        {
            List<Vector2> posList = new();
            Vector2 posMin = pos - new Vector2(radius, radius);
            Vector2 posMax = pos + new Vector2(radius, radius);
            Vector2 fixedPosMin = new((int)posMin.x + 0.5f - radius, (int)posMin.y + 0.5f - radius);
            Vector2 fixedPosMax = new((int)posMax.x + 0.5f + radius, (int)posMax.y + 0.5f + radius);
            for (float x = fixedPosMin.x; x < fixedPosMax.x + 1f; x++)
                for (float y = fixedPosMin.y; y < fixedPosMax.y + 1f; y++)
                    posList.Add(new Vector2(x, y));
            List<Vector2> pickedPosList = new();
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, posList.Count);
                pickedPosList.Add(posList[randomIndex]);
                posList.RemoveAt(randomIndex);
            }
            return pickedPosList;
        }

        public static List<Vector2> CalcAroundPosList(Vector2 pos, float radius)
        {
            Vector2 fixedPos = new((int)pos.x + 0.5f, (int)pos.y + 0.5f);
            List<Vector2> aroundPosList = new();
            for (float x = fixedPos.x - radius; x <= fixedPos.x + radius; x++)
                for (float y = fixedPos.y - radius; y <= fixedPos.y + radius; y++)
                    aroundPosList.Add(new Vector2(x, y));
            return aroundPosList;
        }

        public bool IsOnEdgeOfStage(Vector2 pos) => pos.x <= StageEdgePosMin.x + 1f || pos.x >= StageEdgePosMax.x - 1f || pos.y <= StageEdgePosMin.y + 1f || pos.y >= StageEdgePosMax.y - 1f;

        public bool IsOutOfStage(Vector2 pos, float margin = 0f) => pos.x < StageEdgePosMin.x - margin || pos.x > StageEdgePosMax.x + margin || pos.y < StageEdgePosMin.y - margin || pos.y > StageEdgePosMax.y + margin;

        public bool IsOutOfStageX(Vector2 pos) => pos.x < StageEdgePosMin.x || pos.x > StageEdgePosMax.x;

        public bool IsUpperOfStage(Vector2 pos) => pos.y > StageCenterPos.y;

        public Vector2 ClampPos(Vector2 pos) => new(Mathf.Clamp(pos.x, StageEdgePosMin.x, StageEdgePosMax.x), Mathf.Clamp(pos.y, StageEdgePosMin.y, StageEdgePosMax.y));
    }
}