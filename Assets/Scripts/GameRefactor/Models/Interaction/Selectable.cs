using System;

namespace Models.Interaction
{
 public class Selectable: ISelectable
 {
  public bool IsSelected
  {
   get => _isSelected;
   private set
   {
    _isSelected = value;
    EventIsSelectedUpdated?.Invoke(_isSelected);
   }
  }

  public event Action<bool> EventIsSelectedUpdated;
  private bool _isSelected;

  public void Select()
  {
   IsSelected = true;
  }

  public void Deselect()
  {
   IsSelected = false;
  }
 }
}