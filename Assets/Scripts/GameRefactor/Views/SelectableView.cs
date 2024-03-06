using System;
using DG.Tweening;
using GameRefactor.GameInput;
using UnityEngine;

namespace GameRefactor.Views
{
 public class SelectableView: MonoBehaviour, ISelectable
 {
  public class Factory
  {
   public SelectableView Create(GameObject gameObject, ISelectable selectable)
   {
    var comp = gameObject.AddComponent<SelectableView>();
    comp.Construct(selectable);
    return comp;
   }
  }

  private const float SelectionTime = .2f;
  private const float SelectedHeight = -1;
  private ISelectable _selectable;

  public bool IsSelected => _selectable.IsSelected;

  public event Action<bool> EventIsSelectedUpdated
  {
   add => _selectable.EventIsSelectedUpdated += value;
   remove => _selectable.EventIsSelectedUpdated -= value;
  }

  private void Construct(ISelectable selectable)
  {
   _selectable = selectable;
   EventIsSelectedUpdated += OnIsSelectedUpdated;
  }

  private void OnIsSelectedUpdated(bool isSelected)
  {
   transform.DOMoveZ(isSelected ? SelectedHeight : 0, SelectionTime);
  }

  public void Select()
  {
   _selectable.Select();
  }

  public void Deselect()
  {
   _selectable.Deselect();
  }

  private void OnDestroy()
  {
   EventIsSelectedUpdated -= OnIsSelectedUpdated;
  }
 }
}