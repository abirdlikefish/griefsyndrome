using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
// Input Events
    // GamePlayAction
    public Action JumpEvent;
    public Action<Vector2> MoveEvent;
    public Action PauseEvent;
    public Action Attack_LightEvent;
    public Action Attack_HeavyEvent;
    public Action Attack_UpEvent;
    public Action Attack_DownEvent;
    public Action Attack_FrontEvent;
    public Action Attack_UltimateEvent;
    // UIAction
    public Action ResumeEvent;



// instance
    private static EventManager instance ;
    public static EventManager Instance { get {return instance;}  private set{instance = value;} }
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("found another EventManager instance");
            Destroy(gameObject);
        }
    }
}
