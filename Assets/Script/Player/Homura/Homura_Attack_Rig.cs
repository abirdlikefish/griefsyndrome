using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Rig : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    public Homura_Attack_Rig(PlayerBase playerBase) : base(playerBase)
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
        m_IplayerState.IsAttack_Rig = true;
        if(m_IplayerState.MoveTrend == 0){}
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.RPG.beginSpeed * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.RPG.gravityScale;
        m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.RPG.animationName : HomuraIntelligence.Instance.RPG.animationName_air);
        m_IHomuraAnimationEvent.Fire += this.Fire;
    }

    public override void ExitState()
    {
        m_IplayerState.IsAttack_Rig = false;
        m_IHomuraAnimationEvent.Fire -= this.Fire;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        m_IplayerState.ActionTimeLeft --;
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
