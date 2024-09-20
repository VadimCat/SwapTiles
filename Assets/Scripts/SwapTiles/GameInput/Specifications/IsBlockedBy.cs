using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Specifications
{
 public class IsBlockedBy : ISpecification<InputResult>
 {
  private readonly InputLock _inputLock;
  private readonly ILockingSource _lockingSource;
  private ISpecification<InputResult> _spec;

  public IsBlockedBy(ISpecification<InputResult> spec, InputLock inputLock, ILockingSource lockingSource)
  {
   _spec = spec;
   _inputLock = inputLock;
   _lockingSource = lockingSource;
  }
  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) && _inputLock.IsBlockedBy(_lockingSource);
  }
 }

 public static class IsBlockedByFluentExtension
 {
  public static ISpecification<InputResult> IsBlockedBy(this ISpecification<InputResult> spec, InputLock inputLock,
   ILockingSource lockingSource)
  {
   return new IsBlockedBy(spec, inputLock, lockingSource);
  }
 }
}