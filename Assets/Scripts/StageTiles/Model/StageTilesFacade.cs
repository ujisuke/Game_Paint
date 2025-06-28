using Assets.Scripts.Datas;
using UnityEngine;
using Assets.Scripts.StageTiles.View;


namespace Assets.Scripts.StageTiles.Model
{
    public class StageTilesFacade
    {
        public static StageTilesFacade Instance;
        private readonly StageTilesData stageTilesData;
        public StageTile[,] StageTiles => stageTilesData.StageTiles;
        public static int TilesWidth => StageTilesData.TilesWidth;
        public static int TilesHeight => StageTilesData.TilesHeight;

        public StageTilesFacade(StageTileView[,] stageTileViews)
        {
            stageTilesData = new StageTilesData(stageTileViews);
            Instance = this;
        }

        public void PaintTile(Vector2 pos, ColorName inputColorName)
        {
            stageTilesData.PaintTile(pos, inputColorName);
        }

        public void CompletePaint()
        {
            stageTilesData.CompletePaint();
        }
    }
}
