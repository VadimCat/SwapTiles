using Input.Actions;
using Input.Specifications;

namespace Input.InputActions.SwipeSwap
{
 public class MoveSelectedInputAction : GameInputActionBase
 {
  public MoveSelectedInputAction(ISpecification<InputResult> moveSelectedSpec, MoveTileAction moveTileAction) : base(moveSelectedSpec, moveTileAction)
  {
  }
 }
}