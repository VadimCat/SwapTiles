using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Client.Presenters
{
 public struct TouchData
 {
  public readonly Vector2 Position;
  public readonly TouchPhase Phase;
  
  public TouchData(Vector2 position, TouchPhase phase)
  {
   Position = position;
   Phase = phase;
  }
 }
}