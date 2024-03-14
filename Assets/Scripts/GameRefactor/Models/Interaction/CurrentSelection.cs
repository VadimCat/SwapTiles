using System.Collections.Generic;
using System.Linq;
using GameRefactor.GameInput;
using Ji2.Context.Context;

namespace GameRefactor.Models.Interaction
{
 public class CurrentSelection
 {
  private readonly Dictionary<ISelectable, Entity> _selectionToEntity = new();

  public CurrentSelection(IEnumerable<Entity> entities)
  {
   foreach (var entity in entities)
   {
    ISelectable position = entity.GetService<ISelectable>();
    _selectionToEntity.Add(position, entity);
   }
  }

  public bool IsAnySelected => _selectionToEntity.Keys.Any(s => s.IsSelected);

  public IEnumerable<Entity> AllSelected()
  {
   IEnumerable<ISelectable> selectedKeys =_selectionToEntity.Keys.Where(s => s.IsSelected);
   return selectedKeys.Select(s => _selectionToEntity[s]);
  }
 }
}