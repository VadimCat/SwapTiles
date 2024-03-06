using GameRefactor.Game;

namespace GameRefactor.GameInput.Actions
{
 public class MoveTileAction : IAction
 {
  private readonly ScreenSpacePlane _screenSpacePlane;

  public MoveTileAction(ScreenSpacePlane screenSpacePlane)
  {
   _screenSpacePlane = screenSpacePlane;
  }

  public void Act(InputResult inputResult)
  {
   inputResult.Target.gameObject.transform.position = _screenSpacePlane.GetWorldPositionOnPlane(inputResult.Pos);
  }
 }
}