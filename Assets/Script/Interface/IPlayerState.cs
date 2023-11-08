using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
// statemachine
    public PlayerStateMachine StateMachine{ get; set; }

// parameter

    // level:
    /*
    0 idle
    1 walk airIdle
    2 attack_light
    3 attack_heavy
    4 attack_updownlefrig
    5 attack_untimate jump
    6 damage
    */
    public int ActionLevel { get; set; }
    public int Combo { get; set; }
    public int MaxCombo { get; set; }
    public int MoveTrend { get; set; }
    public int JumpTimeLeft { get; set; }
    public int ActionTimeLeft { get; set; }
    public bool PreInput { get; set; }
    public bool IsChargeOver { get; set; }
    public bool IsOnGround { get; set; }
    public bool IsNearWall { get; set; }
    public bool IsFaceRig { get; set; }

// is in state
    public bool IsIdle { get; set; }
    public bool IsAirIdle { get; set; }
    public bool IsJump { get; set; }
    public bool IsMove { get; set; }
    public bool IsAttack_Light { get; set; }
    public bool IsAttack_Heavy { get; set; }
    public bool IsAttack_Up { get; set; }
    public bool IsAttack_Down { get; set; }
    public bool IsAttack_Lef { get; set; }
    public bool IsAttack_Rig { get; set; }
    public bool IsAttack_Ultimate { get; set; }
    public bool IsDamage { get; set; }

// state
    public PlayerStateBase State_Idle { get; set; }
    public PlayerStateBase State_AirIdle { get; set; }
    public PlayerStateBase State_Jump { get; set; }
    public PlayerStateBase State_Move { get; set; }
    public PlayerStateBase State_Attack_Light { get; set; }
    public PlayerStateBase State_Attack_Heavy { get; set; }
    public PlayerStateBase State_Attack_Up { get; set; }
    public PlayerStateBase State_Attack_Down { get; set; }
    public PlayerStateBase State_Attack_Lef { get; set; }
    public PlayerStateBase State_Attack_Rig { get; set; }
    public PlayerStateBase State_Attack_Ultimate { get; set; }
    public PlayerStateBase State_Damage { get; set; }

}
