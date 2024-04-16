using System.Linq;
using Models.Interaction;

namespace Input.Actions.Rotation
{
 public class EndRotationSwipe: IAction
 {
  private readonly InputLocker _inputLocker;
  private readonly RotationLockSource _rotationLockSource;
  private readonly CurrentSelection _currentSelection;
  private readonly SwipeRotation _swipeRotation;

  public EndRotationSwipe(InputLocker inputLocker, RotationLockSource rotationLockSource, CurrentSelection currentSelection)
  {
   _inputLocker = inputLocker;
   _rotationLockSource = rotationLockSource;
   _currentSelection = currentSelection;
  }
  
  public void Act(InputResult inputResult)
  {
   _inputLocker.Unlock(_rotationLockSource);
   _currentSelection.AllSelected().First().Get<SwipeRotation>().Reset();
  }
 }
}