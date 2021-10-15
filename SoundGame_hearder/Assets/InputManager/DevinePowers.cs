// GENERATED AUTOMATICALLY FROM 'Assets/InputManager/DevinePowers.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DevinePowers : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DevinePowers()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DevinePowers"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""71c5f044-3520-4698-a5bb-30477c639839"",
            ""actions"": [
                {
                    ""name"": ""Clap"",
                    ""type"": ""Button"",
                    ""id"": ""343a9be9-c56d-48e9-9803-a14becb9e5f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Rub"",
                    ""type"": ""Button"",
                    ""id"": ""6a66e888-be4f-44e0-a880-3a5d2c71caf0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""feb95ee1-a10a-4f6a-8724-d2746a070813"",
                    ""path"": ""<OculusTouchController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Clap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ba47d64-0590-4bc8-850b-fcb48f889c24"",
                    ""path"": ""<OculusTouchController>{LeftHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Clap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3006abf-88d1-4a7c-83b1-0852b3ddab9b"",
                    ""path"": ""<OculusTouchController>{RightHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rub"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4be0f283-2973-4d0b-9ea3-bff90266e95b"",
                    ""path"": ""<OculusTouchController>{LeftHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rub"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_Clap = m_Default.FindAction("Clap", throwIfNotFound: true);
        m_Default_Rub = m_Default.FindAction("Rub", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_Clap;
    private readonly InputAction m_Default_Rub;
    public struct DefaultActions
    {
        private @DevinePowers m_Wrapper;
        public DefaultActions(@DevinePowers wrapper) { m_Wrapper = wrapper; }
        public InputAction @Clap => m_Wrapper.m_Default_Clap;
        public InputAction @Rub => m_Wrapper.m_Default_Rub;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @Clap.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClap;
                @Clap.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClap;
                @Clap.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClap;
                @Rub.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRub;
                @Rub.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRub;
                @Rub.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRub;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Clap.started += instance.OnClap;
                @Clap.performed += instance.OnClap;
                @Clap.canceled += instance.OnClap;
                @Rub.started += instance.OnRub;
                @Rub.performed += instance.OnRub;
                @Rub.canceled += instance.OnRub;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnClap(InputAction.CallbackContext context);
        void OnRub(InputAction.CallbackContext context);
    }
}
