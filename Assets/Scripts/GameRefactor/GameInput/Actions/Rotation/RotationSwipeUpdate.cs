using System.Linq;
using Models.Interaction;

namespace Input.Actions.Rotation
{
 public class RotationSwipeUpdate: IAction
 {
  private readonly CurrentSelection _currentSelection;

  public RotationSwipeUpdate(CurrentSelection currentSelection)
  {
   _currentSelection = currentSelection;
  }
  public void Act(InputResult inputResult)
  {
   _currentSelection.AllSelected().First().Get<SwipeRotation>().UpdateSwipe(inputResult.Pos);
  }
 }
}