using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Damage : PlayerStateBase
{
    public Homura_Damage(PlayerBase playerBase) : base(playerBase)
    {
        // m_animationName.Add("Homura_damage");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.ActionLevel = HomuraIntelligence.Instance.actionLevel_damage;
        m_IplayerState.IsJump = true;
        m_IplayerComponent.animator.Play(HomuraIntelligence.Instance.animationName_damage);
        m_IplayerComponent.rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
        m_IplayerComponent.rigidbody2D.velocity = Vector2.zero;
    }

    public override void ExitState()
    {
        m_IplayerState.IsDamage = false;
        base.ExitState();
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
