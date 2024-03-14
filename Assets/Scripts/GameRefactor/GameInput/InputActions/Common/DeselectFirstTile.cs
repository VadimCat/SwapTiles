using GameRefactor.GameInput.Actions;

namespace GameRefactor.GameInput.InputActions
{
 public class DeselectFirstTile : GameInputActionBase
 {
  public DeselectFirstTile(ISpecification beganSelectedTargetSpecification, DeselectTileAction selectTile) 
   : base(beganSelectedTargetSpecification, selectTile)
  {
  }
 }
}