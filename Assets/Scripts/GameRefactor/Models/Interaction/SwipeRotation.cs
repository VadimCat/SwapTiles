using Ji2;
using Ji2Core.DataTypes;
using Models.Solvables;
using UnityEngine;

namespace Models.Interaction
{
 public class SwipeRotation
 {
  private const int SwipeDistance = 200;

  private readonly ITileRotation _tileRotation;
  private readonly Transform _transform;
  private readonly CameraProvider _cameraProvider;

  private Vector2? _lastPos;
  
  public SwipeRotation(ITileRotation tileRotation, Transform transform, CameraProvider cameraProvider)
  {
   _tileRotation = tileRotation;
   _transform = transform;
   _cameraProvider = cameraProvider;
  }

  public void UpdateSwipe(Vector2 movePosition)
  {
   if (_lastPos == null)
   {
    _lastPos = movePosition;
    return;
   }
   
   Vector2 direction = movePosition - _lastPos.Value;
   if (direction.sqrMagnitude > SwipeDistance * SwipeDistance)
   {
    Vector2 objectScreenPosition = _cameraProvider.MainCamera.WorldToScreenPoint(_transform.position);
    Vector2 tileToTouchPosition = objectScreenPosition - movePosition;
    float signedAngle = Vector2.SignedAngle(direction, tileToTouchPosition);
    _tileRotation.Rotate(signedAngle > 0 ? RotationDirection.CounterClockwise : RotationDirection.Clockwise);
    _lastPos = movePosition;
   }
  }

  public void Reset()
  {
   _lastPos = null;
  }

  public class Factory
  {
   private readonly CameraProvider _cameraProvider;

   public Factory(CameraProvider cameraProvider)
   {
    this._cameraProvider = cameraProvider;
   }

   public SwipeRotation Create(ITileRotation tileRotation, Transform tileRoot)
   {
    return new SwipeRotation(tileRotation, tileRoot, _cameraProvider);
   }
  }
 }
}