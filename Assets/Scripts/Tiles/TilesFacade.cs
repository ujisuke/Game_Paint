using UnityEngine;
using UnityEngine.Tilemaps;


namespace Assets.Scripts.Tiles
{
    public class TilesFacade
    {
        public static TilesFacade Instance;
        private readonly TilesData tilesData;
        public Tile[,] Tiles => tilesData.Tiles;

        public TilesFacade(Tilemap tilemap)
        {
            tilesData = new TilesData(tilemap);
            Instance = this;
        }
    }
}
