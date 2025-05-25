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
                    if (tileBase == null)
                        newTiles[x, y] = new(
                            colorIdCorrect: 0,
                            colorIdCurrent: -1,
                            hasBeenPaintedCorrectly: false
                        );
                    else
                        newTiles[x, y] = new(
                            colorIdCorrect: 0,
                            colorIdCurrent: 0,
                            hasBeenPaintedCorrectly: false
                        );
                }
            return newTiles;
        }
    }
}
