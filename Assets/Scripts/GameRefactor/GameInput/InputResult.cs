using Ji2.Context.Context;
using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace GameRefactor.GameInput
{
 public struct InputResult
 {
  public readonly bool HasTarget;
  public readonly Entity Target;
  public readonly TouchPhase TouchPhase;
  public readonly Vector2 Pos;

  public InputResult(bool hasTarget, Entity target, TouchPhase touchPhase, Vector2 pos)
  {
   HasTarget = hasTarget;
   Target = target;
   TouchPhase = touchPhase;
   Pos = pos;
  }

  public InputResult(TouchPhase touchPhase, Vector2 pos)
  {
   TouchPhase = touchPhase;
   Pos = pos;
   HasTarget = false;
   Target = null;
  }
 }
}