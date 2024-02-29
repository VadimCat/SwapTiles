using System.Collections.Generic;

namespace GameRefactor.GameInput
{
 public class SwapInputSwipe : IInputAction
 {
  private readonly IReadOnlyCollection<(ISpecification spec, IAction action)> _expectedInputQueue;
  private Queue<(ISpecification spec, IAction action)> CurrentQueue;

  public SwapInputSwipe(SpecActionsRepository specActionsRepository)
  {
   var specificationsQueue = new List<(ISpecification spec, IAction action)>()
   {
    (specActionsRepository.GetSpec<BeganHasTarget>(), specActionsRepository.GetAction<SpecActionsRepository>())
   };
   _expectedInputQueue = specificationsQueue;
   Reset();
  }

  public IAction NextAction => CurrentQueue.Peek().action;

  public bool CanHandle(InputResult inputResult)
  {
   return CurrentQueue.Peek().spec.IsMatching(inputResult);
  }

  public void Handle(InputResult inputResult)
  {
   var peek = CurrentQueue.Dequeue();
   peek.action.Act(inputResult);
  }

  public void Reset()
  {
   CurrentQueue = new Queue<(ISpecification spec, IAction action)>(_expectedInputQueue);
  }
 }
}