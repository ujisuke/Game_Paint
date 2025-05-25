using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class Tile
    {
        private readonly byte colorIdCorrect;
        private readonly byte colorIdCurrent;
        private readonly bool hasBeenPaintedCorrectly;

        public Tile(byte colorIdCorrect, byte colorIdCurrent, bool hasBeenPaintedCorrectly)
        {
            this.colorIdCorrect = colorIdCorrect;
            this.colorIdCurrent = colorIdCurrent;
            this.hasBeenPaintedCorrectly = hasBeenPaintedCorrectly;
        }
    }
}
