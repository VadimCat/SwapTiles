using System;
using Ji2;
using Ji2.Context.Context;
using Ji2.Tools.Layer;
using SwapTiles.Input;
using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace SwapTiles.GameInput
{
 public class TileInput: IDisposable 
 {
  private readonly TapInputAction _tapInputAction;
  private readonly CameraProvider _cameraProvider;
  private readonly Layer _tilesLayer = Layer.FromName("Tile");
  public event Action<InputResult> EventRayCasted;

  public TileInput(TapInputAction tapInputAction, CameraProvider cameraProvider)
  {
   _tapInputAction = tapInputAction;
   _cameraProvider = cameraProvider;

   _tapInputAction.EventTapUpdated += OnTap;
  }

  private void OnTap(TouchData data)
  {
   Vector2 pos = data.Position;
   TouchPhase touchPhase = data.Phase;
   Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(pos);
   
   EventRayCasted?.Invoke(Physics.Raycast(ray, out RaycastHit result, 100, _tilesLayer.Mask)
    ? new InputResult(true, result.collider.gameObject.GetComponent<Entity>(), touchPhase, pos)
    : new InputResult(touchPhase, pos));
  }

  public void Dispose()
  {
   _tapInputAction.EventTapUpdated -= OnTap;
  }
 }
}