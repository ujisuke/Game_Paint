using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    public class TilesData
    {
        private const uint tilesWidth = 18;
        private const uint tilesHeight = 10;
        private readonly Tile[,] tiles;

        public TilesData(Tilemap tilemap)
        {
            tiles = InitializeTiles(tilemap);
        }

        private Tile[,] InitializeTiles(Tilemap tilemap)
        {
            Tile[,] newTiles = new Tile[tilesWidth, tilesHeight];

            for (int x = 0; x < tilesWidth; x++)
                for (int y = 0; y < tilesHeight; y++)
                {
                    Vector3Int position = new(x, y, 0);
                    TileBase tileBase = tilemap.GetTile(position);
                    newTiles[x, y] = new(
                        colorIdCorrect: 0, // Placeholder for correct color ID
                        colorIdCurrent: 0, // Placeholder for current color ID
                        hasBeenPaintedCorrectly: false // Placeholder for painted state
                    );
                    Debug.Log($"({x}, {y})");
                }
            return newTiles;
        }
    }
}
