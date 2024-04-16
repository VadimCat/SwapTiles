using Input.Actions.Rotation;
using Input.Specifications;

namespace Input.InputActions.Rotate
{
 public class SwipeEndOnTouchEnd : GameInputActionBase
 {
  public SwipeEndOnTouchEnd(ISpecification<InputResult> spec, EndRotationSwipe action) : base(spec, action)
  { }
 }
}