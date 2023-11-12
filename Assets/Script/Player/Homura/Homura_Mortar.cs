using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Homura_Mortar : MonoBehaviour
{
    public GameObject m_bullet_mortar;
    private float m_beginTime;
    private float m_waitTime;
    private bool m_isFired ;
    private Rigidbody2D m_rigidbody2D;

    private void Awake() 
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_rigidbody2D.gravityScale = HomuraIntelligence.Instance.gravityScale_mortar;
    }
    
    public void Initialization(float waitTime)
    {
        this.m_beginTime = Time.time;
        this.m_waitTime = waitTime;
        GetComponent<Disappear>().Initialization(waitTime,HomuraIntelligence.Instance.disappearTime);
        m_isFired = false;
    }

    void Update()
    {
        if((!m_isFired) && Time.time - m_beginTime > m_waitTime)
        {
            Fire();
            m_isFired = true;
            // ObjectPool.Instance.returnObject(gameObject);
        }
    }


    private void Fire()
    {
        GameObject midGameObject = ObjectPool.Instance.getObject(m_bullet_mortar);
        Rigidbody2D midrigidbody2D = midGameObject.GetComponent<Rigidbody2D>();
        midGameObject.GetComponent<Bullet_Mortar>().Initialization();
        midGameObject.transform.localScale = transform.localScale;
        midGameObject.transform.position = new Vector2(HomuraIntelligence.Instance.bulletPosition_mortar.x * transform.localScale.x + transform.position.x , HomuraIntelligence.Instance.bulletPosition_mortar.y + transform.position.y);
        midrigidbody2D.velocity = new Vector2(HomuraIntelligence.Instance.mortar.velocity.x * transform.localScale.x , HomuraIntelligence.Instance.mortar.velocity.y);
    }
    

}
