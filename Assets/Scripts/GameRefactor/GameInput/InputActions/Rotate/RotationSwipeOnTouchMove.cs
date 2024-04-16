using Input.Actions;
using Input.Specifications;

namespace Input.InputActions.Rotate
{
 public class RotationSwipeOnTouchMove : GameInputActionBase
 {
  public RotationSwipeOnTouchMove(ISpecification<InputResult> spec, IAction rotationSwipeUpdate) : base(spec, rotationSwipeUpdate)
  { }
 }
}