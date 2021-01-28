// GENERATED AUTOMATICALLY FROM 'Assets/3 - Scripts/InputSystem/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""31caeecc-2f3f-49e8-b971-305c7d3cb46e"",
            ""actions"": [
                {
                    ""name"": ""Slingshot Slow Motion"",
                    ""type"": ""Button"",
                    ""id"": ""65e8d111-41f5-4867-9bb4-e7a70d92bb99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slingshot Movement Direction Gamepad"",
                    ""type"": ""Value"",
                    ""id"": ""e4051e2c-10b4-4d29-b3eb-d40bb5ebd129"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slingshot Movement Direction Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""131f0c4b-b208-4359-9d91-9550ef65394c"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1218ea2c-43d1-4c41-a9ad-861c06151168"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Slingshot Slow Motion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""764ca119-fbf2-48ee-997d-51a881b460aa"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Slingshot Slow Motion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ccf60a7-35c1-4176-9f97-4b328f6b51b9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Slingshot Slow Motion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4e2a7a08-ab87-4948-92a1-0b653d66967e"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slingshot Movement Direction Gamepad"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""28237243-b644-40e1-a40d-747219da07fe"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Slingshot Movement Direction Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""376738fd-7697-4b11-912c-9e5bb1fe3329"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Slingshot Movement Direction Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""81ec754d-21c1-48fd-9c78-92c0a37cc552"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Slingshot Movement Direction Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""959658d9-8499-47dc-a34a-93cddf1c18f7"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Slingshot Movement Direction Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb477f0c-9b48-4b77-b866-9ed869bee644"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Slingshot Movement Direction Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""0ccc477b-c2e6-4b93-9cb6-645bcf013cbb"",
            ""actions"": [
                {
                    ""name"": ""Next Scene"",
                    ""type"": ""Button"",
                    ""id"": ""e03ccde1-d7ad-4c7a-a9b1-1ea5e7736930"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Previous Scene"",
                    ""type"": ""Button"",
                    ""id"": ""7b6eaad8-d389-4378-b7d8-afa71b94b1bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f5c9d30c-382c-420e-be4c-7da7b8dc41bc"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Next Scene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43be77c6-0955-42d4-8e13-35ebb07763a4"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Previous Scene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_SlingshotSlowMotion = m_Gameplay.FindAction("Slingshot Slow Motion", throwIfNotFound: true);
        m_Gameplay_SlingshotMovementDirectionGamepad = m_Gameplay.FindAction("Slingshot Movement Direction Gamepad", throwIfNotFound: true);
        m_Gameplay_SlingshotMovementDirectionMouse = m_Gameplay.FindAction("Slingshot Movement Direction Mouse", throwIfNotFound: true);
        // Debug
        m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
        m_Debug_NextScene = m_Debug.FindAction("Next Scene", throwIfNotFound: true);
        m_Debug_PreviousScene = m_Debug.FindAction("Previous Scene", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_SlingshotSlowMotion;
    private readonly InputAction m_Gameplay_SlingshotMovementDirectionGamepad;
    private readonly InputAction m_Gameplay_SlingshotMovementDirectionMouse;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SlingshotSlowMotion => m_Wrapper.m_Gameplay_SlingshotSlowMotion;
        public InputAction @SlingshotMovementDirectionGamepad => m_Wrapper.m_Gameplay_SlingshotMovementDirectionGamepad;
        public InputAction @SlingshotMovementDirectionMouse => m_Wrapper.m_Gameplay_SlingshotMovementDirectionMouse;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @SlingshotSlowMotion.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotSlowMotion;
                @SlingshotSlowMotion.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotSlowMotion;
                @SlingshotSlowMotion.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotSlowMotion;
                @SlingshotMovementDirectionGamepad.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotMovementDirectionGamepad;
                @SlingshotMovementDirectionGamepad.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotMovementDirectionGamepad;
                @SlingshotMovementDirectionGamepad.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotMovementDirectionGamepad;
                @SlingshotMovementDirectionMouse.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotMovementDirectionMouse;
                @SlingshotMovementDirectionMouse.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotMovementDirectionMouse;
                @SlingshotMovementDirectionMouse.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSlingshotMovementDirectionMouse;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SlingshotSlowMotion.started += instance.OnSlingshotSlowMotion;
                @SlingshotSlowMotion.performed += instance.OnSlingshotSlowMotion;
                @SlingshotSlowMotion.canceled += instance.OnSlingshotSlowMotion;
                @SlingshotMovementDirectionGamepad.started += instance.OnSlingshotMovementDirectionGamepad;
                @SlingshotMovementDirectionGamepad.performed += instance.OnSlingshotMovementDirectionGamepad;
                @SlingshotMovementDirectionGamepad.canceled += instance.OnSlingshotMovementDirectionGamepad;
                @SlingshotMovementDirectionMouse.started += instance.OnSlingshotMovementDirectionMouse;
                @SlingshotMovementDirectionMouse.performed += instance.OnSlingshotMovementDirectionMouse;
                @SlingshotMovementDirectionMouse.canceled += instance.OnSlingshotMovementDirectionMouse;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Debug
    private readonly InputActionMap m_Debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_Debug_NextScene;
    private readonly InputAction m_Debug_PreviousScene;
    public struct DebugActions
    {
        private @PlayerControls m_Wrapper;
        public DebugActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @NextScene => m_Wrapper.m_Debug_NextScene;
        public InputAction @PreviousScene => m_Wrapper.m_Debug_PreviousScene;
        public InputActionMap Get() { return m_Wrapper.m_Debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                @NextScene.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnNextScene;
                @NextScene.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnNextScene;
                @NextScene.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnNextScene;
                @PreviousScene.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnPreviousScene;
                @PreviousScene.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnPreviousScene;
                @PreviousScene.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnPreviousScene;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @NextScene.started += instance.OnNextScene;
                @NextScene.performed += instance.OnNextScene;
                @NextScene.canceled += instance.OnNextScene;
                @PreviousScene.started += instance.OnPreviousScene;
                @PreviousScene.performed += instance.OnPreviousScene;
                @PreviousScene.canceled += instance.OnPreviousScene;
            }
        }
    }
    public DebugActions @Debug => new DebugActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnSlingshotSlowMotion(InputAction.CallbackContext context);
        void OnSlingshotMovementDirectionGamepad(InputAction.CallbackContext context);
        void OnSlingshotMovementDirectionMouse(InputAction.CallbackContext context);
    }
    public interface IDebugActions
    {
        void OnNextScene(InputAction.CallbackContext context);
        void OnPreviousScene(InputAction.CallbackContext context);
    }
}
