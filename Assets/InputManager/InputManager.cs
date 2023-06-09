//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputManager/InputManager.inputactions
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

public partial class @InputManager : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""CueStickRotation"",
            ""id"": ""f1cddf64-a9b0-4bc2-b309-16e0fa906039"",
            ""actions"": [
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""a79da3ed-2e9b-4802-9b83-88e2d09a631f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Axis"",
                    ""type"": ""Value"",
                    ""id"": ""545dda2d-df92-4a82-b1c1-bbbd73b129fa"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3682b9f6-f105-407c-9f0e-2764be5359c1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b78c81a-7535-4409-96a1-3bc1d7261977"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BallInHand"",
            ""id"": ""92c71a34-eefe-4332-9b12-5d8c9404efe2"",
            ""actions"": [
                {
                    ""name"": ""ClickPressed"",
                    ""type"": ""Button"",
                    ""id"": ""d9ff5295-8902-4b73-8d91-7a9f8eafa723"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d664a668-e970-4a90-ba68-857069b6d260"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CueStickRotation
        m_CueStickRotation = asset.FindActionMap("CueStickRotation", throwIfNotFound: true);
        m_CueStickRotation_Press = m_CueStickRotation.FindAction("Press", throwIfNotFound: true);
        m_CueStickRotation_Axis = m_CueStickRotation.FindAction("Axis", throwIfNotFound: true);
        // BallInHand
        m_BallInHand = asset.FindActionMap("BallInHand", throwIfNotFound: true);
        m_BallInHand_ClickPressed = m_BallInHand.FindAction("ClickPressed", throwIfNotFound: true);
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

    // CueStickRotation
    private readonly InputActionMap m_CueStickRotation;
    private ICueStickRotationActions m_CueStickRotationActionsCallbackInterface;
    private readonly InputAction m_CueStickRotation_Press;
    private readonly InputAction m_CueStickRotation_Axis;
    public struct CueStickRotationActions
    {
        private @InputManager m_Wrapper;
        public CueStickRotationActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Press => m_Wrapper.m_CueStickRotation_Press;
        public InputAction @Axis => m_Wrapper.m_CueStickRotation_Axis;
        public InputActionMap Get() { return m_Wrapper.m_CueStickRotation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CueStickRotationActions set) { return set.Get(); }
        public void SetCallbacks(ICueStickRotationActions instance)
        {
            if (m_Wrapper.m_CueStickRotationActionsCallbackInterface != null)
            {
                @Press.started -= m_Wrapper.m_CueStickRotationActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_CueStickRotationActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_CueStickRotationActionsCallbackInterface.OnPress;
                @Axis.started -= m_Wrapper.m_CueStickRotationActionsCallbackInterface.OnAxis;
                @Axis.performed -= m_Wrapper.m_CueStickRotationActionsCallbackInterface.OnAxis;
                @Axis.canceled -= m_Wrapper.m_CueStickRotationActionsCallbackInterface.OnAxis;
            }
            m_Wrapper.m_CueStickRotationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
                @Axis.started += instance.OnAxis;
                @Axis.performed += instance.OnAxis;
                @Axis.canceled += instance.OnAxis;
            }
        }
    }
    public CueStickRotationActions @CueStickRotation => new CueStickRotationActions(this);

    // BallInHand
    private readonly InputActionMap m_BallInHand;
    private IBallInHandActions m_BallInHandActionsCallbackInterface;
    private readonly InputAction m_BallInHand_ClickPressed;
    public struct BallInHandActions
    {
        private @InputManager m_Wrapper;
        public BallInHandActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @ClickPressed => m_Wrapper.m_BallInHand_ClickPressed;
        public InputActionMap Get() { return m_Wrapper.m_BallInHand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BallInHandActions set) { return set.Get(); }
        public void SetCallbacks(IBallInHandActions instance)
        {
            if (m_Wrapper.m_BallInHandActionsCallbackInterface != null)
            {
                @ClickPressed.started -= m_Wrapper.m_BallInHandActionsCallbackInterface.OnClickPressed;
                @ClickPressed.performed -= m_Wrapper.m_BallInHandActionsCallbackInterface.OnClickPressed;
                @ClickPressed.canceled -= m_Wrapper.m_BallInHandActionsCallbackInterface.OnClickPressed;
            }
            m_Wrapper.m_BallInHandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ClickPressed.started += instance.OnClickPressed;
                @ClickPressed.performed += instance.OnClickPressed;
                @ClickPressed.canceled += instance.OnClickPressed;
            }
        }
    }
    public BallInHandActions @BallInHand => new BallInHandActions(this);
    public interface ICueStickRotationActions
    {
        void OnPress(InputAction.CallbackContext context);
        void OnAxis(InputAction.CallbackContext context);
    }
    public interface IBallInHandActions
    {
        void OnClickPressed(InputAction.CallbackContext context);
    }
}
