using System;
using Ji2.CommonCore;
using UnityEngine;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Client.Presenters
{
    public class SwipeListener: IUpdatable
    {
        private readonly UpdateService _updateService;
        private readonly TouchScreenInputActions _touchScreenInputActions;

        private const float Threshold = 150;
        private float _startTime;
        private Vector2? _startSwipePos;
        private bool _isEnabled;
        public event Action<SwipeData> EventSwiped;
        
        public SwipeListener(UpdateService updateService, TouchScreenInputActions touchScreenInputActions)
        {
            _updateService = updateService;
            _touchScreenInputActions = touchScreenInputActions;
            _touchScreenInputActions.Enable();
        }

        public void Enable()
        {
            _updateService.Add(this);
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;

            _updateService.Remove(this);
            _startSwipePos = null;
        }
        
        public void OnUpdate()
        {
            if (!_isEnabled)
            {
                return;
            }
            
            var phase = _touchScreenInputActions.Input.TouchPhase.ReadValue<TouchPhase>();

            switch (phase)
            {
                case TouchPhase.Began:
                    _startSwipePos = _touchScreenInputActions.Input.TouchPosition.ReadValue<Vector2>();
                    break;
                case TouchPhase.Moved:
                {
                    if (_startSwipePos == null)
                    {
                        _startSwipePos = _touchScreenInputActions.Input.TouchPosition.ReadValue<Vector2>();
                        return;
                    }
                    
                    var currentSwipePos = _touchScreenInputActions.Input.TouchPosition.ReadValue<Vector2>();
                    var dir = currentSwipePos - _startSwipePos.Value;
                    if (dir.sqrMagnitude > Threshold * Threshold)
                    {
                        SwipeData swipeData = new(_startSwipePos.Value, currentSwipePos, currentSwipePos - _startSwipePos.Value);
                        EventSwiped?.Invoke(swipeData);
                        _startSwipePos = null;
                    }

                    break;
                }
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    _startSwipePos = null;
                    break;
            }

        }
    }
}