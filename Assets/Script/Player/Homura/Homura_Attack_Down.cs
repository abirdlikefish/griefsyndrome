using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Down : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    protected IHomuraDodge m_IHomuraDodge;
    protected IHomuraBullet m_IHomuraBullet;
    public Homura_Attack_Down(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
        m_IHomuraDodge = playerBase as Homura;
        m_IHomuraBullet = playerBase as Homura;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.MaxCombo = HomuraIntelligence.Instance.grenade.maxCombo;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.grenade.actionLevel;
        m_IplayerState.IsAttack_Up = true;
        if(m_IplayerState.MoveTrend == 0){}
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.grenade.beginVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.grenade.beginVelocity.y * Vector2.up;

        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.grenade.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
        // Debug.Log(m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.grenade.animationName : HomuraIntelligence.Instance.grenade.animationName_air);
        m_IplayerComponent.animator.Play( m_IplayerState.IsOnGround ? HomuraIntelligence.Instance.grenade.animationName : HomuraIntelligence.Instance.grenade.animationName_air);
        m_IHomuraAnimationEvent.Fire += this.Fire;
        m_IHomuraDodge.SetDodgeArea();
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
    public void Fire()
    {
        Debug.Log("Fire");
        m_IHomuraBullet.Fire_grenade();
        m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.grenade.recoilVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.grenade.recoilVelocity.y * Vector2.up;
        // m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.grenade.recoilVelocity  * (m_IplayerState.IsFaceRig ? 1 : -1) ;
    }
}
