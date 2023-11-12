using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System;
using UnityEngine.Analytics;

public class Disappear : MonoBehaviour
{
    private float m_beginTime;
    private float m_waitTime;
    private float m_disappearTime;
    private float m_beginAlpha;
    private SpriteRenderer m_spriteRenderer;
    public void Initialization(float waitTime , float disappearTime)
    {
        m_beginTime = Time.time;
        m_spriteRenderer = transform.GetComponent<SpriteRenderer>();
        m_spriteRenderer.color = new Color(m_spriteRenderer.color.r, m_spriteRenderer.color.g, m_spriteRenderer.color.b, 1);
        m_beginAlpha = m_spriteRenderer.color.a;
        m_waitTime = waitTime;
        m_disappearTime = disappearTime + waitTime;
    }
    
    void Update()
    {
        if(Time.time - m_beginTime < m_waitTime)
        {
            return;
        }
        float midAlpha = 1 - (Time.time - m_beginTime) / m_disappearTime;
        midAlpha = Mathf.Clamp(midAlpha , 0 , 1 ) * m_beginAlpha;
        Color midColor = m_spriteRenderer.color;
        midColor.a = midAlpha;
        m_spriteRenderer.color = midColor;
        if (Time.time - m_beginTime > m_disappearTime)
        {
            ObjectPool.Instance.returnObject(gameObject);
        // Destroy(gameObject);
        }
    }
}
