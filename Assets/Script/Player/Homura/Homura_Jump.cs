using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Jump : PlayerStateBase
{
    public Homura_Jump(PlayerBase playerBase) : base(playerBase)
    {
        // m_animationName.Add("Homura_jump");
        // m_animationName.Add("Homura_Airjump");
    }

    private bool isAirJump;

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.JumpTimeLeft --;
        m_IplayerState.ActionTimeLeft = HomuraIntelligence.Instance.maxActionTime;
        m_IplayerState.IsChargeOver = false;
        // m_IplayerState.ActionLevel = HomuraIntelligence.Instance.actionLevel_jump;
        m_IplayerState.ActionLevel = 1;
        m_IplayerState.IsJump = true;
        isAirJump = !m_IplayerState.IsOnGround;
        m_IplayerComponent.animator.Play(isAirJump ? HomuraIntelligence.Instance.animationName_airJump : HomuraIntelligence.Instance.animationName_jump);
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        if(HomuraIntelligence.Instance.jumpSpeed > m_IplayerComponent.rigidbody2D.velocity.y)
        {
            m_IplayerComponent.rigidbody2D.velocity += Mathf.Max(HomuraIntelligence.Instance.jumpSpeed - m_IplayerComponent.rigidbody2D.velocity.y , HomuraIntelligence.Instance.jumpSpeed) * Vector2.up;
        }
        // m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.jumpSpeed * Vector2.up;
        
    }

    public override void ExitState()
    {
        m_IplayerState.IsJump = false;
        base.ExitState();
    }

    public override void Update()
    {
        if(m_IplayerComponent.animator.GetCurrentAnimatorStateInfo(0).IsName(isAirJump ? HomuraIntelligence.Instance.animationName_airJump : HomuraIntelligence.Instance.animationName_jump) == false )
        {
            return;
        }
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
