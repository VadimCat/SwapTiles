using Cysharp.Threading.Tasks;
using Ji2;
using Ji2.States;
using Ji2Core.Core.ScreenNavigation;
using SwapTiles.UI.Screens;

namespace SwapTiles.States
{
 public class LevelSelectionState: IState
 {
  private readonly StateMachine _stateMachine;
  private readonly ScreenNavigator _screenNavigator;
  private readonly SceneNavigation _sceneNavigation;

  public LevelSelectionState(StateMachine stateMachine, ScreenNavigator screenNavigator,
   SceneNavigation sceneNavigation)
  {
   _stateMachine = stateMachine;
   _screenNavigator = screenNavigator;
   _sceneNavigation = sceneNavigation;
  }
  
  public async UniTask Enter()
  {
   await _screenNavigator.PushScreen<LevelSelectionScreen>();
   
  }

  public async UniTask Exit()
  {
   await _screenNavigator.CloseScreen<LevelSelectionScreen>();
  }
 }
}