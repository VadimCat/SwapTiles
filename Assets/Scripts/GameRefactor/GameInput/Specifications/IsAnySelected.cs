using Models.Interaction;

namespace Input.Specifications
{
 public class IsAnySelected: ISpecification<InputResult>
 {
  private readonly ISpecification<InputResult> _spec;
  private readonly CurrentSelection _currentSelection;
  private readonly bool _isAnySelected;

  public IsAnySelected(CurrentSelection currentSelection, bool isAnySelected, ISpecification<InputResult> spec)
  {
   _spec = spec;
   _currentSelection = currentSelection;
   _isAnySelected = isAnySelected;
  }
  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) && _currentSelection.IsAnySelected == _isAnySelected;
  }
 }

 public static class IsAnySelectedFluentExtension
 {
  public static ISpecification<InputResult> IsAnySelected(this ISpecification<InputResult> specification, CurrentSelection currentSelection, bool isAnySelected)
  {
   return new IsAnySelected(currentSelection, isAnySelected, specification);
  }
 }
}