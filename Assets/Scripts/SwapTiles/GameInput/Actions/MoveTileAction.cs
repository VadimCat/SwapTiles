using SwapTiles.Game;
using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Actions
{
 public class MoveTileAction : IAction
 {
  private readonly ScreenSpacePlane _screenSpacePlane;
  private readonly InputLock _lock;

  public MoveTileAction(ScreenSpacePlane screenSpacePlane, InputLock @lock)
  {
   _screenSpacePlane = screenSpacePlane;
   _lock = @lock;
  }

  public void Act(InputResult inputResult)
  {
   inputResult.Target.gameObject.transform.position = _screenSpacePlane.GetWorldPositionOnPlane(inputResult.Pos);
  }
 }
}