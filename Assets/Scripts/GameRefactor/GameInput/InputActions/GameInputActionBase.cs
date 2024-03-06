using System.Collections.Generic;

namespace GameRefactor.GameInput.InputActions
{
 public abstract class GameInputActionBase
 {
  private Queue<(ISpecification spec, IAction action)> _currentQueue;
  private IReadOnlyCollection<(ISpecification spec, IAction action)> _expectedInputQueue;

  protected GameInputActionBase(IReadOnlyCollection<(ISpecification spec, IAction action)> expectedInputQueue)
  {
   _expectedInputQueue = expectedInputQueue;
   Reset();
  }

  public IAction NextAction => _currentQueue.Peek().action;

  public bool CanHandle(InputResult inputResult)
  {
   return _currentQueue.Count != 0 && _currentQueue.Peek().spec.IsMatching(inputResult);
  }

  public void Handle(InputResult inputResult)
  {
   _currentQueue.Dequeue().action.Act(inputResult);
  }

  public void Reset()
  {
   _currentQueue = new Queue<(ISpecification spec, IAction action)>(_expectedInputQueue);
  }
 }
}