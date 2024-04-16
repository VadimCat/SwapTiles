using Input.Actions;
using Input.Specifications;

namespace Input.InputActions.SwipeSwap
{
 public class SwapTilesOnSwipeEnd : GameInputActionBase
 {
  public SwapTilesOnSwipeEnd(ISpecification<InputResult> iSpecification, TrySwapTilesByPos trySwapTilesByPos) 
   : base(iSpecification, trySwapTilesByPos)
  {
  }
 }
}