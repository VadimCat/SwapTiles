using System.Collections.Generic;
using GameRefactor.GameInput.Actions;
using GameRefactor.GameInput.Specifications;

namespace GameRefactor.GameInput.InputActions
{
 public class MoveSelectedInputAction : GameInputActionBase
 {
  public MoveSelectedInputAction(MoveSelectedTargetSpecification moveSelectedSpec, MoveTileAction moveTileAction) :
   base(new List<(ISpecification spec, IAction action)>()
   {
    (moveSelectedSpec, moveTileAction)
   })
  {
  }
 }
}