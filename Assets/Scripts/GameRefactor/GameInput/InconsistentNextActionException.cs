using System;

namespace GameRefactor.GameInput
{
 public class InconsistentNextActionException : Exception
 {
  public InconsistentNextActionException()
   : base("Different actions have different next actions.")
  {
  }
 }
}