namespace GameRefactor.GameInput.Actions
{
 public class SelectTileAction : IAction
 {
  public void Act(InputResult inputResult)
  {
   inputResult.Target.GetComponent<ISelectable>().Select();
  }
 }
}