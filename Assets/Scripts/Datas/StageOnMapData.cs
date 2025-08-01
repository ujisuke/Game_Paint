using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class StageOnMapData
    {
        [SerializeField] private string stageSceneName;
        [SerializeField] private List<MoveDirOnMap> moveDirsToNext;
        [NonSerialized] public StageOnMapData PrevStageData;
        [NonSerialized] public StageOnMapData NextStageData;
        [NonSerialized] public float moveSecondsPerEdge;
        public string StageSceneName => stageSceneName;
        public List<MoveDirOnMap> MoveDirsToNext => moveDirsToNext;
        public bool IsToNextDir(MoveDirOnMap moveDir) => moveDirsToNext[0] == moveDir;
        public bool IsToPrevDir(MoveDirOnMap moveDir) => moveDirsToNext[^1] == moveDir;
        public float MoveSecondsToNext => moveSecondsPerEdge * moveDirsToNext.Count;
    }

    public enum MoveDirOnMap
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
}
