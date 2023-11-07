using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Homura : PlayerBase
{

// initialization
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
    //
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

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
        Debug.Log("MoveEvent");
    }
    public override void Move_CanceledEvent(Vector2 input)
    {
        if(IsMove == false)
        {
            return ;
        }
        IsChargeOver = true;
        Debug.Log("Move_CanceledEvent");
    }
    public override void JumpEvent()
    {
        Debug.Log("JumpEvent");
    }
    public override void Jump_CanceledEvent()
    {
        Debug.Log("Jump_CanceledEvent");
    }
    public override void Attack_LightEvent()
    {
        Debug.Log("Attack_LightEvent");
    }
    public override void Attack_Light_CanceledEvent()
    {
        Debug.Log("Attack_Light_CanceledEvent");
    }
    public override void Attack_HeavyEvent()
    {
        Debug.Log("Attack_HeavyEvent");
    }
    public override void Attack_Heavy_CanceledEvent()
    {
        Debug.Log("Attack_Heavy_CanceledEvent");
    }
    public override void Attack_UpEvent()
    {
        Debug.Log("Attack_UpEvent");
    }
    public override void Attack_Up_CanceledEvent()
    {
        Debug.Log("Attack_Up_CanceledEvent");
    }
    public override void Attack_DownEvent()
    {
        Debug.Log("Attack_DownEvent");
    }
    public override void Attack_Down_CanceledEvent()
    {
        Debug.Log("Attack_Down_CanceledEvent");
    }
    public override void Attack_LefEvent()
    {
        Debug.Log("Attack_LefEvent");
    }
    public override void Attack_Lef_CanceledEvent()
    {
        Debug.Log("Attack_Lef_CanceledEvent");
    }
    public override void Attack_RigEvent()
    {
        Debug.Log("Attack_RigEvent");
    }
    public override void Attack_Rig_CanceledEvent()
    {
        Debug.Log("Attack_Rig_CanceledEvent");
    }
    public override void Attack_UltimateEvent()
    {
        Debug.Log("Attack_UltimateEvent");
    }
    public override void Attack_Ultimate_CanceledEvent()
    {
        Debug.Log("Attack_Ultimate_CanceledEvent");
    }
}
