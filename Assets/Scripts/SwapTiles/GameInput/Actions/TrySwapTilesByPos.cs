using Ji2;
using Ji2.Context.Context;
using SwapTiles.Game;
using SwapTiles.Models.Interaction;
using SwapTiles.Models.Solvables;
using UnityEngine;

namespace SwapTiles.GameInput.Actions
{
 public class TrySwapTilesByPos : IAction
 {
  private readonly TilesGrid _grid;
  private readonly ScreenSpacePlane _screenSpacePlane;
  private readonly CameraProvider _cameraProvider;

  public TrySwapTilesByPos(TilesGrid grid, ScreenSpacePlane screenSpacePlane)
  {
   _grid = grid;
   _screenSpacePlane = screenSpacePlane;
  }
  public void Act(InputResult inputResult)
  {
   ISelectable holdsSelectable = inputResult.Target.Get<ISelectable>();
   ITilePosition holdTilePos = inputResult.Target.Get<ITilePosition>();
   Vector3Int holdPos = holdTilePos.Position;
    
   Vector3 worldPos = _screenSpacePlane.GetWorldPositionOnPlane(inputResult.Pos);
   if(_grid.EntityByPos(worldPos, out Entity entityOnPos))
   {
    ITilePosition tilePos = entityOnPos.Get<ITilePosition>();
    ISelectable posSelectable = entityOnPos.Get<ISelectable>();
    Vector3Int pos = tilePos.Position;
    posSelectable.Select();
    
    tilePos.MoveTo(holdPos);
    holdTilePos.MoveTo(pos);
    posSelectable.Deselect();
   }
   
   holdsSelectable.Deselect();
  }
 }
}