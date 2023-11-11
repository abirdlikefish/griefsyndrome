using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Climb : PlayerStateBase
{
    public Homura_Climb(PlayerBase playerBase) : base(playerBase)
    {
    }
    
    private int m_faceDirection ;
    private bool m_isAtTop;
    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.IsChargeOver = false;
        // m_IplayerState.JumpTimeLeft = Math.Clamp(m_IplayerState.JumpTimeLeft , 0 , HomuraIntelligence.Instance.maxJumpTime - 1);
        m_IplayerState.JumpTimeLeft = HomuraIntelligence.Instance.maxJumpTime;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.actionLevel_climb;
        m_IplayerState.ActionTimeLeft = HomuraIntelligence.Instance.maxActionTime;
        m_IplayerState.IsClimb = true;
        m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.animationName_climb );
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale * HomuraIntelligence.Instance.gravityScaleMultiplier_climb;
        m_IplayerComponent.rigidbody2D.velocity = Vector2.zero;
        m_faceDirection = m_IplayerState.IsFaceRig ? 1 : -1;
        m_isAtTop = false;
    }

    public override void ExitState()
    {
        base.ExitState();
        m_IplayerState.IsClimb = false;
        if(!m_isAtTop)
        {
            m_IplayerComponent.rigidbody2D.velocity -= Vector2.right * m_faceDirection * HomuraIntelligence.Instance.escapeSpeed;
        }
    }

    public override void Update()
    {
        base.Update();
        if(m_IplayerState.MoveTrend == 0)
        {
            return;
        }
        Debug.DrawLine(m_IplayerComponent.rigidbody2D.position + m_faceDirection * Vector2.right * 0.2f + Vector2.down * 0.4f , m_IplayerComponent.rigidbody2D.position + m_faceDirection * Vector2.right * 0.3f - Vector2.down * 0.4f  , Color.red);
        LayerMask layerMask_ground = (1 << 8);
        // Collider2D midCollider2D = Physics2D.OverlapCircle(m_IplayerComponent.rigidbody2D.position + m_faceDirection * Vector2.right * 0.2f , 0.25f , layerMask_ground);
        Collider2D midCollider2D = Physics2D.OverlapArea(m_IplayerComponent.rigidbody2D.position + m_faceDirection * Vector2.right * 0.2f + Vector2.down * 0.4f , m_IplayerComponent.rigidbody2D.position + m_faceDirection * Vector2.right * 0.3f - Vector2.down * 0.4f , layerMask_ground);
        if(midCollider2D == null)
        {
            m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_AirIdle);
            return ;
        }
        RaycastHit2D midRaycastHit2D = Physics2D.Raycast(m_IplayerComponent.rigidbody2D.position + Vector2.up * 0.5f + m_faceDirection * Vector2.right * 0.2f , m_faceDirection * Vector2.right , 1 , layerMask_ground);
        float move_y = 0;
        if(!midRaycastHit2D)
        {
            midRaycastHit2D = Physics2D.Raycast(m_IplayerComponent.rigidbody2D.position + Vector2.up * (move_y - 0.5f) + m_faceDirection * Vector2.right * 0.2f , m_faceDirection * Vector2.right , 1 , layerMask_ground);
            while(midRaycastHit2D)
            {
                move_y += 0.1f;
                midRaycastHit2D = Physics2D.Raycast(m_IplayerComponent.rigidbody2D.position + Vector2.up * (move_y - 0.5f) + m_faceDirection * Vector2.right * 0.2f , m_faceDirection * Vector2.right , 1 , layerMask_ground);
            }
            m_IplayerComponent.rigidbody2D.MovePosition(m_IplayerComponent.rigidbody2D.position + Vector2.up * move_y + m_faceDirection * Vector2.right * 0.5f);
            m_isAtTop = true;
            m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Idle);
        }
    }

/*
0 up
1 top
2 drop
*/
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

