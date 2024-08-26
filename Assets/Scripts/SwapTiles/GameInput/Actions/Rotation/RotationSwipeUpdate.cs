using System.Linq;
using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Actions.Rotation
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