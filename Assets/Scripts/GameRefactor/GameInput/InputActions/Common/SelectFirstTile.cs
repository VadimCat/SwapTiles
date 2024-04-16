using Input.Actions;
using Input.Specifications;

namespace Input.InputActions.Common
{
 public class SelectFirstTile : GameInputActionBase
 {
  public SelectFirstTile(ISpecification<InputResult> spec, SelectTileAction selectTile) 
   : base(spec, selectTile)
  {
  }
 }
}