using System;
using Client.Presenters;
using GameRefactor.Interfaces;
using Ji2;
using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace GameRefactor.GameInput
{
 public class TilesRaycastInput
 {
  private readonly TouchpadInputActions _touchpadInputActions;
  private readonly CameraProvider _cameraProvider;
  private readonly int _tilesLayer = LayerMask.NameToLayer($"Tiles");
  public event Action<InputResult> EventRayCasted;

  public TilesRaycastInput(TouchpadInputActions touchpadInputActions, CameraProvider cameraProvider)
  {
   _touchpadInputActions = touchpadInputActions;
   _cameraProvider = cameraProvider;

   _touchpadInputActions.EventTapUpdated += OnTap;
  }

  private void OnTap(Vector2 pos, TouchPhase touchPhase)
  {
   Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(pos);
   if (Physics.Raycast(ray, out RaycastHit result, 100, _tilesLayer))
   {
    EventRayCasted?.Invoke(new InputResult(true, result.collider.gameObject,
     result.collider.gameObject.GetComponents<ITileSolvable>(), touchPhase));
   }
   else
   {
    EventRayCasted?.Invoke(new InputResult(touchPhase));
   }
  }
 }
}