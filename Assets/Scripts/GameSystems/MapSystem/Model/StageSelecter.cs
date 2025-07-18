using System.Collections.Generic;
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
        private static Dictionary<MoveDirOnMap, MoveDirOnMap> oppositeDirDictionary = new()
        {
            { MoveDirOnMap.Up, MoveDirOnMap.Down },
            { MoveDirOnMap.Down, MoveDirOnMap.Up },
            { MoveDirOnMap.Left, MoveDirOnMap.Right },
            { MoveDirOnMap.Right, MoveDirOnMap.Left }
        };


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

        public void ChangeStageTo(MoveDirOnMap moveDirOnMap)
        {
            if (isProcessing)
                return;
            Process().Forget();
            if (currentStageData.MoveToNext == moveDirOnMap)
                currentStageData = currentStageData.NextStageData;
            else if (currentStageData.PrevStageData?.MoveToNext == oppositeDirDictionary[moveDirOnMap])
                currentStageData = currentStageData.PrevStageData;
            CurrentStageSceneName = currentStageData.StageSceneName;
            Debug.Log($"Changed to stage: {currentStageData.StageSceneName}");            
        }
    }
}
