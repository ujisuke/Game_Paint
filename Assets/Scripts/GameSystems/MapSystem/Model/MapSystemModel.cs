using Assets.Scripts.Datas;

namespace Assets.Scripts.GameSystems.MapSystem.Model
{
    public class MapSystemModel
    {
        private readonly StageSelecter stageSelecter;
        public MapSystemModel(StageOnMapDataList mapDataList)
        {
            stageSelecter = new StageSelecter(mapDataList);
        }

        public void Dispose()
        {
            stageSelecter?.Dispose();
        }

        public void ChangeStageTo(MoveDirOnMap moveDir) => stageSelecter.ChangeStageTo(moveDir);
    }
}
