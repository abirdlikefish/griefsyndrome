using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_AirIdle : PlayerStateBase
{
    public Homura_AirIdle(PlayerBase playerBase) : base(playerBase)
    {
        m_animationName.Add("Homura_airIdle");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = 1;
        m_IplayerComponent.animator.Play(m_animationName[0] );
        m_IplayerState.IsAirIdle = true;
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

