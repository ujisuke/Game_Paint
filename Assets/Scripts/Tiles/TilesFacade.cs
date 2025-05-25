using UnityEngine;
using UnityEngine.Tilemaps;


namespace Assets.Scripts.Tiles
{
    public class TilesFacade
    {
        private readonly TilesData tilesData;

        public TilesFacade(Tilemap tilemap)
        {
            tilesData = new TilesData(tilemap);
        }
    }
}
