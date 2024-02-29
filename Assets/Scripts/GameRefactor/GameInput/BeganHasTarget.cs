using UnityEngine.InputSystem;

namespace GameRefactor.GameInput
{
 public class BeganHasTarget : ISpecification
 {
  public bool IsMatching(InputResult inputResult)
  {
   return inputResult.HasTarget && inputResult.TouchPhase == TouchPhase.Began;
  }
 }
}