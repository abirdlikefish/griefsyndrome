using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class Homura_Idle : PlayerStateBase
{
    public Homura_Idle(PlayerBase playerBase) : base(playerBase)
    {
        // m_animationName.Add("Homura_stand");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.IsIdle = true;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.actionLevel_idle;
        m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.animationName_idle );
        m_IplayerState.JumpTimeLeft = HomuraIntelligence.Instance.maxJumpTime;
        m_IplayerState.ActionTimeLeft = HomuraIntelligence.Instance.maxActionTime;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
    }

    public override void ExitState()
    {
        m_IplayerState.IsIdle = false;
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();
        if(m_IplayerState.MoveTrend != 0)
        {
            m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Move);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(m_IplayerComponent.rigidbody2D.velocity.x != 0)
        {
            float dragSpeed = Mathf.Min( Mathf.Abs(m_IplayerComponent.rigidbody2D.velocity.x) , HomuraIntelligence.Instance.dragSpeed);
            m_IplayerComponent.rigidbody2D.velocity += dragSpeed * Mathf.Sign(m_IplayerComponent.rigidbody2D.velocity.x) * Vector2.left ;
        }
    }
}
