using Cysharp.Threading.Tasks;
using Ji2.States;
using Ji2Core.Core.ScreenNavigation;
using SwapTiles.UI.Screens;
using UI.Background;

namespace Client.States
{
    public class GameState : IPayloadedState<GameStatePayload>
    {
        private readonly StateMachine _stateMachine;
        private readonly ScreenNavigator _screenNavigator;
        private readonly BackgroundService _backgroundService;

        private GameStatePayload _payload;
        
        public GameState(StateMachine stateMachine, ScreenNavigator screenNavigator)
        {
            _stateMachine = stateMachine;
            _screenNavigator = screenNavigator;
        }

        public GameStatePayload Payload => _payload;

        public async UniTask Enter(GameStatePayload payload)
        {
            _payload = payload;
            await _screenNavigator.PushScreen<LevelScreen>();
        }

        public async UniTask Exit()
        {
            
            await _screenNavigator.CloseScreen<LevelScreen>();
        }
    }
}