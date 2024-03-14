using GameRefactor.Models.Interaction;

namespace GameRefactor.GameInput.Specifications
{
 public class IsAnySelected: ISpecification
 {
  private readonly ISpecification _spec;
  private readonly CurrentSelection _currentSelection;
  private readonly bool _isAnySelected;

  public IsAnySelected(CurrentSelection currentSelection, bool isAnySelected, ISpecification spec)
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
}