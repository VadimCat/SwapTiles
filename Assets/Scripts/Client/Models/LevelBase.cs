﻿using System.Collections.Generic;
using Ji2Core.Core.Analytics;
using Ji2Core.Core.SaveDataContainer;
using Models;
using UnityEngine;

namespace Client.Models
{
    public abstract class LevelBase
    {
        private readonly Analytics analytics;
        private readonly LevelData levelData;
        protected readonly ISaveDataContainer saveDataContainer;
        private float playTime = 0;

        public string Name => levelData.name;
        public int LevelCount => levelData.uniqueLevelNumber;
        
        protected LevelBase(Analytics analytics, LevelData levelData, ISaveDataContainer saveDataContainer)
        {
            this.analytics = analytics;
            this.levelData = levelData;
            this.saveDataContainer = saveDataContainer;
            
            CheckPlayTimeForAnalytics();
        }

        private void CheckPlayTimeForAnalytics()
        {
            playTime = saveDataContainer.GetValue<float>(Name);
            if (Mathf.Approximately(0, playTime))
            {
                LogAnalyticsLevelFinish(LevelExitType.game_closed);
                playTime = 0;
                saveDataContainer.ResetKey(Name);
            }
        }

        public void AppendPlayTime(float time)
        {
            playTime += time;
            saveDataContainer.SaveValue(Name, time);
        }

        public void LogAnalyticsLevelStart()
        {
            var eventData = new Dictionary<string, object>
            {
                [Constants.LevelNumberKey] = levelData.uniqueLevelNumber,
                [Constants.LevelNameKey] = levelData.name,
                [Constants.LevelCountKey] = levelData.levelCount,
                [Constants.LevelLoopKey] = levelData.lvlLoop,
                [Constants.LevelRandomKey] = levelData.isRandom,
            };
            analytics.LogEventDirectlyTo<YandexMetricaLogger>(Constants.StartEvent, eventData);
            analytics.ForceSendDirectlyTo<YandexMetricaLogger>();
        }
        
        public void LogAnalyticsLevelFinish(LevelExitType levelExitType = LevelExitType.win)
        {
            var eventData = new Dictionary<string, object>
            {
                [Constants.LevelNumberKey] = levelData.uniqueLevelNumber,
                [Constants.LevelNameKey] = levelData.name,
                [Constants.LevelCountKey] = levelData.levelCount,
                [Constants.LevelLoopKey] = levelData.lvlLoop,
                [Constants.LevelRandomKey] = levelData.isRandom,
                [Constants.ResultKey] = ((int)levelExitType).ToString(),
                [Constants.TimeKey] = playTime,
            };

            analytics.LogEventDirectlyTo<YandexMetricaLogger>(Constants.FinishEvent, eventData);
            analytics.ForceSendDirectlyTo<YandexMetricaLogger>();
        }
    }
}