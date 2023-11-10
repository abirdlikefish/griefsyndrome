using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System;

public class Homura_AirIdle : PlayerStateBase
{
    public Homura_AirIdle(PlayerBase playerBase) : base(playerBase)
    {
        // m_animationName.Add("Homura_airIdle");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.JumpTimeLeft = Math.Clamp(m_IplayerState.JumpTimeLeft , 0 , HomuraIntelligence.Instance.maxJumpTime - 1);
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.actionLevel_airIdle;
        m_IplayerState.IsAirIdle = true;
        m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.animationName_airIdle );
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
    }

    public override void ExitState()
    {
        base.ExitState();
        m_IplayerState.IsAirIdle = false;
    }

    public override void Update()
    {
        m_IplayerComponent.rigidbody2D.velocity = new Vector2( 0, m_IplayerComponent.rigidbody2D.velocity.y) + m_IplayerState.MoveTrend * HomuraIntelligence.Instance.moveSpeed;
        if(m_IplayerState.IsOnGround)
        {
            if(m_IplayerState.MoveTrend == 0)
            {
                m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Idle);
            }
            else
            {
                m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Move);
            }
        }
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

