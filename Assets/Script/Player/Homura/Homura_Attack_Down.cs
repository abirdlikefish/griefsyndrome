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
        // m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.granade.beginVelocity * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.granade.beginVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.granade.beginVelocity.y * Vector2.up;

        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.granade.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
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
        m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.granade.recoilVelocity  * (m_IplayerState.IsFaceRig ? 1 : -1) ;
        base.FixedUpdate();
    }
    public void Fire()
    {
        Debug.Log("Fire");
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
