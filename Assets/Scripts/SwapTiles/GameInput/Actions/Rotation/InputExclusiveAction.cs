using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Actions.Rotation
{
 public class InputExclusiveAction: IAction
 {
  private readonly InputLocker _inputLocker;
  private readonly RotationLockSource _rotationLockSource;
  private readonly IAction _action;

  public InputExclusiveAction(IAction action, InputLocker inputLocker,
   RotationLockSource rotationLockSource)
  {
   _action = action;
   _inputLocker = inputLocker;
   _rotationLockSource = rotationLockSource;
  }
  
  public void Act(InputResult inputResult)
  {
   if (_inputLocker.TryLock(_rotationLockSource))
   {
    _action.Act(inputResult);
   }
  }
 }
}