using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Specifications
{
 public class IsDefaultPosition : ISpecification<InputResult>
 {
  private readonly bool _isOnDefaultPos;
  private readonly ISpecification<InputResult> _spec;

  public IsDefaultPosition(bool isOnDefaultPos, ISpecification<InputResult> spec)
  {
   _isOnDefaultPos = isOnDefaultPos;
   _spec = spec;
  }

  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) &&
          inputResult.Target.Get<DefaultTilePosition>().IsOnDefaultPosition() == _isOnDefaultPos;
  }
 }

 public static class IsDefaultPositionFluentExtension
 {
  public static ISpecification<InputResult> IsDefaultPosition(this ISpecification<InputResult> specification, bool isOnDefaultPos)
  {
   return new IsDefaultPosition(isOnDefaultPos, specification);
  }
 }
}