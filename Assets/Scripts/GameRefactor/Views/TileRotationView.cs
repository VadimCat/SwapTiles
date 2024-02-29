using System;
using Client.Models;
using Client.Presenters;
using DG.Tweening;
using GameRefactor.Interfaces;
using UnityEngine;

namespace GameRefactor.Views
{
 public class TileRotationView : MonoBehaviour, ITileRotation
 {
  public class Factory
  {
   public TileRotationView Create(GameObject gameObject, ITileRotation rotation)
   {
    var comp = gameObject.AddComponent<TileRotationView>();
    comp.Construct(rotation);
     return comp;
   }
  }
  
  private ITileRotation _tileRotation;
  public bool IsCompleted => _tileRotation.IsCompleted;

  public event Action<bool> EventIsCompletedUpdated
  {
   add => _tileRotation.EventIsCompletedUpdated += value;
   remove => _tileRotation.EventIsCompletedUpdated -= value;
  }

  public event Action<int> EventRotationUpdated
  {
   add => _tileRotation.EventRotationUpdated += value;
   remove => _tileRotation.EventRotationUpdated -= value;
  }

  public int Rotation => _tileRotation.Rotation;

  private void Construct(ITileRotation tileRotation)
  {
   _tileRotation = tileRotation;
   
   transform.localRotation = Quaternion.Euler(0, 0, tileRotation.Rotation);

   _tileRotation.EventRotationUpdated += OnTileRotationUpdate;
  }

  private void OnTileRotationUpdate(int rotation)
  {
   var newRotation = Quaternion.Euler(0, 0, rotation);
   transform.DORotateQuaternion(newRotation, 1f);
  }

  public void Rotate(RotationDirection direction)
  {
   _tileRotation.Rotate(direction);
  }
 }
}