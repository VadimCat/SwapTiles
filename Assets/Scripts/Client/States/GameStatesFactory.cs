﻿using System;
using System.Collections.Generic;
using Client.Models;
using Client.Presenters;
using Ji2;
using Ji2.Audio;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Context;
using Ji2.Presenters.Tutorial;
using Ji2.States;
using Ji2Core.Core.ScreenNavigation;
using UI.Background;

namespace Client.States
{
    public class GameStatesFactory : IStateFactory
    {
        private readonly IDependenciesProvider _dp;

        public GameStatesFactory(IDependenciesProvider dp)
        {
            this._dp = dp;
        }

        public Dictionary<Type, IExitableState> GetStates(StateMachine stateMachine)
        {
            var screenNavigator = _dp.GetService<ScreenNavigator>();
            var dict = new Dictionary<Type, IExitableState>
            {
                [typeof(InitialState)] = new InitialState(stateMachine, screenNavigator,
                    _dp.GetService<TutorialService>(),
                    _dp.GetService<LevelsLoopProgress>(), _dp.GetService<ISaveDataContainer>()),
                [typeof(LoadLevelState)] = new LoadLevelState(_dp, stateMachine, _dp.GetService<SceneLoader>(),
                    screenNavigator, _dp.GetService<LevelsConfig>(), _dp.GetService<BackgroundService>(),
                    _dp.GetService<LevelFactory>(), _dp.GetService<LevelPresenterFactory>()),
                [typeof(GameState)] = new GameState(stateMachine, screenNavigator),
                [typeof(LevelCompletedState)] = new LevelCompletedState(stateMachine, screenNavigator,
                    _dp.GetService<LevelsLoopProgress>(), _dp.GetService<LevelsConfig>(), _dp.GetService<Sound>())
            };

            return dict;
        }
    }
}