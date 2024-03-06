using System.Collections.Generic;
using GameRefactor.GameInput.Actions;
using GameRefactor.GameInput.Specifications;

namespace GameRefactor.GameInput.InputActions
{
 public class SwapTilesOnTapEnd : GameInputActionBase
 {
  public SwapTilesOnTapEnd(TrySwapTilesByPos trySwapTilesByPos, EndSelectedTargetSpecification endSelected) : base(
   new List<(ISpecification spec, IAction action)>()
   {
    (endSelected, trySwapTilesByPos)
   })
  {
  }
 }
}