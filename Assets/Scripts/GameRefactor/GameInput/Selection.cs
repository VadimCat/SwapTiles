using System.Collections.Generic;
using UnityEngine;

namespace GameRefactor.GameInput
{
 public class Selection
 {
  private readonly Dictionary<GameObject, ISelectable> _selectables;
  private ISelectable _selected;

  public Selection(Dictionary<GameObject, ISelectable> selectables)
  {
   _selectables = selectables;
  }

  public void Select(GameObject target)
  {
   _selectables[target].Select();
  }
 }
}