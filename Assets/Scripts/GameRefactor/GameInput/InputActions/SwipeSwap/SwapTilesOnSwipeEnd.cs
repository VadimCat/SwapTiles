using GameRefactor.GameInput.Actions;

namespace GameRefactor.GameInput.InputActions
{
 public class SwapTilesOnSwipeEnd : GameInputActionBase
 {
  public SwapTilesOnSwipeEnd(ISpecification iSpecification, TrySwapTilesByPos trySwapTilesByPos) 
   : base(iSpecification, trySwapTilesByPos)
  {
  }
 }
}