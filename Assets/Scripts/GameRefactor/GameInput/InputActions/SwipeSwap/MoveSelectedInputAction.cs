using GameRefactor.GameInput.Actions;

namespace GameRefactor.GameInput.InputActions
{
 public class MoveSelectedInputAction : GameInputActionBase
 {
  public MoveSelectedInputAction(ISpecification moveSelectedSpec, MoveTileAction moveTileAction) : base(moveSelectedSpec, moveTileAction)
  {
  }
 }
}