using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions.Common
{
 public class DeselectFirstTile : GameInputActionBase
 {
  public DeselectFirstTile(ISpecification<InputResult> beganSelectedTargetSpecification, DeselectTileAction selectTile) 
   : base(beganSelectedTargetSpecification, selectTile)
  {
  }
 }
}