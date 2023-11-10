using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Move : PlayerStateBase
{
    public Homura_Move(PlayerBase playerBase) : base(playerBase)
    {
        // m_animationName.Add("Homura_run");
    }
    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.IsMove = true;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.actionLevel_move;
        // m_IplayerState.ActionLevel = 1;
        m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.animationName_move );
        // m_IplayerComponent.animator.Play(m_animationName[0]);
        m_IplayerState.JumpTimeLeft = HomuraIntelligence.Instance.maxJumpTime;
        m_IplayerState.ActionTimeLeft = HomuraIntelligence.Instance.maxActionTime;
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.moveSpeed * m_IplayerState.MoveTrend;
    }

    public override void ExitState()
    {
        base.ExitState();
        m_IplayerState.IsMove = false;
    }

    public override void Update()
    {
        base.Update();
        if(!m_IplayerState.IsOnGround)
        {
            m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_AirIdle);
        }
        else if(m_IplayerState.MoveTrend == 0)
        {
            m_IplayerComponent.rigidbody2D.velocity = Vector2.zero;
            m_IplayerState.StateMachine.ChangeState(m_IplayerState.State_Idle);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
