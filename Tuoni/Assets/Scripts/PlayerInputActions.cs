//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerDefault"",
            ""id"": ""eece80ee-6531-4bb4-8d9b-ad2fdc336667"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""92fb28a3-297d-4155-ada1-20a5c5fed898"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TakeDamage"",
                    ""type"": ""Button"",
                    ""id"": ""403890b8-ba01-4423-89fc-4482407b3bd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DealDamage"",
                    ""type"": ""Button"",
                    ""id"": ""3e95410b-5458-44e5-9021-d2826e2872cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ec70cf2c-41dc-4a6f-ba74-e7c1549f4d7b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bc8e709a-7714-434f-8528-68b5f8801c77"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5c1e03fd-8857-44a2-ab43-f96257801749"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e0272813-ada3-4613-963e-667ce9f3bd42"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6489137a-82da-4eee-a506-b67e8f39426e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9e55bb9c-cb11-4ca2-9600-a438f6d2b2ce"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeDamage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24877119-f6e9-42ee-a404-6cb95f642173"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DealDamage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerDefault
        m_PlayerDefault = asset.FindActionMap("PlayerDefault", throwIfNotFound: true);
        m_PlayerDefault_Movement = m_PlayerDefault.FindAction("Movement", throwIfNotFound: true);
        m_PlayerDefault_TakeDamage = m_PlayerDefault.FindAction("TakeDamage", throwIfNotFound: true);
        m_PlayerDefault_DealDamage = m_PlayerDefault.FindAction("DealDamage", throwIfNotFound: true);
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

    // PlayerDefault
    private readonly InputActionMap m_PlayerDefault;
    private List<IPlayerDefaultActions> m_PlayerDefaultActionsCallbackInterfaces = new List<IPlayerDefaultActions>();
    private readonly InputAction m_PlayerDefault_Movement;
    private readonly InputAction m_PlayerDefault_TakeDamage;
    private readonly InputAction m_PlayerDefault_DealDamage;
    public struct PlayerDefaultActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerDefaultActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerDefault_Movement;
        public InputAction @TakeDamage => m_Wrapper.m_PlayerDefault_TakeDamage;
        public InputAction @DealDamage => m_Wrapper.m_PlayerDefault_DealDamage;
        public InputActionMap Get() { return m_Wrapper.m_PlayerDefault; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerDefaultActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerDefaultActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @TakeDamage.started += instance.OnTakeDamage;
            @TakeDamage.performed += instance.OnTakeDamage;
            @TakeDamage.canceled += instance.OnTakeDamage;
            @DealDamage.started += instance.OnDealDamage;
            @DealDamage.performed += instance.OnDealDamage;
            @DealDamage.canceled += instance.OnDealDamage;
        }

        private void UnregisterCallbacks(IPlayerDefaultActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @TakeDamage.started -= instance.OnTakeDamage;
            @TakeDamage.performed -= instance.OnTakeDamage;
            @TakeDamage.canceled -= instance.OnTakeDamage;
            @DealDamage.started -= instance.OnDealDamage;
            @DealDamage.performed -= instance.OnDealDamage;
            @DealDamage.canceled -= instance.OnDealDamage;
        }

        public void RemoveCallbacks(IPlayerDefaultActions instance)
        {
            if (m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerDefaultActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerDefaultActions @PlayerDefault => new PlayerDefaultActions(this);
    public interface IPlayerDefaultActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnTakeDamage(InputAction.CallbackContext context);
        void OnDealDamage(InputAction.CallbackContext context);
    }
}
