using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ji2.Presenters;
using Ji2Core.DataTypes;
using SwapTiles.Models.Solvables;
using UnityEngine;

namespace SwapTiles.Views
{
 public class TileRotationView : ITileRotation
 {
  const float RotationTime = .2f;
  
  private readonly Transform _transform;
  private readonly ITileRotation _tileRotation;
  private readonly AnimationQueue _animationQueue;
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

  public int DiscreteRotationAngle => _tileRotation.DiscreteRotationAngle;

  public TileRotationView(Transform transform, ITileRotation tileRotation, AnimationQueue animationQueue)
  {
   _transform = transform;
   _tileRotation = tileRotation;
   _animationQueue = animationQueue;

   transform.localRotation = Quaternion.Euler(0, 0, tileRotation.DiscreteRotationAngle);

   _tileRotation.EventRotationUpdated += OnTileRotationUpdate;
  }

  private void OnTileRotationUpdate(int rotation)
  {
   var newRotation = Quaternion.Euler(0, 0, rotation);
   _animationQueue.Animate(_transform.DORotateQuaternion(newRotation, RotationTime).ToUniTask()).Forget();
  }

  public void Rotate(RotationDirection direction)
  {
   _tileRotation.Rotate(direction);
  }

  public class Factory
  {
   private readonly AnimationQueue _animationQueue;

   public Factory(AnimationQueue animationQueue)
   {
    _animationQueue = animationQueue;
   }

   public ITileRotation Create(Transform tileRoot, ITileRotation tileRotation)
   {
    return new TileRotationView(tileRoot, tileRotation, _animationQueue);
   }
  }
 }
}