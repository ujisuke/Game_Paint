using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class TilesData
    {
        private const int tilesWidth = 18;
        public static int TilesWidth => tilesWidth;
        private const int tilesHeight = 10;
        public static int TilesHeight => tilesHeight;
        private readonly Tile[,] tiles;
        public Tile[,] Tiles => tiles;

        public TilesData(TileView[,] tileViews)
        {
            tiles = InitializeTiles(tileViews);
        }

        private static Tile[,] InitializeTiles(TileView[,] tileViews)
        {
            Tile[,] newTiles = new Tile[tilesWidth, tilesHeight];

            for (int x = 0; x < tilesWidth; x++)
                for (int y = 0; y < tilesHeight; y++)
                    if (tileViews[x, y].ColorNameCorrect == ColorName.wallColor)
                        newTiles[x, y] = new Tile(
                            colorNameCorrect: ColorName.wallColor,
                            colorNameCurrent: ColorName.wallColor,
                            colorNamePrev: ColorName.wallColor);
                    else
                        newTiles[x, y] = new Tile(
                            colorNameCorrect: tileViews[x, y].ColorNameCorrect,
                            colorNameCurrent: ColorName.defaultColor,
                            colorNamePrev: ColorName.defaultColor);
            return newTiles;
        }

        public void PaintTile(Vector2 pos, ColorName inputColorName)
        {
            Vector2Int posInt = Vector2Int.FloorToInt(pos);
            tiles[posInt.x, posInt.y] = tiles[posInt.x, posInt.y].Paint(inputColorName);
            if (tiles[posInt.x, posInt.y].IsUpdated())
                TilesController.Instance.PaintTileView(pos, tiles[posInt.x, posInt.y].ColorNameCurrent);
        }
    }
}
