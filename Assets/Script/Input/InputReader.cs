using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader", order = 1)]
public class InputReader : ScriptableObject, GameInput.IGamePlayActions, GameInput.IUIActions
{
    public static InputReader Instance { get; private set; }
    private GameInput m_gameInput;

    // public void Initialization()
    // {
    //     if(m_gameInput == null)
    //     {
    //         m_gameInput = new GameInput();
    //         m_gameInput.GamePlay.SetCallbacks(this);
    //         m_gameInput.UI.SetCallbacks(this);
    //         UseGamePlayAction();
    //     }
    // }

    // public InputReader()
    private void OnEnable()
    {
        Debug.Log("scriptable object constructor");
        if(Instance == null)
        {
            Instance = this;
        }
        if(m_gameInput == null)
        {
            m_gameInput = new GameInput();
            m_gameInput.GamePlay.SetCallbacks(this);
            m_gameInput.UI.SetCallbacks(this);
            UseGamePlayAction();
        }
    }

    private void UseGamePlayAction()
    {
            m_gameInput.GamePlay.Enable();
            m_gameInput.UI.Disable();
    }
    private void UseUIAction()
    {
        m_gameInput.GamePlay.Disable();
        m_gameInput.UI.Enable();
    }

// IGamePlayActions
    public void OnAttack_Down(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_DownEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Attack_Down_CanceledEvent?.Invoke();
        }
    }

    public void OnAttack_Heavy(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_HeavyEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Attack_Heavy_CanceledEvent?.Invoke();
        }
    }

    public void OnAttack_Light(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_LightEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Attack_Light_CanceledEvent?.Invoke();
        }
    }

    public void OnAttack_Ultimate(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_UltimateEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Attack_Ultimate_CanceledEvent?.Invoke();
        }
    }

    public void OnAttack_Up(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_UpEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Attack_Up_CanceledEvent?.Invoke();
        }
    }

    public void OnAttack_Lef(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_LefEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Attack_Lef_CanceledEvent?.Invoke();
        }
    }

    public void OnAttack_Rig(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_RigEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Attack_Rig_CanceledEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.JumpEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Jump_CanceledEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            EventManager.Instance.MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            EventManager.Instance.Move_CanceledEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            UseUIAction();
            EventManager.Instance.PauseEvent?.Invoke();
        }
    }

// IUIActions
    public void OnResume(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            UseGamePlayAction();
            EventManager.Instance.ResumeEvent?.Invoke();
        }
    }

}
