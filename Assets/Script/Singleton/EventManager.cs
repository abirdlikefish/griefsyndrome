using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
// Input Events
    // GamePlayAction
    public Action PauseEvent;
    public Action<Vector2> MoveEvent;
    public Action<Vector2> Move_CanceledEvent;
    public Action JumpEvent;
    public Action Jump_CanceledEvent;
    public Action Attack_LightEvent;
    public Action Attack_Light_CanceledEvent;
    public Action Attack_HeavyEvent;
    public Action Attack_Heavy_CanceledEvent;
    public Action Attack_UpEvent;
    public Action Attack_Up_CanceledEvent;
    public Action Attack_DownEvent;
    public Action Attack_Down_CanceledEvent;
    public Action Attack_LefEvent;
    public Action Attack_Lef_CanceledEvent;
    public Action Attack_RigEvent;
    public Action Attack_Rig_CanceledEvent;
    public Action Attack_UltimateEvent;
    public Action Attack_Ultimate_CanceledEvent;
    // UIAction
    public Action ResumeEvent;



// instance
    private static EventManager instance ;
    public static EventManager Instance { get {return instance;}  private set{instance = value;} }
    // public InputReader inputReader;
    private void Awake() 
    {
        if(Instance == null)
        {
            // inputReader.Initialization();
            Instance = this;
        }
        else
        {
            Debug.LogWarning("found another EventManager instance");
            Destroy(gameObject);
        }
    }
}
