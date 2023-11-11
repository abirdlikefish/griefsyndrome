using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Up : PlayerStateBase
{
    protected IHomuraAnimationEvent m_IHomuraAnimationEvent;
    public Homura_Attack_Up(PlayerBase playerBase) : base(playerBase)
    {
        m_IHomuraAnimationEvent = playerBase as Homura;
    }
    private bool m_isOnGround ;
// granade_front
    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 1;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.IsAttack_Up = true;
        if(m_IplayerState.MoveTrend == 0){}
        if(m_IplayerState.IsOnGround)
        {
            m_IplayerState.MaxCombo = HomuraIntelligence.Instance.mortar.maxCombo;
            m_IplayerState.ActionLevel = HomuraIntelligence.Instance.mortar.actionLevel;
            m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.mortar.beginVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.mortar.beginVelocity.y * Vector2.up;

            m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.mortar.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
            m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.mortar.animationName);
            m_isOnGround = true;
        }
        else
        {
            m_IplayerState.MaxCombo = HomuraIntelligence.Instance.granade_front.maxCombo;
            m_IplayerState.ActionLevel = HomuraIntelligence.Instance.granade_front.actionLevel;
            m_IplayerComponent.rigidbody2D.velocity = HomuraIntelligence.Instance.granade_front.beginVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.granade_front.beginVelocity.y * Vector2.up;
            m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.granade_front.gravityScaleMultiplier * HomuraIntelligence.Instance.gravityScale;
            m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.granade_front.animationName);
            m_isOnGround = false;
        }
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
    public void Fire()
    {
        if(m_isOnGround)
        {
            // m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.mortar.recoilVelocity  * (m_IplayerState.IsFaceRig ? 1 : -1) ;
            m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.mortar.recoilVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.mortar.recoilVelocity.y * Vector2.up;
            Debug.Log("Fire");
        }
        else
        {
            m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.granade_front.recoilVelocity.x * (m_IplayerState.IsFaceRig ? 1 : -1) * Vector2.right + HomuraIntelligence.Instance.granade_front.recoilVelocity.y * Vector2.up;
            // m_IplayerComponent.rigidbody2D.velocity += HomuraIntelligence.Instance.granade_front.recoilVelocity  * (m_IplayerState.IsFaceRig ? 1 : -1) ;
            Debug.Log("Fire");
        }
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
