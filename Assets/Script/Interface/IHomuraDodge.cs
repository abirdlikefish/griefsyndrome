using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHomuraDodge 
{
    public bool IsDodgeSucceeded { get; set; }
    // public GameObject DodgeArea { get; set; }
    // public bool IsInvincible { get; set; }
    public void SetDodgeArea();
    // public void GenerateShadow();
}
