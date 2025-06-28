using Assets.Scripts.Datas;
using UnityEngine;
using Assets.Scripts.Tiles.View;


namespace Assets.Scripts.Tiles.Model
{
    public class TilesFacade
    {
        public static TilesFacade Instance;
        private readonly TilesData tilesData;
        public Tile[,] Tiles => tilesData.Tiles;
        public static int TilesWidth => TilesData.TilesWidth;
        public static int TilesHeight => TilesData.TilesHeight;

        public TilesFacade(TileView[,] tileViews)
        {
            tilesData = new TilesData(tileViews);
            Instance = this;
        }

        public void PaintTile(Vector2 pos, ColorName inputColorName)
        {
            tilesData.PaintTile(pos, inputColorName);
        }
    }
}
