namespace GameRefactor.GameInput.Specifications
{
 public class IsSelected: ISpecification
 {
  private readonly ISpecification _spec;
  private readonly bool _isSelected;

  public IsSelected(bool isSelected, ISpecification spec)
  {
   _spec = spec;
   _isSelected = isSelected;
  }
  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) && inputResult.Target.GetService<ISelectable>().IsSelected == _isSelected;
  }
 }
}