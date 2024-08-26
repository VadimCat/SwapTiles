using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions.SwipeSwap
{
 public class MoveSelectedInputAction : GameInputActionBase
 {
  public MoveSelectedInputAction(ISpecification<InputResult> moveSelectedSpec, MoveTileAction moveTileAction) : base(moveSelectedSpec, moveTileAction)
  {
  }
 }
}