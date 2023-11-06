using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader", order = 1)]
public class InputReader : ScriptableObject, GameInput.IGamePlayActions, GameInput.IUIActions
{   
    private GameInput m_gameInput;

    private void Awake()
    {
        if(m_gameInput == null)
        {
            m_gameInput = new GameInput();
            m_gameInput.GamePlay.SetCallbacks(this);
            m_gameInput.UI.SetCallbacks(this);
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
    }

    public void OnAttack_Front(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_FrontEvent?.Invoke();
        }
    }

    public void OnAttack_Heavy(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_HeavyEvent?.Invoke();
        }
    }

    public void OnAttack_Light(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_LightEvent?.Invoke();
        }
    }

    public void OnAttack_Ultimate(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_UltimateEvent?.Invoke();
        }
    }

    public void OnAttack_Up(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.Attack_UpEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.JumpEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.MoveEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            EventManager.Instance.PauseEvent?.Invoke();
        }
    }

// IUIActions
    public void OnResume(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
