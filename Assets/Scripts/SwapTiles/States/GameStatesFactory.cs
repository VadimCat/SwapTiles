using System;
using System.Collections.Generic;
using Client.States;
using Ji2.Context;
using Ji2.Presenters.Tutorial;
using Ji2.States;
using Ji2Core.Core.ScreenNavigation;

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
            var dict = new Dictionary<Type, IExitableState>
            {
                [typeof(InitialState)] = new InitialState(stateMachine, screenNavigator,
                    _dp.Get<TutorialService>()),
                [typeof(GameState)] = new GameState(stateMachine, screenNavigator),
            };

            return dict;
        }
    }
}