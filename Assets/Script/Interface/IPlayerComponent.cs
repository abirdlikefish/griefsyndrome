using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerComponent
{
    public Rigidbody2D rigidbody2D { get; set; }
    public Collider2D collider2D { get; set; } 
    public SpriteRenderer spriteRenderer { get; set; }
    public Animator animator { get; set; }
}
