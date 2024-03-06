using System;

namespace GameRefactor.GameInput
{
 public interface ISelectable
 {
  bool IsSelected { get; }
  event Action<bool> EventIsSelectedUpdated; 
  void Select();
  void Deselect();
 }
}