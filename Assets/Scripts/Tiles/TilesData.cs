using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    public class TilesData
    {
        private const uint tilesWidth = 18;
        private const uint tilesHeight = 10;
        private readonly Tile[,] tiles;
        public Tile[,] Tiles => tiles;

        public TilesData(Tilemap tilemap)
        {
            tiles = InitializeTiles(tilemap);
        }

        private static Tile[,] InitializeTiles(Tilemap tilemap)
        {
            Tile[,] newTiles = new Tile[tilesWidth, tilesHeight];

            for (int i = 0; i < tilemap.gameObject.transform.childCount; i++)
            {
                Transform tileObject = tilemap.gameObject.transform.GetChild(i);
                newTiles[(int)tileObject.position.x, (int)tileObject.position.y] = new(
                    colorIdCorrect: 0,
                    colorIdCurrent: 0,
                    hasBeenPaintedCorrectly: false
                );
            }

            for (int x = 0; x < tilesWidth; x++)
                for (int y = 0; y < tilesHeight; y++)
                    if (newTiles[x, y] == null)
                        newTiles[x, y] = new(
                            colorIdCorrect: -1,
                            colorIdCurrent: -1,
                            hasBeenPaintedCorrectly: false
                        );
            return newTiles;
        }
    }
}
