using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IPlayerState , IPlayerComponent
{
    [SerializeField]
    protected PlayerStateMachine m_stateMachine;
    public PlayerStateMachine StateMachine { get { return m_stateMachine; } set { m_stateMachine = value; } }

// parameter
    [SerializeField]
    protected int m_combo;
    public int Combo { get { return m_combo; } set { m_combo = value; } }
    [SerializeField]
    protected int m_maxCombo;
    public int MaxCombo { get { return m_maxCombo; } set { m_maxCombo = value; } }

    [SerializeField]
    protected int m_actionLevel;
    public int ActionLevel { get { return m_actionLevel; } set { m_actionLevel = value; } }

    [SerializeField]
    protected int m_jumpTimeLeft;
    public int JumpTimeLeft { get { return m_jumpTimeLeft;} set { m_jumpTimeLeft = value; } }

    [SerializeField]
    protected int m_actionTimeLeft;
    public int ActionTimeLeft { get { return m_actionTimeLeft; } set { m_actionTimeLeft = value; } }

    [SerializeField]
    protected bool m_preInput;
    public bool PreInput { get { return m_preInput; } set { m_preInput = value; } }

    [SerializeField]
    protected bool m_isChargeOver;
    public bool IsChargeOver { get { return m_isChargeOver; } set { m_isChargeOver = value; } }

    [SerializeField]
    protected int m_moveTrend;
    public int MoveTrend 
    {
        get 
        {
            if(IsFaceRig && m_moveTrend < 0)
            {
                IsFaceRig = false;
            }
            else if(!IsFaceRig && m_moveTrend > 0)
            {
                IsFaceRig = true;
            }
            return m_moveTrend;
        } 
        set { m_moveTrend = value ;} 
    }

    [SerializeField]
    protected bool m_isFaceRig;
    public bool IsFaceRig
    {
        get { return m_isFaceRig; } 
        set
        {
            if(value == m_isFaceRig)
            {
                return;
            }
            else if(value)
            {
                m_isFaceRig = true;
                transform.localScale = new Vector3(1,1,1);
            }
            else
            {
                m_isFaceRig = false;
                transform.localScale = new Vector3(-1,1,1);
            }
        }
    }

    [SerializeField]
    protected bool m_isOnGround;
    public bool IsOnGround
    {
        get
        {
            RaycastHit2D mid ;
            float position_y = -0.4f;
            LayerMask layerMask = (1 << 8);
            for(float position_x = -0.15f ; position_x <= 0.15f ; position_x += 0.1f)
            {
                Vector2 midPosition = new Vector2(position_x + transform.position.x , position_y + transform.position.y ) ;
                mid = Physics2D.Raycast(midPosition , Vector2.down , 0.12f , layerMask);
                if(mid)
                {
                    m_isOnGround = true;
                    return true;
                }
            }
            m_isOnGround = false;
            return false;
        }
        set{}
    }

    [SerializeField]
    protected bool m_isNearWall;
    public bool IsNearWall {get { return m_isNearWall; } set { m_isNearWall = value; }}

// is in state

    [SerializeField]
    protected bool m_isIdle;
    public bool IsIdle {get { return m_isIdle;} set { m_isIdle = value; }}
    [SerializeField]
    protected bool m_isAirIdle;
    public bool IsAirIdle {get { return m_isAirIdle;} set { m_isAirIdle = value; }}
    [SerializeField]
    protected bool m_isJump;
    public bool IsJump {get { return m_isJump;} set { m_isJump = value; }}
    [SerializeField]
    protected bool m_isMove;
    public bool IsMove {get { return m_isMove;} set { m_isMove = value; }}
    [SerializeField]
    protected bool m_isAttack_Light;
    public bool IsAttack_Light {get { return m_isAttack_Light;} set { m_isAttack_Light = value; }}
    
    [SerializeField]
    protected bool m_isAttack_Heavy;
    public bool IsAttack_Heavy {get { return m_isAttack_Heavy;} set { m_isAttack_Heavy = value; }}

    [SerializeField]
    protected bool m_isAttack_Up;
    public bool IsAttack_Up {get { return m_isAttack_Up;} set { m_isAttack_Up = value; }}

    [SerializeField]
    protected bool m_isAttack_Down;
    public bool IsAttack_Down {get { return m_isAttack_Down;} set { m_isAttack_Down = value; }}

    [SerializeField]
    protected bool m_isAttack_Lef;
    public bool IsAttack_Lef {get { return m_isAttack_Lef;} set { m_isAttack_Lef = value; }}

    [SerializeField]
    protected bool m_isAttack_Rig;
    public bool IsAttack_Rig {get { return m_isAttack_Rig;} set { m_isAttack_Rig = value; }}

    [SerializeField]
    protected bool m_isAttack_Ultimate;
    public bool IsAttack_Ultimate {get { return m_isAttack_Ultimate;} set { m_isAttack_Ultimate = value; }}

    [SerializeField]
    protected bool m_isDamage;
    public bool IsDamage {get { return m_isDamage;} set { m_isDamage = value; }}

// state
    public PlayerStateBase State_Jump { get ; set ; }
    public PlayerStateBase State_Move { get ; set ; }
    public PlayerStateBase State_Attack_Light { get ; set ; }
    public PlayerStateBase State_Attack_Heavy { get ; set ; }
    public PlayerStateBase State_Attack_Up { get ; set ; }
    public PlayerStateBase State_Attack_Down { get ; set ; }
    public PlayerStateBase State_Attack_Lef { get ; set ; }
    public PlayerStateBase State_Attack_Rig { get ; set ; }
    public PlayerStateBase State_Attack_Ultimate { get ; set ; }
    public PlayerStateBase State_Damage { get ; set ; }
    public PlayerStateBase State_Idle { get ; set ; }
    public PlayerStateBase State_AirIdle { get ; set ; }

// component
    public Rigidbody2D rigidbody2D { get ; set ;  }
    public Collider2D collider2D { get ; set ; }
    public SpriteRenderer spriteRenderer { get ; set ; }
    public Animator animator { get ; set ; }

// initialize
    private void Start() 
    {
        Initialization();
    }
    public virtual void Initialization()
    {
        InitializeEvent();
        InitializeComponent();
        InitializeParameter();
        InitializeState();
        InitializeStateMachine();
    }
    protected virtual void InitializeParameter()
    {
        m_combo = 0;
        m_maxCombo = 0;
        m_actionLevel = 0;
        m_moveTrend = 0;
        m_jumpTimeLeft = 0;
        m_actionTimeLeft = 1;
        m_preInput = false;
        m_isChargeOver = false;
        m_isFaceRig = true;
        m_isNearWall = false;
        m_isJump = false;
        m_isMove = false;
        m_isAttack_Light = false;
        m_isAttack_Heavy = false;
        m_isAttack_Up = false;
        m_isAttack_Down = false;
        m_isAttack_Lef = false;
        m_isAttack_Rig = false;
        m_isAttack_Ultimate = false;
        m_isDamage = false;
    }
    protected virtual void InitializeState()
    {
    }

    protected virtual void InitializeStateMachine()
    {
    }

    protected virtual void InitializeEvent()
    {
        EventManager.Instance.PauseEvent += PauseEvent;
        EventManager.Instance.ResumeEvent += ResumeEvent;
        EventManager.Instance.MoveEvent += MoveEvent;
        EventManager.Instance.Move_CanceledEvent += Move_CanceledEvent;
        EventManager.Instance.JumpEvent += JumpEvent;
        EventManager.Instance.Jump_CanceledEvent += Jump_CanceledEvent;
        EventManager.Instance.Attack_LightEvent += Attack_LightEvent;
        EventManager.Instance.Attack_Light_CanceledEvent += Attack_Light_CanceledEvent;
        EventManager.Instance.Attack_HeavyEvent += Attack_HeavyEvent;
        EventManager.Instance.Attack_Heavy_CanceledEvent += Attack_Heavy_CanceledEvent;
        EventManager.Instance.Attack_UpEvent += Attack_UpEvent;
        EventManager.Instance.Attack_Up_CanceledEvent += Attack_Up_CanceledEvent;
        EventManager.Instance.Attack_DownEvent += Attack_DownEvent;
        EventManager.Instance.Attack_Down_CanceledEvent += Attack_Down_CanceledEvent;
        EventManager.Instance.Attack_LefEvent += Attack_LefEvent;
        EventManager.Instance.Attack_Lef_CanceledEvent += Attack_Lef_CanceledEvent;
        EventManager.Instance.Attack_RigEvent += Attack_RigEvent;
        EventManager.Instance.Attack_Rig_CanceledEvent += Attack_Rig_CanceledEvent;
        EventManager.Instance.Attack_UltimateEvent += Attack_UltimateEvent;
        EventManager.Instance.Attack_Ultimate_CanceledEvent += Attack_Ultimate_CanceledEvent;
    }

    protected virtual void InitializeComponent()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

//
    protected virtual void Update() 
    {
        StateMachine.Update();
    }

    protected virtual void FixedUpdate() 
    {
        StateMachine.FixedUpdate();
    }
// input event
    public virtual void PauseEvent()
    {
        Debug.Log("PauseEvent");
    }
    public virtual void ResumeEvent()
    {
        Debug.Log("ResumeEvent");
    }
    
    public virtual void MoveEvent(Vector2 input)
    {
        Debug.Log("MoveEvent");
    }
    public virtual void Move_CanceledEvent(Vector2 input)
    {
        Debug.Log("Move_CanceledEvent");
    }
    public virtual void JumpEvent()
    {
        Debug.Log("JumpEvent");
    }
    public virtual void Jump_CanceledEvent()
    {
        Debug.Log("Jump_CanceledEvent");
    }
    public virtual void Attack_LightEvent()
    {
        Debug.Log("Attack_LightEvent");
    }
    public virtual void Attack_Light_CanceledEvent()
    {
        Debug.Log("Attack_Light_CanceledEvent");
    }
    public virtual void Attack_HeavyEvent()
    {
        Debug.Log("Attack_HeavyEvent");
    }
    public virtual void Attack_Heavy_CanceledEvent()
    {
        Debug.Log("Attack_Heavy_CanceledEvent");
    }
    public virtual void Attack_UpEvent()
    {
        Debug.Log("Attack_UpEvent");
    }
    public virtual void Attack_Up_CanceledEvent()
    {
        Debug.Log("Attack_Up_CanceledEvent");
    }
    public virtual void Attack_DownEvent()
    {
        Debug.Log("Attack_DownEvent");
    }
    public virtual void Attack_Down_CanceledEvent()
    {
        Debug.Log("Attack_Down_CanceledEvent");
    }
    public virtual void Attack_LefEvent()
    {
        Debug.Log("Attack_LefEvent");
    }
    public virtual void Attack_Lef_CanceledEvent()
    {
        Debug.Log("Attack_Lef_CanceledEvent");
    }
    public virtual void Attack_RigEvent()
    {
        Debug.Log("Attack_RigEvent");
    }
    public virtual void Attack_Rig_CanceledEvent()
    {
        Debug.Log("Attack_Rig_CanceledEvent");
    }
    public virtual void Attack_UltimateEvent()
    {
        Debug.Log("Attack_UltimateEvent");
    }
    public virtual void Attack_Ultimate_CanceledEvent()
    {
        Debug.Log("Attack_Ultimate_CanceledEvent");
    }

}
