using Models.Interaction;

namespace Input.Specifications
{
 public class CanInteract: ISpecification<InputResult>
 {
  private readonly InputLocker _inputLocker;
  private readonly ILockingSource _lockingSource;
  private readonly bool _canInteract;
  private readonly ISpecification<InputResult> _specification;

  public CanInteract(InputLocker inputLocker, ILockingSource lockingSource, bool canInteract,
   ISpecification<InputResult> specification)
  {
   _inputLocker = inputLocker;
   _lockingSource = lockingSource;
   _canInteract = canInteract;
   _specification = specification;
  }

  public bool IsMatching(InputResult inputResult)
  {
   return _specification.IsMatching(inputResult) && _inputLocker.CanInteract(_lockingSource) == _canInteract;
  }
 }

 public static class CanInteractFluentExtension
 {
  public static ISpecification<InputResult> CanInteract(this ISpecification<InputResult> spec, InputLocker inputLocker, 
   ILockingSource lockingSource, bool canInteract)
  {
   return new CanInteract(inputLocker, lockingSource, canInteract, spec);
  }
 }
}