using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Homura_DodgeArea : MonoBehaviour , IHealth
{
    IHomuraDodge m_IHomuraDodge;

    public void BeHit(float damage)
    {
        m_IHomuraDodge.IsDodgeSucceeded = true;
        // Debug.Log("succeeded");
    }

    public void Initialization(Homura homura)
    {
        m_IHomuraDodge = homura ;
    }

}
