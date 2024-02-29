using System;
using GameRefactor.Interfaces;
using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace GameRefactor.GameInput
{
 public class InputResult
 {
  public readonly bool HasTarget;
  public readonly GameObject Target;
  public readonly ITileSolvable[] Solvables;
  public readonly TouchPhase TouchPhase;

  public InputResult(bool hasTarget, GameObject target, ITileSolvable[] tileSolvables, TouchPhase touchPhase)
  {
   HasTarget = hasTarget;
   Target = target;
   Solvables = tileSolvables;
   TouchPhase = touchPhase;
  }

  public InputResult(TouchPhase touchPhase)
  {
   TouchPhase = touchPhase;
   HasTarget = false;
   Solvables = Array.Empty<ITileSolvable>();
  }
 }
}