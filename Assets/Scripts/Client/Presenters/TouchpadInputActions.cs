using System;
using Ji2.CommonCore;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Client.Presenters
{
 public class TouchpadInputActions
 {
  private readonly TouchScreenInputActions _touchInput;
  private readonly UpdateService _updateService;
  
  public event Action<Vector2, TouchPhase> EventTapUpdated;

  public TouchpadInputActions(TouchScreenInputActions touchInput, UpdateService updateService)
  {
   _touchInput = touchInput;
   _updateService = updateService;
  }

  public void Enable()
  {
   _touchInput.Input.TouchPhase.performed += OnTouchPerfomed;
  }

  private void OnTouchPerfomed(InputAction.CallbackContext input)
  {
   switch (input.ReadValue<TouchPhase>())
   {
    case TouchPhase.None:
     break;
    case TouchPhase.Began:
     EventTapUpdated?.Invoke(_touchInput.Input.TouchPosition.ReadValue<Vector2>(), TouchPhase.Began);
     break;
    case TouchPhase.Moved:
     break;
    case TouchPhase.Ended:
     EventTapUpdated?.Invoke(_touchInput.Input.TouchPosition.ReadValue<Vector2>(), TouchPhase.Ended);
     break;
    case TouchPhase.Canceled:
     break;
    case TouchPhase.Stationary:
     break;
    default:
     throw new ArgumentOutOfRangeException();
   }
  }

  public void Disable()
  {
   _touchInput.Input.TouchPhase.performed -= OnTouchPerfomed;
  }
 }
}