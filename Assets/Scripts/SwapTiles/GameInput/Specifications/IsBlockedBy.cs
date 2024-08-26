using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Specifications
{
 public class IsBlockedBy : ISpecification<InputResult>
 {
  private readonly InputLocker _inputLocker;
  private readonly ILockingSource _lockingSource;
  private ISpecification<InputResult> _spec;

  public IsBlockedBy(ISpecification<InputResult> spec, InputLocker inputLocker, ILockingSource lockingSource)
  {
   _spec = spec;
   _inputLocker = inputLocker;
   _lockingSource = lockingSource;
  }
  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) && _inputLocker.IsBlockedBy(_lockingSource);
  }
 }

 public static class IsBlockedByFluentExtension
 {
  public static ISpecification<InputResult> IsBlockedBy(this ISpecification<InputResult> spec, InputLocker inputLocker,
   ILockingSource lockingSource)
  {
   return new IsBlockedBy(spec, inputLocker, lockingSource);
  }
 }
}