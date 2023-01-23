﻿using Client.Models;
using Client.UI.Screens;
using Cysharp.Threading.Tasks;
using Ji2Core.Core.ScreenNavigation;
using Ji2Core.Core.States;

namespace Client.States
{
    public class LevelCompletedState : IPayloadedState<LevelCompletedPayload>
    {
        private readonly StateMachine stateMachine;
        private readonly ScreenNavigator screenNavigator;
        private readonly LevelsLoopProgress levelsLoopProgress;
        private readonly LevelsConfig levelsConfig;

        public LevelCompletedState(StateMachine stateMachine, ScreenNavigator screenNavigator,
            LevelsLoopProgress levelsLoopProgress, LevelsConfig levelsConfig)
        {
            this.stateMachine = stateMachine;
            this.screenNavigator = screenNavigator;
            this.levelsLoopProgress = levelsLoopProgress;
            this.levelsConfig = levelsConfig;
        }

        public async UniTask Enter(LevelCompletedPayload payload)
        {
            var screen = await screenNavigator.PushScreen<LevelCompletedScreen>();
            var levelName = payload.level.Name;
            var levelViewConfig = levelsConfig.GetData(levelName);
            var levelResult = levelViewConfig.Image;
            screen.SetLevelResult(levelResult, payload.level.LevelCount);
            screen.ClickNext += OnClickNext;
        }

        private void OnClickNext()
        {
            var levelData = levelsLoopProgress.GetNextLevelData();
            stateMachine.Enter<LoadLevelState, LoadLevelStatePayload>(new LoadLevelStatePayload(levelData, .5f));
        }

        public async UniTask Exit()
        {
            await screenNavigator.CloseScreen<LevelCompletedScreen>();
        }
    }
    

    public class LevelCompletedPayload
    {
        public Level level;
    }
}