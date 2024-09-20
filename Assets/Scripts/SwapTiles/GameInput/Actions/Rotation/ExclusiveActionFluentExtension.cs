using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Actions.Rotation
{
 public static class ExclusiveActionFluentExtension
 {
  public static IAction Lockable(this IAction action, InputLock inputLock,
   RotationLockSource rotationLockSource)
  {
   return new InputExclusiveAction(action, inputLock, rotationLockSource);
  }
 }
}