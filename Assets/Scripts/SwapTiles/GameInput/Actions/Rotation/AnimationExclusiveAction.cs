using Ji2.Presenters;

namespace SwapTiles.GameInput.Actions.Rotation
{
 public class AnimationExclusiveAction : IAction
 {
  private readonly IAction _action;
  private readonly AnimationQueue _animationQueue;

  public AnimationExclusiveAction(IAction action, AnimationQueue animationQueue)
  {
   _action = action;
   _animationQueue = animationQueue;
  }
  
  public void Act(InputResult inputResult)
  {
   if (_animationQueue.IsFree)
   {
    _action.Act(inputResult);
   }
  }
 }

 public static class AnimationExclusiveActionFluentExtension
 {
  public static IAction AnimationExclusive(this IAction action, AnimationQueue animationQueue)
  {
   return new AnimationExclusiveAction(action, animationQueue);
  }
 }
}