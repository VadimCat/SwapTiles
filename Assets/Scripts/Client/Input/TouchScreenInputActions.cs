//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Client/Input/TouchScreenInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @TouchScreenInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchScreenInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchScreenInputActions"",
    ""maps"": [
        {
            ""name"": ""Input"",
            ""id"": ""46db3850-4261-4f25-8285-c18aa86e247f"",
            ""actions"": [
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""16e18be4-e1ba-485a-93d5-583835dd0c74"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPhase"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b899c5ad-c3b1-4836-9445-d4a7f9c5d4c1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8cef35af-5747-4410-9d52-266e8831a9bc"",
                    ""path"": ""<Touchscreen>/primaryTouch/phase"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPhase"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a048fe3e-9dba-48b2-b918-288b57009bf2"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Input
        m_Input = asset.FindActionMap("Input", throwIfNotFound: true);
        m_Input_TouchPosition = m_Input.FindAction("TouchPosition", throwIfNotFound: true);
        m_Input_TouchPhase = m_Input.FindAction("TouchPhase", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Input
    private readonly InputActionMap m_Input;
    private IInputActions m_InputActionsCallbackInterface;
    private readonly InputAction m_Input_TouchPosition;
    private readonly InputAction m_Input_TouchPhase;
    public struct InputActions
    {
        private @TouchScreenInputActions m_Wrapper;
        public InputActions(@TouchScreenInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchPosition => m_Wrapper.m_Input_TouchPosition;
        public InputAction @TouchPhase => m_Wrapper.m_Input_TouchPhase;
        public InputActionMap Get() { return m_Wrapper.m_Input; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputActions set) { return set.Get(); }
        public void SetCallbacks(IInputActions instance)
        {
            if (m_Wrapper.m_InputActionsCallbackInterface != null)
            {
                @TouchPosition.started -= m_Wrapper.m_InputActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnTouchPosition;
                @TouchPhase.started -= m_Wrapper.m_InputActionsCallbackInterface.OnTouchPhase;
                @TouchPhase.performed -= m_Wrapper.m_InputActionsCallbackInterface.OnTouchPhase;
                @TouchPhase.canceled -= m_Wrapper.m_InputActionsCallbackInterface.OnTouchPhase;
            }
            m_Wrapper.m_InputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
                @TouchPhase.started += instance.OnTouchPhase;
                @TouchPhase.performed += instance.OnTouchPhase;
                @TouchPhase.canceled += instance.OnTouchPhase;
            }
        }
    }
    public InputActions @Input => new InputActions(this);
    public interface IInputActions
    {
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnTouchPhase(InputAction.CallbackContext context);
    }
}
