using System;
using System.Collections.Generic;
using Ji2;
using Ji2.CommonCore.SaveDataContainer;
using Ji2.Context;
using Ji2.Presenters.Tutorial;
using Ji2.States;
using Ji2Core.Core.ScreenNavigation;
using SwapTiles.Game.Level;

namespace SwapTiles.States
{
    public class GameStatesFactory : IStateFactory
    {
        private readonly IDependenciesProvider _dp;

        public GameStatesFactory(IDependenciesProvider dp)
        {
            _dp = dp;
        }

        public Dictionary<Type, IExitableState> GetStates(StateMachine stateMachine)
        {
            var screenNavigator = _dp.Get<ScreenNavigator>();
            var sceneNavigation = _dp.Get<SceneNavigation>();
            var dict = new Dictionary<Type, IExitableState>
            {
                [typeof(InitialState)] = new InitialState(stateMachine, screenNavigator,
                    _dp.Get<TutorialService>(), _dp.Get<ISave>(), 
                    _dp.Get<LevelsConfig>()),
                [typeof(GameState)] = new GameState(stateMachine, screenNavigator),
                [typeof(LevelSelectionState)] =
                    new LevelSelectionState(stateMachine, screenNavigator, sceneNavigation)
            };

            return dict;
        }
    }
}