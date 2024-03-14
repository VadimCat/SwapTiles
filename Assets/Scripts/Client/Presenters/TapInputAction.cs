using System;
using Ji2.CommonCore;
using Ji2.Utils;
using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Client.Presenters
{
 public class TapInputAction: IUpdatable, IDisposable
 {
  private readonly TouchScreenInputActions _touchInput;
  private readonly UpdateService _updateService;

  private readonly ReactiveProperty<TouchData> _tapData = new ();
  public event Action<TouchData> EventTapUpdated
  {
   add => _tapData.EventValueChanged += value;
   remove => _tapData.EventValueChanged -= value;
  }

  public TapInputAction(TouchScreenInputActions touchInput, UpdateService updateService)
  {
   _touchInput = touchInput;
   _updateService = updateService;
  }

  public void Enable()
  {
   _updateService.Add(this);
  }
  
  private void UpdateTapData(TouchPhase phase)
  {
   _tapData.Value = new TouchData(_touchInput.Input.TouchPosition.ReadValue<Vector2>(), phase);
  }

  public void OnUpdate()
  {
   TouchPhase phase = _touchInput.Input.TouchPhase.ReadValue<TouchPhase>();
   switch (phase)
   {
    case TouchPhase.None:
     break;
    case TouchPhase.Began:
     UpdateTapData(phase);
     break;
    case TouchPhase.Moved:
     UpdateTapData(phase);
     break;
    case TouchPhase.Ended:
     UpdateTapData(phase);
     break;
    case TouchPhase.Canceled:
     UpdateTapData(phase);
     break;
    case TouchPhase.Stationary:
     break;
    default:
     throw new ArgumentOutOfRangeException();
   }
  }

  public void Disable()
  {
   _updateService.Remove(this);
  }

  public void Dispose()
  {
   _updateService.Remove(this);
  }
 }

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