using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Actions.Rotation
{
 public class InputExclusiveAction: IAction
 {
  private readonly InputLock _inputLock;
  private readonly RotationLockSource _rotationLockSource;
  private readonly IAction _action;

  public InputExclusiveAction(IAction action, InputLock inputLock,
   RotationLockSource rotationLockSource)
  {
   _action = action;
   _inputLock = inputLock;
   _rotationLockSource = rotationLockSource;
  }
  
  public void Act(InputResult inputResult)
  {
   if (_inputLock.TryLock(_rotationLockSource))
   {
    _action.Act(inputResult);
   }
  }
 }
}