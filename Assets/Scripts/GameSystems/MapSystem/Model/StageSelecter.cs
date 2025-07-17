using System.Threading;
using Assets.Scripts.Datas;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameSystems.MapSystem.Model
{
    public class StageSelecter
    {
        private StageOnMapData currentStageData;
        public static string CurrentStageSceneName;
        private bool isProcessing;
        private readonly CancellationTokenSource cancellationTokenSource;


        public StageSelecter(StageOnMapDataList stageOnMapDataList)
        {
            CurrentStageSceneName ??= stageOnMapDataList.FirstStageName;
            currentStageData = stageOnMapDataList.GetMapData(CurrentStageSceneName);
            isProcessing = false;
            cancellationTokenSource = new();
        }

        private async UniTask Process()
        {
            isProcessing = true;
            await UniTask.Delay(250, cancellationToken: cancellationTokenSource.Token);
            isProcessing = false;
        }

        public void Dispose()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        public void ChangeStageToUp()
        {
            if (isProcessing)
                return;
            Process().Forget();
            if (currentStageData.MoveToNext == MoveDirOnMap.Up)
                currentStageData = currentStageData.NextStageData;
            else if (currentStageData.PrevStageData?.MoveToNext == MoveDirOnMap.Down)
                currentStageData = currentStageData.PrevStageData;

            Debug.Log($"Changed to stage: {currentStageData.StageSceneName}");
        }

        public void ChangeStageToDown()
        {
            if (isProcessing)
                return;
            Process().Forget();
            if (currentStageData.MoveToNext == MoveDirOnMap.Down)
                currentStageData = currentStageData.NextStageData;
            else if (currentStageData.PrevStageData?.MoveToNext == MoveDirOnMap.Up)
                currentStageData = currentStageData.PrevStageData;
            Debug.Log($"Changed to stage: {currentStageData.StageSceneName}");
        }

        public void ChangeStageToLeft()
        {
            if (isProcessing)
                return;
            Process().Forget();
            if (currentStageData.MoveToNext == MoveDirOnMap.Left)
                currentStageData = currentStageData.NextStageData;
            else if (currentStageData.PrevStageData?.MoveToNext == MoveDirOnMap.Right)
                currentStageData = currentStageData.PrevStageData;
            Debug.Log($"Changed to stage: {currentStageData.StageSceneName}");    
        }

        public void ChangeStageToRight()
        {
            if (isProcessing)
                return;
            Process().Forget();
            if (currentStageData.MoveToNext == MoveDirOnMap.Right)
                currentStageData = currentStageData.NextStageData;
            else if (currentStageData.PrevStageData?.MoveToNext == MoveDirOnMap.Left)
                currentStageData = currentStageData.PrevStageData;
            Debug.Log($"Changed to stage: {currentStageData.StageSceneName}");            
        }
    }
}
