using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions.Rotate
{
 public class RotationSwipeOnTouchMove : GameInputActionBase
 {
  public RotationSwipeOnTouchMove(ISpecification<InputResult> spec, IAction rotationSwipeUpdate) : base(spec, rotationSwipeUpdate)
  { }
 }
}