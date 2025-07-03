using Assets.Scripts.Datas;
using UnityEngine;
using Assets.Scripts.StageTiles.View;


namespace Assets.Scripts.StageTiles.Model
{
    public class StageTilesModel
    {
        public static StageTilesModel Instance;
        private readonly StageTilesData stageTilesData;
        public StageTile[,] StageTiles => stageTilesData.StageTiles;
        public static int TilesWidth => StageTilesData.TilesWidth;
        public static int TilesHeight => StageTilesData.TilesHeight;
        

        public StageTilesModel(StageTileView[,] stageTileViews, SignDataList signDataList)
        {
            stageTilesData = new StageTilesData(stageTileViews, signDataList);
            Instance = this;
        }

        public void PaintTile(Vector2 pos, ColorName colorNameInput)
        {
            stageTilesData.PaintTile(pos, colorNameInput);
        }

        public void CompletePaint(ColorName colorNameInput)
        {
            stageTilesData.CompletePaint(colorNameInput);
        }
    }
}
