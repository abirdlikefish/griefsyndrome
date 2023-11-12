using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Ultimate : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    protected IHomuraBullet m_IHomuraBullet;
    public Homura_Attack_Ultimate(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
        m_IHomuraBullet = playerBase as Homura;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = HomuraIntelligence.Instance.timeFreeze.maxCombo;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.timeFreeze.actionLevel;
        m_IplayerState.IsAttack_Ultimate = true;
        if(m_IplayerState.MoveTrend == 0){}
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.timeFreeze.beginVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.timeFreeze.beginVelocity.y * Vector2.up;
        // m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.timeFreeze.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.timeFreeze.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
        m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.timeFreeze.animationName : HomuraIntelligence.Instance.timeFreeze.animationName_air);
        m_IHomuraAnimationEvent.Fire += this.Fire;
    }

    public override void ExitState()
    {
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        m_IplayerState.IsAttack_Ultimate = false;
        m_IplayerState.ActionTimeLeft --;
        m_IHomuraAnimationEvent.Fire -= this.Fire;
        base.ExitState();
    }
    public void Fire()
    {
        m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.timeFreeze.recoilVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.timeFreeze.recoilVelocity.y * Vector2.up;
        // m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.timeFreeze.recoilVelocity  * (m_IplayerState.IsFaceRig ? 1 : -1) ;
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
                // float dragSpeed = Mathf.Min( Mathf.Abs(m_IplayerComponent.rigidbody2D.velocity.x) , HomuraIntelligence.Instance.dragSpeed_air);
                // m_IplayerComponent.rigidbody2D.velocity += dragSpeed * Mathf.Sign(m_IplayerComponent.rigidbody2D.velocity.x) * Vector2.left ;
                
        float diffSpeed = - m_IplayerComponent.rigidbody2D.velocity.x;
        m_IplayerComponent.rigidbody2D.velocity += diffSpeed * HomuraIntelligence.Instance.dragSpeedMultiplier_air * Vector2.right;
            }
        }
    }
}
