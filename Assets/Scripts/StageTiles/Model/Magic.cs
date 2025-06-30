using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StageTiles.Model
{
    public class Magic
    {
        public static void Summon(List<Vector2Int> paintedPosInts)
        {
            if (paintedPosInts.Count == 3)
                Debug.Log("SUMMON");
        }
    }
}