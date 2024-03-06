using GameRefactor.Game;
using GameRefactor.Interfaces;
using Ji2;
using Ji2.Context.Context;
using UnityEngine;

namespace GameRefactor.GameInput.Actions
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
   ISelectable holdsSelectable = inputResult.Target.GetService<ISelectable>();
   ITilePosition holdTilePos = inputResult.Target.GetService<ITilePosition>();
   Vector3Int holdPos = holdTilePos.Position;
    
   Vector3 worldPos = _screenSpacePlane.GetWorldPositionOnPlane(inputResult.Pos);
   Entity entityOnPos = _grid.GetEntityByPos(worldPos);
   ITilePosition tilePos = entityOnPos.GetService<ITilePosition>();
   ISelectable posSelectable = entityOnPos.GetService<ISelectable>();
   Vector3Int pos = tilePos.Position;
   posSelectable.Select();
   
   tilePos.MoveTo(holdPos);
   holdTilePos.MoveTo(pos);
   holdsSelectable.Deselect();
   posSelectable.Deselect();
  }
 }
}