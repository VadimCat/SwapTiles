using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace GameRefactor.GameInput.Specifications
{
 public class MoveSelectedTargetSpecification : ISpecification
 {
  public bool IsMatching(InputResult inputResult)
  {
   return inputResult is { HasTarget: true, TouchPhase: TouchPhase.Moved } &&
          inputResult.Target.GetService<ISelectable>().IsSelected;
  }
 }
}