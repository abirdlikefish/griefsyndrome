using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase
{
    protected IPlayerState m_IplayerState;
    protected IPlayerComponent m_IplayerComponent;
    protected List<string> m_animationName;
    public PlayerStateBase(PlayerBase playerBase)
    {
        this.m_IplayerState = playerBase;
        this.m_IplayerComponent = playerBase;
        this.m_animationName = new List<string>();
    }
    public virtual void EnterState()
    {
    }

    public virtual void ExitState()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

}
