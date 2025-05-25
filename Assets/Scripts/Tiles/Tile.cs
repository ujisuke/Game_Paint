using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class Tile
    {
        private readonly int colorIdCorrect;
        private readonly int colorIdCurrent;
        private readonly bool hasBeenPaintedCorrectly;

        public Tile(int colorIdCorrect, int colorIdCurrent, bool hasBeenPaintedCorrectly)
        {
            this.colorIdCorrect = colorIdCorrect;
            this.colorIdCurrent = colorIdCurrent;
            this.hasBeenPaintedCorrectly = hasBeenPaintedCorrectly;
        }
    }
}
