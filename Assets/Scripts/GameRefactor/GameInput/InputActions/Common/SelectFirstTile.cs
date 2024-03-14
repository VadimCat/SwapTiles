using GameRefactor.GameInput.Actions;

namespace GameRefactor.GameInput.InputActions
{
 public class SelectFirstTile : GameInputActionBase
 {
  public SelectFirstTile(ISpecification spec, SelectTileAction selectTile) 
   : base(spec, selectTile)
  {
  }
 }
}