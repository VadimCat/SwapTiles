using GameRefactor.GameInput.Actions;

namespace GameRefactor.GameInput.InputActions.TapSwap
{
 public class SwapTilesOnTapEnd: GameInputActionBase
 {
  public SwapTilesOnTapEnd(ISpecification spec, SwipeWithSelected action) : base(spec, action)
  {
  }
 }
}