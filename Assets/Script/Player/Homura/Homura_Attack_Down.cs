using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Down : PlayerStateBase
{
    public Homura_Attack_Down(PlayerBase playerBase) : base(playerBase)
    {
        m_animationName.Add("Homura_granade");
        m_animationName.Add("Homura_granadeAir");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = 1;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = 4;
        m_IplayerState.IsAttack_Down = true;
        if(m_IplayerState.MoveTrend == 0){}
        m_IplayerComponent.rigidbody2D.velocity = Vector2.zero;
        m_IplayerComponent.rigidbody2D.gravityScale = 0;
        if(m_IplayerState.IsOnGround)
        {
            m_animationNameIndex = 0;
        }
        else
        {
            m_animationNameIndex = 1;
        }
        m_IplayerComponent.animator.Play(m_animationName[m_animationNameIndex]);
    }

    public override void ExitState()
    {
        m_IplayerComponent.rigidbody2D.gravityScale = 1;
        m_IplayerState.IsAttack_Down = false;
        m_IplayerState.ActionTimeLeft --;
        base.ExitState();
    }
    public override void Update()
    {
        if(!m_IplayerComponent.animator.GetCurrentAnimatorStateInfo(0).IsName(m_animationName[m_animationNameIndex]))
        {
            return;
        }
        base.Update();
        float midNormalizedTime = m_IplayerComponent.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(midNormalizedTime >= 1.0f)
        {
            if(m_IplayerState.IsOnGround)
            {
                m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Idle);
            }
            else
            {
                m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_AirIdle);
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
