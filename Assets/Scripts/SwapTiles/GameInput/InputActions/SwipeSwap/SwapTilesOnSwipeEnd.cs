using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions.SwipeSwap
{
 public class SwapTilesOnSwipeEnd : GameInputActionBase
 {
  public SwapTilesOnSwipeEnd(ISpecification<InputResult> iSpecification, TrySwapTilesByPos trySwapTilesByPos) 
   : base(iSpecification, trySwapTilesByPos)
  {
  }
 }
}