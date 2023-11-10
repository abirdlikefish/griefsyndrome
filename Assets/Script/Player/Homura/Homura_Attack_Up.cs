using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Up : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    public Homura_Attack_Up(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
    }
    private bool m_isOnGround ;
// granade_front
    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.IsAttack_Up = true;
        if(m_IplayerState.MoveTrend == 0){}
        if(m_IplayerState.IsOnGround)
        {
            m_IplayerState.MaxCombo = HomuraIntelligence.Instance.mortar.maxCombo;
            m_IplayerState.ActionLevel = HomuraIntelligence.Instance.mortar.actionLevel;
            m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.mortar.beginSpeed * (m_IplayerState.IsFaceRig ? 1 : -1) ;
            m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.mortar.gravityScale;
            m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.mortar.animationName);
            m_isOnGround = true;
        }
        else
        {
            m_IplayerState.MaxCombo = HomuraIntelligence.Instance.granade_front.maxCombo;
            m_IplayerState.ActionLevel = HomuraIntelligence.Instance.granade_front.actionLevel;
            m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.granade_front.beginSpeed * (m_IplayerState.IsFaceRig ? 1 : -1) ;
            m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.granade_front.gravityScale;
            m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.granade_front.animationName);
            m_isOnGround = false;
        }
        m_IHomuraAnimationEvent.Fire += this.Fire;
    }

    public override void ExitState()
    {
        m_IplayerState.IsAttack_Up = false;
        m_IplayerState.ActionTimeLeft --;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        m_IHomuraAnimationEvent.Fire -= this.Fire;
        base.ExitState();
    }
    public void Fire()
    {
        if(m_isOnGround)
        {
            Debug.Log("Fire");
        }
        else
        {
            Debug.Log("Fire");
        }
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
