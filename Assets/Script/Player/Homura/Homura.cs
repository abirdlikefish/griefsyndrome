using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Homura : PlayerBase , IHomuraAnimationEvent
{

// initialization

    public override void Initialization()
    {
        base.Initialization();
        rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale;
    }
    protected override void InitializeState()
    {
        State_Jump = new Homura_Jump(this);
        State_Idle = new Homura_Idle(this);
        State_Move = new Homura_Move(this);
        State_Damage = new Homura_Damage(this);
        State_AirIdle = new Homura_AirIdle(this);
        State_Attack_Up = new Homura_Attack_Up(this);
        State_Attack_Down = new Homura_Attack_Down(this);
        State_Attack_Lef = new Homura_Attack_Lef(this);
        State_Attack_Rig = new Homura_Attack_Rig(this);
        State_Attack_Light = new Homura_Attack_Light(this);
        State_Attack_Heavy = new Homura_Attack_Heavy(this);
        State_Attack_Ultimate = new Homura_Attack_Ultimate(this);
    }

    protected override void InitializeStateMachine()
    {
        StateMachine = new PlayerStateMachine(this);
        StateMachine.Initialization(State_Idle);
    }
// update
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

// animation event
    public void AnimationEvent_End()
    {
        StateMachine.ChangeState( IsOnGround ? State_Idle : State_AirIdle);
    }
    public void AnimationEvent_Fire()
    {
        Fire?.Invoke();
    }
    public void AnimationEvent_AfterFire()
    {
        ReShoot?.Invoke();
    }
    public Action Fire{get ; set ; }
    public Action ReShoot{get ; set ; }

// input event
    public override void PauseEvent()
    {
        Debug.Log("PauseEvent");
    }
    public override void ResumeEvent()
    {
        Debug.Log("ResumeEvent");
    }
    
    public override void MoveEvent(Vector2 input)
    {
        MoveTrend = input.x > 0 ? 1 : -1;
        if(ActionLevel >= 1 && IsMove == false)
        {
            return ;
        }

        if(input.sqrMagnitude < 0.1f)
        {
            Debug.LogError("MoveEvent Input is " + input.ToString());
        }
        if(input.x < 0)
        {
            IsFaceRig = false;
        }
        else
        {
            IsFaceRig = true;
        }

        StateMachine.ChangeState(State_Move);
    }
    public override void Move_CanceledEvent(Vector2 input)
    {
        MoveTrend = 0;
        if(IsMove == false)
        {
            return ;
        }
        IsChargeOver = true;
    }
    public override void JumpEvent()
    {
        if(ActionLevel >= 5 )
        {
            return ;
        }
        if(JumpTimeLeft <= 0)
        {
            return;
        }
        StateMachine.ChangeState(State_Jump);
    }
    public override void Jump_CanceledEvent()
    {
        if(IsJump == false)
        {
            return ;
        }
        IsChargeOver = true;
    }
    public override void Attack_LightEvent()
    {
        if(ActionTimeLeft <= 0)
        {
            return;
        }
        if(IsAttack_Light && Combo < MaxCombo)
        {
            PreInput = true;
            return;
        }
        if(ActionLevel >= 2)
        {
            return ;
        }
        StateMachine.ChangeState(State_Attack_Light);
    }
    public override void Attack_Light_CanceledEvent()
    {
        if(IsAttack_Light == false)
        {
            return;
        }
        IsChargeOver = true;
    }
    public override void Attack_HeavyEvent()
    {
        if(ActionTimeLeft <= 0)
        {
            return;
        }
        if(ActionLevel >= 3)
        {
            return ;
        }
        StateMachine.ChangeState(State_Attack_Heavy);
    }
    public override void Attack_Heavy_CanceledEvent()
    {
        if(IsAttack_Heavy == false)
        {
            return;
        }
        IsChargeOver = true;
    }
    public override void Attack_LefEvent()
    {
        if(ActionTimeLeft <= 0)
        {
            return;
        }
        if(ActionLevel >= 4)
        {
            return ;
        }
        StateMachine.ChangeState(State_Attack_Lef);
    }
    public override void Attack_Lef_CanceledEvent()
    {
        if(IsAttack_Lef == false)
        {
            return ;
        }
        IsChargeOver = true;
    }
    public override void Attack_RigEvent()
    {
        if(ActionTimeLeft <= 0)
        {
            return;
        }
        if(ActionLevel >= 4)
        {
            return ;
        }
        StateMachine.ChangeState(State_Attack_Rig);
    }
    public override void Attack_Rig_CanceledEvent()
    {
        if(IsAttack_Rig == false)
        {
            return ;
        }
        IsChargeOver = true;
    }
    public override void Attack_UpEvent()
    {
        if(ActionTimeLeft <= 0)
        {
            return;
        }
        if(ActionLevel >= 4)
        {
            return ;
        }
        StateMachine.ChangeState(State_Attack_Up);
    }
    public override void Attack_Up_CanceledEvent()
    {
        if(IsAttack_Up == false)
        {
            return ;
        }
        IsChargeOver = true;
    }
    public override void Attack_DownEvent()
    {
        if(ActionTimeLeft <= 0)
        {
            return;
        }
        if(ActionLevel >= 4)
        {
            return ;
        }
        StateMachine.ChangeState(State_Attack_Down);
    }
    public override void Attack_Down_CanceledEvent()
    {
        if(IsAttack_Down == false)
        {
            return ;
        }
        IsChargeOver = true;
    }
    public override void Attack_UltimateEvent()
    {
        if(ActionTimeLeft <= 0)
        {
            return;
        }
        if(ActionLevel >= 5)
        {
            return ;
        }
        StateMachine.ChangeState(State_Attack_Ultimate);
    }
    public override void Attack_Ultimate_CanceledEvent()
    {
        if(IsAttack_Ultimate == false)
        {
            return ;
        }
        IsChargeOver = true;
    }
}
