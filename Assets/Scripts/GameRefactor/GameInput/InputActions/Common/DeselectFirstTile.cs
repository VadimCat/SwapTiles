using Input.Actions;
using Input.Specifications;

namespace Input.InputActions.Common
{
 public class DeselectFirstTile : GameInputActionBase
 {
  public DeselectFirstTile(ISpecification<InputResult> beganSelectedTargetSpecification, DeselectTileAction selectTile) 
   : base(beganSelectedTargetSpecification, selectTile)
  {
  }
 }
}