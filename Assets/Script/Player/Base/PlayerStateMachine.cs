using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    protected IPlayerState m_IplayerState;
    protected PlayerStateBase m_playerState;
    public PlayerStateMachine(IPlayerState playerState)
    {
        this.m_IplayerState = playerState;
    }

    public void ChangeState(PlayerStateBase playerState)
    {
        this.m_playerState.ExitState();
        this.m_playerState = playerState;
        this.m_playerState.EnterState();
// Debug.Log("change state");
    }

    public void Initialization(PlayerStateBase playerState)
    {
        this.m_playerState = playerState;
        this.m_playerState.EnterState();
    }

    public void Update()
    {
        this.m_playerState.Update();
    }

    public void FixedUpdate()
    {
        this.m_playerState.FixedUpdate();
    }

}
