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
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.RPG.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        // if(m_IplayerState.IsOnGround)
        // {
        //     m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.RPG.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        // }
        // else
        // {
        //     m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.RPG.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        // }
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.RPG.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
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
        m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.RPG.recoilVelocity  * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        Debug.Log("Fire");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(m_IplayerComponent.rigidbody2D.velocity.x != 0)
        {
            if(m_IplayerState.IsOnGround)
            {
                float dragSpeed = Mathf.Min( Mathf.Abs(m_IplayerComponent.rigidbody2D.velocity.x) , HomuraIntelligence.Instance.dragSpeed);
                m_IplayerComponent.rigidbody2D.velocity += dragSpeed * Mathf.Sign(m_IplayerComponent.rigidbody2D.velocity.x) * Vector2.left ;
            }
            else   
            {
                float dragSpeed = Mathf.Min( Mathf.Abs(m_IplayerComponent.rigidbody2D.velocity.x) , HomuraIntelligence.Instance.dragSpeed_air);
                m_IplayerComponent.rigidbody2D.velocity += dragSpeed * Mathf.Sign(m_IplayerComponent.rigidbody2D.velocity.x) * Vector2.left ;
            }
        }
    }
}
