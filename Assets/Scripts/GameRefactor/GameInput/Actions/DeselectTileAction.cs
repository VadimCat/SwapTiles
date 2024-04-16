using Models.Interaction;

namespace Input.Actions
{
 public class DeselectTileAction : IAction
 {
  public void Act(InputResult inputResult)
  {
   inputResult.Target.GetComponent<ISelectable>().Deselect();
  }
 }
}