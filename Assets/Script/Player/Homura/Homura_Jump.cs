using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Jump : PlayerStateBase
{
    public Homura_Jump(PlayerBase playerBase) : base(playerBase)
    {
        m_animationName.Add("Homura_jump");
        m_animationName.Add("Homura_Airjump");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.JumpTimeLeft --;
        m_IplayerState.ActionTimeLeft = HomuraIntelligence.Instance.maxActionTime;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = 5;
        m_IplayerState.IsJump = true;
        if(m_IplayerState.IsOnGround)
        {
            m_animationNameIndex = 0;
        }
        else
        {
            m_animationNameIndex = 1;
        }
        m_IplayerComponent.animator.Play(m_animationName[m_animationNameIndex]);
        
        // m_IplayerComponent.rigidbody2D.velocity = new Vector2(m_IplayerState.MoveTrend , 5);
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.jumpSpeed + HomuraIntelligence.Instance.moveSpeed * m_IplayerState.MoveTrend;
    }

    public override void ExitState()
    {
        m_IplayerState.IsJump = false;
        base.ExitState();
    }

    public override void Update()
    {
        if(m_IplayerComponent.animator.GetCurrentAnimatorStateInfo(0).IsName(m_animationName[m_animationNameIndex]) == false )
        {
            return;
        }
        base.Update();
        if(m_IplayerComponent.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_AirIdle);
        }
        if(m_IplayerComponent.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f / 4.0f)
        {
            m_IplayerState.ActionLevel = 1;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
