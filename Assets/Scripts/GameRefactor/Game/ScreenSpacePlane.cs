using Ji2;
using UnityEngine;

namespace Tiles
{
 public class ScreenSpacePlane
 {
  private readonly CameraProvider _cameraProvider;
  private Plane _xy = new(Vector3.forward, new Vector3(0, 0, 0));

  public ScreenSpacePlane(CameraProvider cameraProvider)
  {
   _cameraProvider = cameraProvider;
  }
  
  public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition)
  {
   Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(screenPosition);
   _xy.Raycast(ray, out var distance);
   return ray.GetPoint(distance);
  }
 }
}