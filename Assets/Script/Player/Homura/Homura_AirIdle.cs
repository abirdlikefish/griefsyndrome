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

    private int m_jumpStage = 0;
    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.JumpTimeLeft = Math.Clamp(m_IplayerState.JumpTimeLeft , 0 , HomuraIntelligence.Instance.maxJumpTime - 1);
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.actionLevel_airIdle;
        m_IplayerState.IsAirIdle = true;
        m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.animationName_airIdle );
        m_jumpStage = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
        m_IplayerState.IsAirIdle = false;
    }

    public override void Update()
    {
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

/*
0 up
1 top
2 drop
*/
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(m_jumpStage == 0 && Mathf.Abs(m_IplayerComponent.rigidbody2D.velocity.y) < HomuraIntelligence.Instance.judgeIsTop_speed)
        {
            m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScaleMultiplier_top * HomuraIntelligence.Instance.gravityScale;
            m_jumpStage = 1;
        }
        if(m_jumpStage <= 1 && m_IplayerComponent.rigidbody2D.velocity.y < 0 && Mathf.Abs(m_IplayerComponent.rigidbody2D.velocity.y) > HomuraIntelligence.Instance.judgeIsTop_speed)
        {
            m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScaleMultiplier_drop * HomuraIntelligence.Instance.gravityScale;
            m_jumpStage = 2;
        }
        // m_IplayerComponent.rigidbody2D.velocity = new Vector2( 0, m_IplayerComponent.rigidbody2D.velocity.y) + m_IplayerState.MoveTrend * HomuraIntelligence.Instance.moveSpeed;
        
        if(m_IplayerState.MoveTrend == 0)
        {
            float dragSpeed = Mathf.Min( Mathf.Abs(m_IplayerComponent.rigidbody2D.velocity.x) , HomuraIntelligence.Instance.dragSpeed_air);
            m_IplayerComponent.rigidbody2D.velocity += dragSpeed * Mathf.Sign(m_IplayerComponent.rigidbody2D.velocity.x) * Vector2.left ;
        }
        else
        {
            float diffSpeed = HomuraIntelligence.Instance.maxSpeed_air * m_IplayerState.MoveTrend - m_IplayerComponent.rigidbody2D.velocity.x;
            m_IplayerComponent.rigidbody2D.velocity += diffSpeed * HomuraIntelligence.Instance.diffSpeedMultiplier_air * Vector2.right;
        }
    }
}

