using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Light : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    protected IHomuraBullet m_IHomuraBullet;
    public Homura_Attack_Light(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
        m_IHomuraBullet = playerBase as Homura;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = HomuraIntelligence.Instance.handgun.maxCombo;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.handgun.actionLevel;
        m_IplayerState.IsAttack_Light = true;
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.handgun.beginVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.handgun.beginVelocity.y * Vector2.up;
        // m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.handgun.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.handgun.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
        m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.handgun.animationName : HomuraIntelligence.Instance.handgun.animationName_air);
        m_IHomuraAnimationEvent.Fire += this.Fire;
        m_IHomuraAnimationEvent.ReShoot += this.ReShoot;
    }

    public override void ExitState()
    {
        m_IplayerState.IsAttack_Light = false;
        m_IplayerState.ActionTimeLeft --;
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        m_IHomuraAnimationEvent.Fire -= this.Fire;
        m_IHomuraAnimationEvent.ReShoot -= this.ReShoot;
        base.ExitState();
    }

    public void ReShoot()
    {
        if(m_IplayerState.PreInput && m_IplayerState.Combo < m_IplayerState.MaxCombo)
        {
            m_IplayerState.PreInput = false;
            m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.handgun.animationName : HomuraIntelligence.Instance.handgun.animationName_air, 0 , 3.0f / 8.0f);
            m_IplayerState.Combo ++;
        }
    }

    public void Fire()
    {
        Debug.Log("Fire");
        m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.handgun.recoilVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.handgun.recoilVelocity.y * Vector2.up;
        // m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.handgun.recoilVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        m_IHomuraBullet.CreateCartridge_handgun();
        m_IHomuraBullet.Fire_handgun();
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
