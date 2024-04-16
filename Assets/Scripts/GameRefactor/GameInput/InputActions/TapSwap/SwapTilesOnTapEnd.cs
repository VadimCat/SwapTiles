using Input.Actions;
using Input.Specifications;

namespace Input.InputActions.TapSwap
{
 public class SwapTilesOnTapEnd: GameInputActionBase
 {
  public SwapTilesOnTapEnd(ISpecification<InputResult> spec, SwipeWithSelected action) : base(spec, action)
  {
  }
 }
}