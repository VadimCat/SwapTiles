using System;
using System.Collections.Generic;
using Client.Presenters;
using GameRefactor.Interfaces;
using Ji2;
using Ji2.CommonCore;
using UnityEngine;

namespace GameRefactor.GameInput
{
 public class TilesRaycastInput: IUpdatable
 {
  private const string TileLayerName = "Tile";
  private readonly UpdateService _updateService;
  private readonly CameraProvider _cameraProvider;
  private TouchScreenInputActions _inputActions;
  private readonly int _layer = LayerMask.NameToLayer(TileLayerName);
  private readonly List<ITileSolvable> _solvableList = new(5);
  
  public TilesRaycastInput(UpdateService updateService, CameraProvider cameraProvider)
  {
   _updateService = updateService;
   _cameraProvider = cameraProvider;
   _inputActions = new TouchScreenInputActions();
   _updateService.Add(this);
  }
  
  public void OnUpdate()
  {
   var phase = _inputActions.Input.TouchPhase.ReadValue<TouchPhase>();
   Debug.LogError(phase.ToString());
   switch (phase)
   {
    case TouchPhase.Began:

     Vector2 inputPos = _inputActions.Input.TouchPosition.ReadValue<Vector2>();
     Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(inputPos);
     
     if (Physics.Raycast(ray, out RaycastHit hit, 100, _layer))
     {
      hit.transform.GetComponents(_solvableList);
     }

     Debug.LogError(_solvableList);
     break;
    case TouchPhase.Moved:
     break;
    case TouchPhase.Stationary:
     break;
    case TouchPhase.Ended:
     break;
    case TouchPhase.Canceled:
     break;
    default:
     throw new ArgumentOutOfRangeException();
   }
  }
 }
}