using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Down : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    public Homura_Attack_Down(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = HomuraIntelligence.Instance.granade.maxCombo;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.granade.actionLevel;
        m_IplayerState.IsAttack_Up = true;
        if(m_IplayerState.MoveTrend == 0){}
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.granade.beginSpeed * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.granade.gravityScale;
        Debug.Log(m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.granade.animationName : HomuraIntelligence.Instance.granade.animationName_air);
        m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.granade.animationName : HomuraIntelligence.Instance.granade.animationName_air);
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

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public void Fire()
    {
        Debug.Log("Fire");
    }
}
