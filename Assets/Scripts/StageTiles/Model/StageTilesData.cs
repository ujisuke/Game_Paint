using Assets.Scripts.Datas;
using UnityEngine;
using Assets.Scripts.StageTiles.View;
using Assets.Scripts.StageTiles.Controller;
using System.Collections.Generic;

namespace Assets.Scripts.StageTiles.Model
{
    public class StageTilesData
    {
        private readonly StageTile[,] stageTiles;
        public StageTile[,] StageTiles => stageTiles;
        private readonly List<Vector2Int> paintedPosIntByPlayerList;
        private readonly List<Vector2Int> paintedPosIntByEnemyList;
        private readonly SummonDataList signDataList;
        private ColorName colorNamePlayer;
        private ColorName colorNameEnemy;

        public StageTilesData(StageTileView[,] stageTileViews, SummonDataList signDataList)
        {
            this.signDataList = signDataList;
            stageTiles = InitializeTiles(stageTileViews);
            paintedPosIntByPlayerList = new();
            paintedPosIntByEnemyList = new();
        }

        private static StageTile[,] InitializeTiles(StageTileView[,] stageTileViews)
        {
            int tilesWidth = StageData.Instance.Width;
            int tilesHeight = StageData.Instance.Height;
            StageTile[,] newStageTiles = new StageTile[tilesWidth, tilesHeight];

            for (int x = 0; x < tilesWidth; x++)
                for (int y = 0; y < tilesHeight; y++)
                    if (stageTileViews[x, y].ColorNameCorrect == ColorName.wallColor)
                        newStageTiles[x, y] = StageTile.Initialize(ColorName.wallColor, true);
                    else
                        newStageTiles[x, y] = StageTile.Initialize(ColorName.defaultTileColor, false);
            return newStageTiles;
        }

        public void PaintTile(Vector2 pos, ColorName colorNameInput, bool isByEnemy)
        {
            Vector2Int posInt = Vector2Int.FloorToInt(pos);
            StageTile targetStageTile = stageTiles[posInt.x, posInt.y];
            StageTile newTargetStageTile = targetStageTile.Paint(colorNameInput);
            if (newTargetStageTile.ColorNameCurrent == targetStageTile.ColorNameCurrent)
                return;
            stageTiles[posInt.x, posInt.y] = newTargetStageTile;
            StageTilesController.Instance.PaintTileView(posInt, newTargetStageTile.ColorNameCurrent);
            if (isByEnemy)
            {
                paintedPosIntByEnemyList.Add(posInt);
                colorNameEnemy = colorNameInput;
            }
            else
            {
                paintedPosIntByPlayerList.Add(posInt);
                colorNamePlayer = colorNameInput;
            }
        }

        public void CompletePaint(bool isByEnemy)
        {
            List<Vector2Int> paintedPosIntList = isByEnemy ? paintedPosIntByEnemyList : paintedPosIntByPlayerList;
            signDataList.Summon(paintedPosIntList, isByEnemy ? colorNameEnemy : colorNamePlayer, isByEnemy);
            for (int i = 0; i < paintedPosIntList.Count; i++)
            {
                StageTilesController.Instance.ResetTileView(paintedPosIntList[i]);
                StageTile targetStageTile = stageTiles[paintedPosIntList[i].x, paintedPosIntList[i].y];
                stageTiles[paintedPosIntList[i].x, paintedPosIntList[i].y] = targetStageTile.ResetColor();
            }
            if (isByEnemy)
                paintedPosIntByEnemyList.Clear();
            else
                paintedPosIntByPlayerList.Clear();
        }
    }
}
