using UnityEngine;

namespace Client.Presenters
{
 public struct SwipeData
 {
  public readonly Vector2 From;
  public readonly Vector2 To;
  public readonly Vector2 Direction;

  public SwipeData(Vector2 from, Vector2 to, Vector2 direction)
  {
   From = from;
   To = to;
   Direction = direction;
  }
 }
}