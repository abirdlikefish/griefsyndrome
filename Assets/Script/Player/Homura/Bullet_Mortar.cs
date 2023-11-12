using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Mortar : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    private void Awake() 
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_rigidBody.gravityScale = HomuraIntelligence.Instance.mortar.bulletGravityScale;
    }
    public void Initialization()
    {
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            IHealth midHealth = other.GetComponent<EnemyBase>();
            midHealth.BeHit(1);
            DestroyBullet();
        }
        else if(other.tag == "Ground")
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        m_rigidBody.velocity = Vector2.zero;
        ObjectPool.Instance.returnObject(gameObject);
    }
}
