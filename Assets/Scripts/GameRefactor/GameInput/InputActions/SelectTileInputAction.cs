using System.Collections.Generic;
using GameRefactor.GameInput.Actions;
using GameRefactor.GameInput.Specifications;

namespace GameRefactor.GameInput.InputActions
{
 public class SelectTileInputAction : GameInputActionBase
 {
  public SelectTileInputAction(BeganUnselectedTargetSpecification beganUnselectedTargetSpecification,
   SelectTileAction selectTile) 
   : base(new List<(ISpecification spec, IAction action)>()
  {
   (beganUnselectedTargetSpecification, selectTile)
  }
   )
  { }
 }
}