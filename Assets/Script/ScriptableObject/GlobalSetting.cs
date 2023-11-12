using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GlobalSetting", menuName = "ScriptableObjects/GlobalSetting", order = 2)]
public class GlobalSetting : ScriptableObject
{
    
    public static GlobalSetting Instance { get; private set; }
    private void OnEnable() 
    {
        Debug.Log("GlobalSetting OnEnable");
        if(Instance == null)
        {
            Instance = this;
            Debug.Log("GlobalSetting Instance");
        }
    }
    public float coyoteTime = 0f;
    public float difficulty = 0f;
}
