using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace GameRefactor.GameInput.Specifications
{
 public class BeganUnselectedTargetSpecification : ISpecification
 {
  public bool IsMatching(InputResult inputResult)
  {
   var result = inputResult is { HasTarget: true, TouchPhase: TouchPhase.Began } &&
                !inputResult.Target.GetService<ISelectable>().IsSelected;
   
   return result;
  }
 }
}