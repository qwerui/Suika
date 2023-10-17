//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/InputSystem/TouchAction.inputactions
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

public partial class @TouchAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchAction"",
    ""maps"": [
        {
            ""name"": ""GameAction"",
            ""id"": ""18ce457f-e5cf-46da-ac59-b42ae3ae3202"",
            ""actions"": [
                {
                    ""name"": ""TouchInput"",
                    ""type"": ""Button"",
                    ""id"": ""5d1a07e2-7eca-4a1f-ab05-5ccf3b55c99c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2c72c8b3-436d-4ad8-9f9c-c0dee128dd64"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3e057dce-41f4-4885-ae0b-043459d2f688"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20e6067d-ca2e-4e85-9817-ebcc06c42c34"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": []
        }
    ]
}");
        // GameAction
        m_GameAction = asset.FindActionMap("GameAction", throwIfNotFound: true);
        m_GameAction_TouchInput = m_GameAction.FindAction("TouchInput", throwIfNotFound: true);
        m_GameAction_TouchPosition = m_GameAction.FindAction("TouchPosition", throwIfNotFound: true);
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

    // GameAction
    private readonly InputActionMap m_GameAction;
    private List<IGameActionActions> m_GameActionActionsCallbackInterfaces = new List<IGameActionActions>();
    private readonly InputAction m_GameAction_TouchInput;
    private readonly InputAction m_GameAction_TouchPosition;
    public struct GameActionActions
    {
        private @TouchAction m_Wrapper;
        public GameActionActions(@TouchAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchInput => m_Wrapper.m_GameAction_TouchInput;
        public InputAction @TouchPosition => m_Wrapper.m_GameAction_TouchPosition;
        public InputActionMap Get() { return m_Wrapper.m_GameAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActionActions set) { return set.Get(); }
        public void AddCallbacks(IGameActionActions instance)
        {
            if (instance == null || m_Wrapper.m_GameActionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameActionActionsCallbackInterfaces.Add(instance);
            @TouchInput.started += instance.OnTouchInput;
            @TouchInput.performed += instance.OnTouchInput;
            @TouchInput.canceled += instance.OnTouchInput;
            @TouchPosition.started += instance.OnTouchPosition;
            @TouchPosition.performed += instance.OnTouchPosition;
            @TouchPosition.canceled += instance.OnTouchPosition;
        }

        private void UnregisterCallbacks(IGameActionActions instance)
        {
            @TouchInput.started -= instance.OnTouchInput;
            @TouchInput.performed -= instance.OnTouchInput;
            @TouchInput.canceled -= instance.OnTouchInput;
            @TouchPosition.started -= instance.OnTouchPosition;
            @TouchPosition.performed -= instance.OnTouchPosition;
            @TouchPosition.canceled -= instance.OnTouchPosition;
        }

        public void RemoveCallbacks(IGameActionActions instance)
        {
            if (m_Wrapper.m_GameActionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameActionActions instance)
        {
            foreach (var item in m_Wrapper.m_GameActionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameActionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameActionActions @GameAction => new GameActionActions(this);
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface IGameActionActions
    {
        void OnTouchInput(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
    }
}