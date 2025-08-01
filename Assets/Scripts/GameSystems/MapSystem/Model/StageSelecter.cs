using System;
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
        private static readonly Dictionary<MoveDirOnMap, MoveDirOnMap> oppositeDirDictionary = new()
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

        private async UniTask Process(float waitSeconds)
        {
            isProcessing = true;
            await UniTask.Delay(TimeSpan.FromSeconds(waitSeconds), cancellationToken: cancellationTokenSource.Token);
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
            if (currentStageData.IsToNextDir(moveDirOnMap))
            {
                Process(currentStageData.MoveSecondsToNext).Forget();
                currentStageData = currentStageData.NextStageData;
            }
            else if (currentStageData.PrevStageData?.IsToPrevDir(oppositeDirDictionary[moveDirOnMap]) == true)
            {
                Process(currentStageData.PrevStageData.MoveSecondsToNext).Forget();
                currentStageData = currentStageData.PrevStageData;
            }
            CurrentStageSceneName = currentStageData.StageSceneName;
            Debug.Log($"Changed to stage: {currentStageData.StageSceneName}");            
        }
    }
}
