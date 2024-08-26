using SwapTiles.Models.Interaction;

namespace SwapTiles.GameInput.Actions
{
 public class SelectTileAction : IAction
 {
  public void Act(InputResult inputResult)
  {
   inputResult.Target.GetComponent<ISelectable>().Select();
  }
 }
}