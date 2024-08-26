using SwapTiles.GameInput.Actions.Rotation;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions.Rotate
{
 public class SwipeEndOnTouchEnd : GameInputActionBase
 {
  public SwipeEndOnTouchEnd(ISpecification<InputResult> spec, EndRotationSwipe action) : base(spec, action)
  { }
 }
}