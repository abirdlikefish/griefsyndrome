using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            IHealth m_IHealth = other.GetComponent<IHealth>();
            m_IHealth.BeHit(1);
        }
    }
}
