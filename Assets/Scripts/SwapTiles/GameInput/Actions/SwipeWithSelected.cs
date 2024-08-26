using System.Linq;
using Ji2.Context.Context;
using SwapTiles.Models.Interaction;
using SwapTiles.Models.Solvables;
using UnityEngine;

namespace SwapTiles.GameInput.Actions
{
 public class SwipeWithSelected: IAction
 {
  private readonly CurrentSelection _currentSelection;

  public SwipeWithSelected(CurrentSelection currentSelection)
  {
   _currentSelection = currentSelection;
  }
  public void Act(InputResult inputResult)
  {
   Entity currentSelectedEntity = _currentSelection.AllSelected().First();
   ISelectable selectedSelection = currentSelectedEntity.Get<ISelectable>();
   ITilePosition selectedPosition = currentSelectedEntity.Get<ITilePosition>();
   Vector3Int selectedPositionIndex = selectedPosition.Position;
    
   ISelectable inputSelection = inputResult.Target.Get<ISelectable>();
   ITilePosition inputPosition = inputResult.Target.Get<ITilePosition>();
   Vector3Int inputPositionIndex = inputPosition.Position;
    
   inputSelection.Select();
   inputPosition.MoveTo(selectedPositionIndex);
   selectedPosition.MoveTo(inputPositionIndex);
    
   inputSelection.Deselect();
   selectedSelection.Deselect();
  }
 }
}