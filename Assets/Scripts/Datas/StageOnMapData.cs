using System;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class StageOnMapData
    {
        [SerializeField] private string stageSceneName;
        [SerializeField] private MoveDirOnMap moveToNext;
        [NonSerialized] public StageOnMapData PrevStageData;
        [NonSerialized] public StageOnMapData NextStageData;
        public string StageSceneName => stageSceneName;
        public MoveDirOnMap MoveToNext => moveToNext;
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
