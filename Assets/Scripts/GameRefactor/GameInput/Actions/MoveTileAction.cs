using Tiles;
using Models.Interaction;

namespace Input.Actions
{
 public class MoveTileAction : IAction
 {
  private readonly ScreenSpacePlane _screenSpacePlane;
  private readonly InputLocker _locker;

  public MoveTileAction(ScreenSpacePlane screenSpacePlane, InputLocker locker)
  {
   _screenSpacePlane = screenSpacePlane;
   _locker = locker;
  }

  public void Act(InputResult inputResult)
  {
   inputResult.Target.gameObject.transform.position = _screenSpacePlane.GetWorldPositionOnPlane(inputResult.Pos);
  }
 }
}