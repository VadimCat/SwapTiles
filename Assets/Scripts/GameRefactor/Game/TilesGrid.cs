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

  //Todo: may be should create system that contains all entities, could find, attach, same problem as CurrentSelection
  public TilesGrid(GridField gridField, IEnumerable<Entity> entities)
  {
   _gridField = gridField;
   foreach (var entity in entities)
   {
    ITilePosition position = entity.GetService<ITilePosition>();
    _positionToEntity.Add(position, entity); 
   }
  }

  public bool EntityByPos(Vector3 pos, out Entity entity)
  {
   if (_gridField.GetReversePoint(pos, out Vector3Int index))
   {
    ITilePosition key  =_positionToEntity.Keys.First(p => p.Position == index);
    entity = _positionToEntity[key];
    return true;
   }

   entity = null;
   return false;
  }
 }
}