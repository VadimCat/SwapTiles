using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Specifications
{
 public class CanInteract: ISpecification<InputResult>
 {
  private readonly InputLock _inputLock;
  private readonly ILockingSource _lockingSource;
  private readonly bool _canInteract;
  private readonly ISpecification<InputResult> _specification;

  public CanInteract(InputLock inputLock, ILockingSource lockingSource, bool canInteract,
   ISpecification<InputResult> specification)
  {
   _inputLock = inputLock;
   _lockingSource = lockingSource;
   _canInteract = canInteract;
   _specification = specification;
  }

  public bool IsMatching(InputResult inputResult)
  {
   return _specification.IsMatching(inputResult) && _inputLock.CanInteract(_lockingSource) == _canInteract;
  }
 }

 public static class CanInteractFluentExtension
 {
  public static ISpecification<InputResult> CanInteract(this ISpecification<InputResult> spec, InputLock inputLock, 
   ILockingSource lockingSource, bool canInteract)
  {
   return new CanInteract(inputLock, lockingSource, canInteract, spec);
  }
 }
}