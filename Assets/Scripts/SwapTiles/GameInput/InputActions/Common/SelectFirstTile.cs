using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions.Common
{
 public class SelectFirstTile : GameInputActionBase
 {
  public SelectFirstTile(ISpecification<InputResult> spec, SelectTileAction selectTile) 
   : base(spec, selectTile)
  {
  }
 }
}