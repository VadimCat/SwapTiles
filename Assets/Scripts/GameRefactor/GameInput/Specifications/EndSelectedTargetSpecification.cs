using UnityEngine.InputSystem;

namespace GameRefactor.GameInput.Specifications
{
 public class EndSelectedTargetSpecification : ISpecification
 {
  public bool IsMatching(InputResult inputResult)
  {
   return inputResult is { HasTarget: true, TouchPhase: TouchPhase.Ended } &&
          inputResult.Target.GetService<ISelectable>().IsSelected;
  }
 }
}