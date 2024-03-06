using System.Collections.Generic;
using System.Linq;
using Client.Views;
using GameRefactor.Interfaces;
using Ji2.Context.Context;
using UnityEngine;

namespace GameRefactor.Game
{
 public class TilesGrid
 {
  private readonly GridField _gridField;
  private readonly Dictionary<ITilePosition, Entity> _positionToEntity = new();

  public TilesGrid(GridField gridField)
  {
   _gridField = gridField;
  }

  public void AddEntity(Entity entity)
  {
   ITilePosition position = entity.GetService<ITilePosition>();
   _positionToEntity.Add(position, entity);
  }

  public Entity GetEntityByPos(Vector3 pos)
  {
   Vector3Int entityPos = _gridField.GetReversePoint(pos);
   ITilePosition key  =_positionToEntity.Keys.First(p => p.Position == entityPos);
   return _positionToEntity[key];
  }
 }
}