namespace SwapTiles.GameInput.Specifications
{
 public class HasTarget : ISpecification<InputResult>
 {
  private readonly ISpecification<InputResult> _specification;
  private readonly bool _hasTarget;

  public HasTarget(bool hasTarget, ISpecification<InputResult> specification)
  {
   _specification = specification;
   _hasTarget = hasTarget;
  }
  
  public bool IsMatching(InputResult inputResult)
  {
   return _specification.IsMatching(inputResult) && inputResult.HasTarget == _hasTarget;
  }
 }

 public static class HasTargetFluentExtension
 {
  public static ISpecification<InputResult> HasTarget(this ISpecification<InputResult> iSpecification, bool hasTarget)
  {
   return new HasTarget(hasTarget, iSpecification);
  }
 }
}