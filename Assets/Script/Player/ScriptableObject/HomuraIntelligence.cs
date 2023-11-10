using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using System;

[CreateAssetMenu(fileName = "HomuraIntelligence", menuName = "ScriptableObjects/HomuraIntelligence", order = 0)]
public class HomuraIntelligence : ScriptableObject
{
    public static HomuraIntelligence Instance { get; private set; }
    private void OnEnable() 
    {
        Debug.Log("HomuraIntelligence OnEnable");
        if(Instance == null)
        {
            Instance = this;
            Debug.Log("HomuraIntelligence Instance");
        }
    }

    public float gravityScale = 1;
    public float jumpHeight = 4;
    public Vector2 moveSpeed = new Vector2(2.0f, 0);
    public Vector2 jumpSpeed { get{return new Vector2(0 , math.sqrt(2 * gravityScale * Physics2D.gravity.y * -1 * jumpHeight)) ;}}
    public int maxJumpTime = 2;
    public int maxActionTime = 1;
    public string animationName_move = "Homura_run";
    public string animationName_idle = "Homura_stand";
    public string animationName_airIdle = "Homura_airIdle";
    public string animationName_jump = "Homura_jump";
    public string animationName_airJump = "Homura_airJump";
    public int actionLevel_move = 1;
    public int actionLevel_idle = 0;
    public int actionLevel_airIdle = 1;
    public int actionLevel_jump = 5;


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
    [Serializable]
    public struct Data
    {
        public int actionLevel;
        public float gravityScale;
        public int maxCombo;
        public Vector2 beginSpeed;
        public Vector2 recoil;
        public string animationName;
        public string animationName_air;
        public Data( int actionLevel , float gravityScale, int maxCombo , Vector2 beginSpeed, Vector2 recoil , string animationName , string animationName_air)
        {
            this.actionLevel = actionLevel;
            this.gravityScale = gravityScale;
            this.maxCombo = maxCombo;
            this.beginSpeed = beginSpeed;
            this.recoil = recoil;
            this.animationName = animationName ;
            this.animationName_air = animationName_air;
        }
    }

    [SerializeField]
    public Data handgun = new Data(2 , 0 , 5 ,new Vector2(0,0) , new Vector2(0,0) , "Homura_handGun" , "Homura_handGunAir");
    public Data RPG = new Data(4 , 0 , 1 ,new Vector2(0,0) , new Vector2(-1,0) , "Homura_RPG" , "Homura_RPGair");
    public Data minimi = new Data(3 , 0.1f , 10 ,new Vector2(0,0) , new Vector2(-0.1f,0) , "Homura_minimiB" , "Homura_minimi");
    public Data mortar = new Data(4 , 0 , 1 , new Vector2(0,0) , new Vector2(0,0) , "Homura_mortar" , "Homura_mortar");
    public Data granade = new Data(4 , 1 , 1 , new Vector2(-1,1) , new Vector2(0,0) , "Homura_granade" , "Homura_granadeAir");
    public Data granade_front = new Data(4 , 1 , 1 , new Vector2(1,1) , new Vector2(0,0) , "Homura_granadeB" , "Homura_granadeB");
    public Data timeFreeze = new Data(5 , 0 , 1 , new Vector2(0,0) , new Vector2(0,0) , "Homura_mortar" , "Homura_mortar");

}
