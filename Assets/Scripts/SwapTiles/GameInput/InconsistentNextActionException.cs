using System;
using System.Text;
using SwapTiles.GameInput.InputActions;

namespace SwapTiles.GameInput
{
 public class InconsistentNextActionException : Exception
 {
  public InconsistentNextActionException(GameInputActionBase[] gameInputActionBases) : base(GenerateErrorMessage(gameInputActionBases))
  {
  }

  private static string GenerateErrorMessage(GameInputActionBase[] gameInputActionBases)
  {
   StringBuilder errorMessage = new();
   errorMessage.AppendLine("Different actions have different next actions.");
   foreach (var inputAction in gameInputActionBases)
   {
    errorMessage.AppendLine(inputAction.GetType().Name);
   }

   return errorMessage.ToString();
  }
 }

}