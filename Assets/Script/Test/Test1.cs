using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    protected bool m_isOnGround;
    public bool IsOnGround
    {
        get
        {
            RaycastHit2D mid ;
            float position_y = -0.4f;
            LayerMask layerMask = (1 << 8);
            for(float position_x = -0.1f ; position_x <= 0.1f ; position_x += 0.05f)
            {
                Vector2 midPosition = new Vector2(position_x + transform.position.x , position_y + transform.position.y ) ;
        Debug.DrawLine(transform.position , midPosition );
        Debug.DrawLine(midPosition , midPosition + Vector2.down * 0.1f , Color.red);
        Debug.DrawLine(midPosition + Vector2.down * 0.1f , midPosition + Vector2.down * 0.2f , Color.blue);
                mid = Physics2D.Raycast(midPosition , Vector2.down , 0.11f , layerMask);
                if(mid)
                {
                    m_isOnGround = true;
                    return true;
                }
        // Debug.Log(midPosition.ToString());
            }
            m_isOnGround = false;
            return false;
        }
        set{}
    }
    void Update() 
    {
        Debug.Log(IsOnGround);
    }
}
