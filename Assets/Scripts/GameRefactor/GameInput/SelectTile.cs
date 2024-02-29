namespace GameRefactor.GameInput
{
 public class SelectTile : IAction
 {
  private readonly Selection _selection;

  public SelectTile(Selection selection)
  {
   _selection = selection;
  }

  public void Act(InputResult inputResult)
  {
   _selection.Select(inputResult.Target);
  }
 }
}