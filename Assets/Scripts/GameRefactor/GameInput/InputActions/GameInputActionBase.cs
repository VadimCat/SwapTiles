namespace GameRefactor.GameInput.InputActions
{
 public class GameInputActionBase
 {
  private readonly ISpecification _spec;
  public readonly IAction Action;

  protected GameInputActionBase(ISpecification spec, IAction action)
  {
   _spec = spec;
   Action = action;
  }

  public bool CanHandle(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult);
  }

  public void Handle(InputResult inputResult)
  {
   Action.Act(inputResult);
  }
 }
}