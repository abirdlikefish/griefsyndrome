using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homura_Idle : PlayerStateBase
{
    public Homura_Idle(PlayerBase playerBase) : base(playerBase)
    {
        m_animationName.Add("Homura_stand");
    }

    public override void EnterState()
    {
        base.EnterState();
        m_IplayerState.Combo = 0;
        m_IplayerState.IsChargeOver = false;
        m_IplayerState.IsIdle = true;
        m_IplayerState.ActionLevel = 0;
        m_IplayerComponent.animator.Play(m_animationName[0]);
    }

    public override void ExitState()
    {
        m_IplayerState.Combo = 0;
        m_IplayerState.IsIdle = false;
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
