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
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
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
                return;
            }
            else
            {
                m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Move);
                return;
            }
        }
        Debug.DrawLine(m_IplayerComponent.rigidbody2D.position + m_IplayerState.MoveTrend * Vector2.right * 0.2f + Vector2.down * 0.4f , m_IplayerComponent.rigidbody2D.position + m_IplayerState.MoveTrend * Vector2.right * 0.3f - Vector2.down * 0.4f  , Color.green);
        LayerMask layerMask_ground = (1 << 8);
        Collider2D midCollider2D = Physics2D.OverlapArea(m_IplayerComponent.rigidbody2D.position + m_IplayerState.MoveTrend * Vector2.right * 0.2f + Vector2.down * 0.4f , m_IplayerComponent.rigidbody2D.position + m_IplayerState.MoveTrend * Vector2.right * 0.3f - Vector2.down * 0.4f , layerMask_ground);
        // RaycastHit2D midRaycastHit2D = Physics2D.Raycast(m_IplayerComponent.rigidbody2D.position + Vector2.up * 0.5f + m_IplayerState.MoveTrend * Vector2.right * 0.2f , m_IplayerState.MoveTrend * Vector2.right , 1 , layerMask_ground);
        if(midCollider2D)
        {
            m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Climb);
            return;
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

