using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Heavy : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    protected IHomuraBullet m_IHomuraBullet;
    private int shootMode ;
    private int[] m_frameCnt = {11 , 12};
    public Homura_Attack_Heavy(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
        m_IHomuraBullet = playerBase as Homura;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = HomuraIntelligence.Instance.minimi.maxCombo;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.minimi.actionLevel;
        m_IplayerState.IsAttack_Heavy = true;
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.minimi.beginVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.minimi.beginVelocity.y * Vector2.up;
        // m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.minimi.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
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
        if(shootMode == 0)
        {
            m_IHomuraBullet.Discard_minimi();
        }
        else
        {
            m_IHomuraBullet.Discard_minimiB();
        }
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Fire()
    {
        Debug.Log("Fire");
        m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.minimi.recoilVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.minimi.recoilVelocity.y * Vector2.up;
        m_IHomuraBullet.CreateCartridge_minimi();
        m_IHomuraBullet.Fire_minimi();
        // m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.minimi.recoilVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
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
        // float diffSpeed = - m_IplayerComponent.rigidbody2D.velocity.x;
        // m_IplayerComponent.rigidbody2D.velocity += diffSpeed * HomuraIntelligence.Instance.dragSpeedMultiplier_air * Vector2.right;
            }
        }
    }
}
