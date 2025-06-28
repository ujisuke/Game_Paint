using Assets.Scripts.Datas;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles.View
{
    public class TilesView
    {
        private readonly TileView[,] tileViews;
        public TileView[,] TileViews => tileViews;

        public TilesView(Tilemap tilemap, int tilesWidth, int tilesHeight)
        {
            tileViews = InitializeTiles(tilemap, tilesWidth, tilesHeight);
        }

        private static TileView[,] InitializeTiles(Tilemap tilemap, int tilesWidth, int tilesHeight)
        {
            TileView[,] newTileViews = new TileView[tilesWidth, tilesHeight];

            for (int i = 0; i < tilemap.gameObject.transform.childCount; i++)
            {
                Transform tileObject = tilemap.gameObject.transform.GetChild(i);
                newTileViews[(int)tileObject.position.x, (int)tileObject.position.y] = 
                    tileObject.GetComponent<TileView>();
            }
            return newTileViews;
        }

        public void PaintTile(Vector2 pos, ColorName newColorName) =>
            tileViews[(int)pos.x, (int)pos.y].Paint(newColorName);
    }
}
