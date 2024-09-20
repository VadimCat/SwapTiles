using System.Linq;
using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Actions.Rotation
{
 public class EndRotationSwipe: IAction
 {
  private readonly InputLock _inputLock;
  private readonly RotationLockSource _rotationLockSource;
  private readonly CurrentSelection _currentSelection;
  private readonly SwipeRotation _swipeRotation;

  public EndRotationSwipe(InputLock inputLock, RotationLockSource rotationLockSource, CurrentSelection currentSelection)
  {
   _inputLock = inputLock;
   _rotationLockSource = rotationLockSource;
   _currentSelection = currentSelection;
  }
  
  public void Act(InputResult inputResult)
  {
   _inputLock.Unlock(_rotationLockSource);
   _currentSelection.AllSelected().First().Get<SwipeRotation>().Reset();
  }
 }
}