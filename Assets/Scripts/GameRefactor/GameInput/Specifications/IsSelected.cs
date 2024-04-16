using Models.Interaction;

namespace Input.Specifications
{
 public class IsSelected: ISpecification<InputResult>
 {
  private readonly ISpecification<InputResult> _spec;
  private readonly bool _isSelected;

  public IsSelected(bool isSelected, ISpecification<InputResult> spec)
  {
   _spec = spec;
   _isSelected = isSelected;
  }
  public bool IsMatching(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult) && inputResult.Target.Get<ISelectable>().IsSelected == _isSelected;
  }
 }

 public static class IsSelectedFluentExtension
 {
  public static ISpecification<InputResult> IsSelected(this ISpecification<InputResult> specification, bool isSelected)
  {
   return new IsSelected(isSelected, specification);
  }
 }
}