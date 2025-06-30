using Assets.Scripts.Datas;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.StageTiles.View
{
    public class StageTilesView
    {
        private readonly StageTileView[,] stageTileViews;
        public StageTileView[,] StageTileViews => stageTileViews;

        public StageTilesView(Tilemap tilemap, int tilesWidth, int tilesHeight)
        {
            stageTileViews = InitializeTiles(tilemap, tilesWidth, tilesHeight);
        }

        private static StageTileView[,] InitializeTiles(Tilemap tilemap, int tilesWidth, int tilesHeight)
        {
            StageTileView[,] newTileViews = new StageTileView[tilesWidth, tilesHeight];

            for (int i = 0; i < tilemap.gameObject.transform.childCount; i++)
            {
                Transform tileObject = tilemap.gameObject.transform.GetChild(i);
                newTileViews[(int)tileObject.position.x, (int)tileObject.position.y] =
                    tileObject.GetComponent<StageTileView>();
            }
            return newTileViews;
        }

        public void PaintTile(Vector2Int posInt, ColorName newColorName) =>
            stageTileViews[posInt.x, posInt.y].Paint(newColorName);
            
        public void ResetTileView(Vector2Int posInt) =>
            stageTileViews[posInt.x, posInt.y].Reset();
    }
}
