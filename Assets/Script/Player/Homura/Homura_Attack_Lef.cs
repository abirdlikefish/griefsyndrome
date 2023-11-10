using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Lef : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    public Homura_Attack_Lef(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = HomuraIntelligence.Instance.RPG.maxCombo;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.RPG.actionLevel;
        m_IplayerState.IsAttack_Lef = true;
        if(m_IplayerState.MoveTrend == 0){}
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.RPG.beginSpeed * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.RPG.gravityScale;
        m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.RPG.animationName : HomuraIntelligence.Instance.RPG.animationName_air);
        m_IHomuraAnimationEvent.Fire += this.Fire;
    }

    public override void ExitState()
    {
        m_IplayerState.IsAttack_Lef = false;
        m_IplayerState.ActionTimeLeft --;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        m_IHomuraAnimationEvent.Fire -= this.Fire;
        base.ExitState();
    }

    public void Fire()
    {
        Debug.Log("Fire");
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
