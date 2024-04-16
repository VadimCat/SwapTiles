using System;

namespace Models.Interaction
{
 public interface ISelectable
 {
  bool IsSelected { get; }
  event Action<bool> EventIsSelectedUpdated; 
  void Select();
  void Deselect();
 }
}