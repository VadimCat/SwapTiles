using Models.Interaction;

namespace Input.Actions.Rotation
{
 public static class ExclusiveActionFluentExtension
 {
  public static IAction Lockable(this IAction action, InputLocker inputLocker,
   RotationLockSource rotationLockSource)
  {
   return new InputExclusiveAction(action, inputLocker, rotationLockSource);
  }
 }
}