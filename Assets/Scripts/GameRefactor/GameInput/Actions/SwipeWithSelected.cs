using System.Linq;
using GameRefactor.Interfaces;
using GameRefactor.Models.Interaction;
using Ji2.Context.Context;
using UnityEngine;

namespace GameRefactor.GameInput.Actions
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
   ISelectable selectedSelection = currentSelectedEntity.GetService<ISelectable>();
   ITilePosition selectedPosition = currentSelectedEntity.GetService<ITilePosition>();
   Vector3Int selectedPositionIndex = selectedPosition.Position;
    
   ISelectable inputSelection = inputResult.Target.GetService<ISelectable>();
   ITilePosition inputPosition = inputResult.Target.GetService<ITilePosition>();
   Vector3Int inputPositionIndex = inputPosition.Position;
    
   inputSelection.Select();
   inputPosition.MoveTo(selectedPositionIndex);
   selectedPosition.MoveTo(inputPositionIndex);
    
   inputSelection.Deselect();
   selectedSelection.Deselect();
  }
 }
}