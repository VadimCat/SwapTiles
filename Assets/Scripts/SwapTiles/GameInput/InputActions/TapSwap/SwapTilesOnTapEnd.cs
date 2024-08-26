using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions.TapSwap
{
 public class SwapTilesOnTapEnd: GameInputActionBase
 {
  public SwapTilesOnTapEnd(ISpecification<InputResult> spec, SwipeWithSelected action) : base(spec, action)
  {
  }
 }
}