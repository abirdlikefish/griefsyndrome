using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Homura : PlayerBase , IHomuraAnimationEvent , IHomuraDodge , IHomuraBullet
{

// health
    public override void BeHit(float damage)
    {
        if(IsInvincible)
        {
            return;
        }
        base.BeHit(damage);
        StateMachine.ChangeState(State_Damage);
    }

// dodge
    [SerializeField]
    private bool isDodgeSucceeded ;
    public bool IsDodgeSucceeded { get { return isDodgeSucceeded; } set { isDodgeSucceeded = value; } }  

    [SerializeField]
    public GameObject dodgeArea;
    public GameObject DodgeArea { get { return dodgeArea; } set { dodgeArea = value; } }
    private GameObject m_dodgeArea;

    public void SetDodgeArea()
    {
        IsDodgeSucceeded = false;
        IsInvincible = true;
        m_dodgeArea = ObjectPool.Instance.getObject(dodgeArea);
        if(m_dodgeArea == null)
        {
            Debug.LogWarning("m_dodgeArea is null");
        }
        m_dodgeArea.transform.position = transform.position;
        m_dodgeArea.transform.localScale = transform.localScale ;
        Homura_DodgeArea homura_DodgeArea = m_dodgeArea.GetComponent<Homura_DodgeArea>();
        if(homura_DodgeArea == null)
        {
            Debug.LogWarning("homura_DodgeArea is null");
        }
        homura_DodgeArea.Initialization(this);
        StartCoroutine("DodgeDetermine");
    }
    private IEnumerator DodgeDetermine()
    {
        float beginTime = Time.time;
        while(Time.time - beginTime < HomuraIntelligence.Instance.invincibleTime)
        {

            yield return null;
        }
        ObjectPool.Instance.returnObject(m_dodgeArea);
        IsInvincible = false;
    }
// bullet
    public GameObject m_RPG;
    public GameObject m_minimi;
    public GameObject m_minimiB;
    public GameObject m_mortar;
    public GameObject m_cartridge;
    public GameObject m_bullet_handgun;
    public GameObject m_bullet_minimi;
    public GameObject m_bullet_RPG;
    public GameObject m_bullet_grenade;
    public void CreateCartridge_handgun()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_cartridge);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Disappear>().Initialization(HomuraIntelligence.Instance.cartridgeWaitTime , HomuraIntelligence.Instance.disappearTime);
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.discardPosition_cartridge_handgun.x * transform.localScale.x , HomuraIntelligence.Instance.discardPosition_cartridge_handgun.y) + rigidbody2D.position;
        midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.discardVelocity_cartridge_handgun.x * transform.localScale.x , HomuraIntelligence.Instance.discardVelocity_cartridge_handgun.y);
    }

    public void CreateCartridge_minimi()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_cartridge);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Disappear>().Initialization(HomuraIntelligence.Instance.cartridgeWaitTime , HomuraIntelligence.Instance.disappearTime);
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.discardPosition_cartridge_minimi.x * transform.localScale.x , HomuraIntelligence.Instance.discardPosition_cartridge_minimi.y) + rigidbody2D.position;
        midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.discardVelocity_cartridge_minimi.x * transform.localScale.x , HomuraIntelligence.Instance.discardVelocity_cartridge_minimi.y);
    }

    public void Discard_RPG()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_RPG);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Disappear>().Initialization(0 , HomuraIntelligence.Instance.disappearTime);
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.discardPosition_RPG.x * transform.localScale.x , HomuraIntelligence.Instance.discardPosition_RPG.y) + rigidbody2D.position;
        midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.discardVelocity_RPG.x * transform.localScale.x , HomuraIntelligence.Instance.discardVelocity_RPG.y);
    }

    public void Discard_minimi()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_minimi);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Disappear>().Initialization(0 , HomuraIntelligence.Instance.disappearTime);
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.discardPosition_minimi.x * transform.localScale.x , HomuraIntelligence.Instance.discardPosition_minimi.y) + rigidbody2D.position;
        midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.discardVelocity_minimi.x * transform.localScale.x , HomuraIntelligence.Instance.discardVelocity_minimi.y);
    }

    public void Discard_minimiB()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_minimiB);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Disappear>().Initialization(0 , HomuraIntelligence.Instance.disappearTime);
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.discardPosition_minimi.x * transform.localScale.x , HomuraIntelligence.Instance.discardPosition_minimi.y) + rigidbody2D.position;
        midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.discardVelocity_minimi.x * transform.localScale.x , HomuraIntelligence.Instance.discardVelocity_minimi.y);
    }

    public void Fire_RPG()
    {
        // GameObject midGameObject = ObjectPool.Instance.getObject(m_bullet_RPG);
        // Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        // midGameObject.GetComponent<Bullet_RPG>().Initialization();
        // midGameObject.transform.localScale = transform.localScale;
        // midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.RPG.position.x * transform.localScale.x + transform.position.x , HomuraIntelligence.Instance.RPG.position.y + transform.position.y);
        // midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.RPG.velocity.x * transform.localScale.x , HomuraIntelligence.Instance.RPG.velocity.y);
    }

    public void Fire_minimi()
    {
        // GameObject midGameObject = ObjectPool.Instance.getObject(m_bullet_minimi);
        // Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        // midGameObject.GetComponent<Bullet_Minimi>().Initialization();
        // midGameObject.transform.localScale = transform.localScale;
        // midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.minimi.position.x * transform.localScale.x + transform.position.x , HomuraIntelligence.Instance.minimi.position.y + transform.position.y);
        // midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.minimi.velocity.x * transform.localScale.x , HomuraIntelligence.Instance.minimi.velocity.y);
    }

    public void Fire_handgun()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_bullet_handgun);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Bullet_Handgun>().Initialization();
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.handgun.position.x * transform.localScale.x + transform.position.x , HomuraIntelligence.Instance.handgun.position.y + transform.position.y);
        midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.handgun.velocity.x * transform.localScale.x , HomuraIntelligence.Instance.handgun.velocity.y);
    }
    public void Fire_grenade()
    {
        // GameObject midGameObject = ObjectPool.Instance.getObject(m_bullet_grenade);
        // Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        // midGameObject.GetComponent<Bullet_Grenade>().Initialization();
        // midGameObject.transform.localScale = transform.localScale;
        // midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.grenade.position.x * transform.localScale.x + transform.position.x , HomuraIntelligence.Instance.grenade.position.y + transform.position.y);
        // midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.grenade.velocity.x * transform.localScale.x , HomuraIntelligence.Instance.grenade.velocity.y);
    }
    public void Fire_grenade_front()
    {
        // GameObject midGameObject = ObjectPool.Instance.getObject(m_bullet_grenade);
        // Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        // midGameObject.GetComponent<Bullet_Grenade>().Initialization();
        // midGameObject.transform.localScale = transform.localScale;
        // midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.grenade_front.position.x * transform.localScale.x + transform.position.x , HomuraIntelligence.Instance.grenade_front.position.y + transform.position.y);
        // midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.grenade_front.velocity.x * transform.localScale.x , HomuraIntelligence.Instance.grenade_front.velocity.y);
    }


    public void Place_mortar()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_mortar);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Homura_Mortar>().Initialization(HomuraIntelligence.Instance.mortarWaitTime);
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.mortar.position.x * transform.localScale.x , HomuraIntelligence.Instance.mortar.position.y) + rigidbody2D.position;
    }
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
        State_Climb = new Homura_Climb(this);
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

    protected override void InitializeParameter()
    {
        base.InitializeParameter();

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
