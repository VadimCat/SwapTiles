namespace GameRefactor.GameInput.Specifications
{
 public class HasTarget : ISpecification
 {
  private readonly ISpecification _specification;
  private readonly bool _hasTarget;

  public HasTarget(bool hasTarget, ISpecification specification)
  {
   _specification = specification;
   _hasTarget = hasTarget;
  }
  
  public bool IsMatching(InputResult inputResult)
  {
   return _specification.IsMatching(inputResult) && inputResult.HasTarget == _hasTarget;
  }
 }
}