using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Heavy : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    private int shootMode ;
    private int[] m_frameCnt = {11 , 12};
    public Homura_Attack_Heavy(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = HomuraIntelligence.Instance.minimi.maxCombo;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.minimi.actionLevel;
        m_IplayerState.IsAttack_Heavy = true;
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.minimi.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        // if(m_IplayerState.IsOnGround)
        // {
        //     m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.minimi.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        // }
        // else
        // {
        //     m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.minimi.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        // }
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.minimi.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
        m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.minimi.animationName : HomuraIntelligence.Instance.minimi.animationName_air);
        shootMode = m_IplayerState.IsOnGround ? 0 : 1;
        m_IHomuraAnimationEvent.Fire += this.Fire;
        m_IHomuraAnimationEvent.ReShoot += this.ReShoot;
    }
    public override void ExitState()
    {
        m_IplayerState.IsAttack_Heavy = false;
        m_IplayerState.ActionTimeLeft --;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        m_IHomuraAnimationEvent.Fire -= this.Fire;
        m_IHomuraAnimationEvent.ReShoot -= this.ReShoot;
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Fire()
    {
        Debug.Log("Fire");
        m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.minimi.recoilVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
    }
    public void ReShoot()
    {
        if(m_IplayerState.Combo < m_IplayerState.MaxCombo && !m_IplayerState.IsChargeOver)
        {
            m_IplayerComponent.animator.Play( shootMode == 0 ? HomuraIntelligence.Instance.minimi.animationName : HomuraIntelligence.Instance.minimi.animationName_air, 0 , 6.0f / m_frameCnt[shootMode]);
            m_IplayerState.Combo ++;
        }
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
