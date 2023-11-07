using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Attack_Up : PlayerStateBase
{
    public Homura_Attack_Up(PlayerBase playerBase) : base(playerBase)
    {
        m_animationName.Add("Homura_mortar");
        m_animationName.Add("Homura_granadeB");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 0;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.ActionLevel = 0;
        m_IplayerComponent.animator.Play(m_animationName[0]);
    }

    public override void ExitState()
    {
        m_IplayerState.Combo = 0;
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
