using Assets.Scripts.Datas;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tiles
{
    public class TilesController : MonoBehaviour
    {
        private TilesFacade tilesFacade;
        [SerializeField] private Tilemap tilemap;
        private TilesView tilesView;
        public static TilesController Instance;

        private void Awake()
        {
            Instance = this;
            tilesView = new TilesView(tilemap, TilesData.TilesWidth, TilesData.TilesHeight);
            tilesFacade = new TilesFacade(tilesView.TileViews);
        }

        public void PaintTileView(Vector2 playerPos, ColorName newColorName) =>
            tilesView.PaintTile(playerPos, newColorName);
    }
}
