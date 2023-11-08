using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[CreateAssetMenu(fileName = "HomuraIntelligence", menuName = "ScriptableObjects/HomuraIntelligence", order = 0)]
public class HomuraIntelligence : ScriptableObject
{
    public static HomuraIntelligence Instance { get; private set; }
    private void OnEnable() 
    {
        if(Instance == null)
        {
            Instance = this;
            Debug.LogWarning("HomuraIntelligence Instance");
        }
    }

    public float gravityScale = 1;
    public float jumpHeight = 4;
    public Vector2 moveSpeed = new Vector2(2.0f, 0);
    public Vector2 jumpSpeed { get{return new Vector2(0 , math.sqrt(2 * gravityScale * Physics2D.gravity.y * -1 * jumpHeight)) ;}}
    public int maxJumpTime = 2;
    public int maxActionTime = 1;
    public Vector2 recoil_GRP = new Vector2(-1 , 0);
    public Vector2 recoil_handgun = new Vector2(0 , 0);
    public Vector2 recoil_minimi = new Vector2(-0.1f , 0);
    public Vector2 recoil_mortar = new Vector2(0,0);
    public Vector2 recoil_granade = new Vector2(-1,1);
    public Vector2 recoil_granade_front = new Vector2(1,1);
    public float gravityScale_GPR = 0;
    public float gravityScale_handgun = 0;
    public float gravityScale_minimi = 0.1f;
    public float gravityScale_mortar = 1;
    public float gravityScale_granade = 1;
    public float gravityScale_granade_front = 1;
}
