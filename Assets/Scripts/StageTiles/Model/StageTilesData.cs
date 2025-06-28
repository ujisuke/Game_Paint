using Assets.Scripts.Datas;
using UnityEngine;
using Assets.Scripts.StageTiles.View;
using Assets.Scripts.StageTiles.Controller;

namespace Assets.Scripts.StageTiles.Model
{
    public class StageTilesData
    {
        private const int tilesWidth = 18;
        public static int TilesWidth => tilesWidth;
        private const int tilesHeight = 10;
        public static int TilesHeight => tilesHeight;
        private readonly StageTile[,] stageTiles;
        public StageTile[,] StageTiles => stageTiles;

        public StageTilesData(StageTileView[,] stageTileViews)
        {
            stageTiles = InitializeTiles(stageTileViews);
        }

        private static StageTile[,] InitializeTiles(StageTileView[,] stageTileViews)
        {
            StageTile[,] newStageTiles = new StageTile[tilesWidth, tilesHeight];

            for (int x = 0; x < tilesWidth; x++)
                for (int y = 0; y < tilesHeight; y++)
                    if (stageTileViews[x, y].ColorNameCorrect == ColorName.wallColor)
                        newStageTiles[x, y] = new StageTile(
                            colorNameCorrect: ColorName.wallColor,
                            colorNameCurrent: ColorName.wallColor,
                            colorNamePrev: ColorName.wallColor);
                    else
                        newStageTiles[x, y] = new StageTile(
                            colorNameCorrect: stageTileViews[x, y].ColorNameCorrect,
                            colorNameCurrent: ColorName.defaultColor,
                            colorNamePrev: ColorName.defaultColor);
            return newStageTiles;
        }

        public void PaintTile(Vector2 pos, ColorName inputColorName)
        {
            Vector2Int posInt = Vector2Int.FloorToInt(pos);
            stageTiles[posInt.x, posInt.y] = stageTiles[posInt.x, posInt.y].Paint(inputColorName);
            if (stageTiles[posInt.x, posInt.y].IsUpdated())
                StageTilesController.Instance.PaintTileView(pos, stageTiles[posInt.x, posInt.y].ColorNameCurrent);
        }
    }
}
