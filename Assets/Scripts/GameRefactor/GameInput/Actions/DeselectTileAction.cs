namespace GameRefactor.GameInput.Actions
{
 public class DeselectTileAction : IAction
 {
  public void Act(InputResult inputResult)
  {
   inputResult.Target.GetComponent<ISelectable>().Deselect();
  }
 }
}