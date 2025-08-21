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
        

        public StageTilesModel(StageTileView[,] stageTileViews, SummonDataList signDataList)
        {
            stageTilesData = new StageTilesData(stageTileViews, signDataList);
            Instance = this;
        }

        public void PaintTile(Vector2 pos, ColorName colorNameInput, bool isByEnemy) => stageTilesData.PaintTile(pos, colorNameInput, isByEnemy);

        public void CompletePaint(bool isByEnemy) => stageTilesData.CompletePaint(isByEnemy);
    }
}
