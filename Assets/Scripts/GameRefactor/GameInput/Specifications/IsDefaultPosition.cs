using GameRefactor.Models.Interaction;

namespace GameRefactor.GameInput.Specifications
{
 public class IsDefaultPosition : ISpecification
 {
  private readonly bool _isOnDefaultPos;
  private readonly ISpecification _spec;

  public IsDefaultPosition(bool isOnDefaultPos, ISpecification spec)
  {
   _isOnDefaultPos = isOnDefaultPos;
   _spec = spec;
  }

  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) &&
          inputResult.Target.GetService<DefaultTilePosition>().IsOnDefaultPosition() == _isOnDefaultPos;
  }
 }
}