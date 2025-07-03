using Assets.Scripts.Datas;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.StageTiles.Model;
using Assets.Scripts.StageTiles.View;

namespace Assets.Scripts.StageTiles.Controller
{
    public class StageTilesController : MonoBehaviour
    {
        private StageTilesModel stageTilesModel;
        [SerializeField] private Tilemap tilemap;
        private StageTilesView stageTilesView;
        public static StageTilesController Instance;
        [SerializeField] private SignDataList signDataList;

        private void Awake()
        {
            Instance = this;
            stageTilesView = new StageTilesView(tilemap, StageTilesData.TilesWidth, StageTilesData.TilesHeight);
            stageTilesModel = new StageTilesModel(stageTilesView.StageTileViews, signDataList);
        }

        public void PaintTileView(Vector2Int playerPosInt, ColorName newColorName) =>
            stageTilesView.PaintTile(playerPosInt, newColorName);
        
        public void ResetTileView(Vector2Int posInt) =>
            stageTilesView.ResetTileView(posInt);
    }
}
