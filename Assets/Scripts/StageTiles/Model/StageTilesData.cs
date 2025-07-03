using Assets.Scripts.Datas;
using UnityEngine;
using Assets.Scripts.StageTiles.View;
using Assets.Scripts.StageTiles.Controller;
using System.Collections.Generic;

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
        private readonly List<Vector2Int> paintedPosInts;
        private readonly SignDataList signDataList;

        public StageTilesData(StageTileView[,] stageTileViews, SignDataList signDataList)
        {
            this.signDataList = signDataList;
            stageTiles = InitializeTiles(stageTileViews);
            paintedPosInts = new();
        }

        private static StageTile[,] InitializeTiles(StageTileView[,] stageTileViews)
        {
            StageTile[,] newStageTiles = new StageTile[tilesWidth, tilesHeight];

            for (int x = 0; x < tilesWidth; x++)
                for (int y = 0; y < tilesHeight; y++)
                    if (stageTileViews[x, y].ColorNameCorrect == ColorName.wallColor)
                        newStageTiles[x, y] = StageTile.Initialize(ColorName.wallColor);
                    else
                        newStageTiles[x, y] = StageTile.Initialize(ColorName.defaultColor);
            return newStageTiles;
        }

        public void PaintTile(Vector2 pos, ColorName colorNameInput)
        {
            Vector2Int posInt = Vector2Int.FloorToInt(pos);
            StageTile targetStageTile = stageTiles[posInt.x, posInt.y];
            StageTile newTargetStageTile = targetStageTile.Paint(colorNameInput);
            if (newTargetStageTile.ColorNameCurrent == targetStageTile.ColorNameCurrent)
                return;
            stageTiles[posInt.x, posInt.y] = newTargetStageTile;
            StageTilesController.Instance.PaintTileView(posInt, newTargetStageTile.ColorNameCurrent);
            paintedPosInts.Add(posInt);
        }

        public void CompletePaint(ColorName colorNameInput)
        {
            signDataList.Summon(paintedPosInts, colorNameInput);
            for (int i = 0; i < paintedPosInts.Count; i++)
            {
                StageTilesController.Instance.ResetTileView(paintedPosInts[i]);
                StageTile targetStageTile = stageTiles[paintedPosInts[i].x, paintedPosInts[i].y];
                stageTiles[paintedPosInts[i].x, paintedPosInts[i].y] = targetStageTile.ResetColor();
            }
            paintedPosInts.Clear();
        }
    }
}
