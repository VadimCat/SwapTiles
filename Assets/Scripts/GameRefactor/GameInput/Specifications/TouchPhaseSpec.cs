namespace GameRefactor.GameInput.Specifications
{
 public class TouchPhaseSpec: ISpecification
 {
  private readonly ISpecification _spec;
  private readonly UnityEngine.InputSystem.TouchPhase _touchPhase;

  public TouchPhaseSpec(UnityEngine.InputSystem.TouchPhase touchPhase, ISpecification spec)
  {
   _spec = spec;
   _touchPhase = touchPhase;
  }
  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) && inputResult.TouchPhase == _touchPhase;
  }
 }
}