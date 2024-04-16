using UnityEngine.InputSystem;

namespace Input.Specifications
{
 public class TouchPhaseSpec: ISpecification<InputResult>
 {
  private readonly ISpecification<InputResult> _spec;
  private readonly TouchPhase _touchPhase;

  public TouchPhaseSpec(TouchPhase touchPhase, ISpecification<InputResult> spec)
  {
   _spec = spec;
   _touchPhase = touchPhase;
  }
  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) && inputResult.TouchPhase == _touchPhase;
  }
 }
 
 public static class TouchPhaseFluentExtension
 {
  public static ISpecification<InputResult> TouchPhase(this ISpecification<InputResult> specification, TouchPhase touchPhase)
  {
   return new TouchPhaseSpec(touchPhase, specification);
  }
 }
}