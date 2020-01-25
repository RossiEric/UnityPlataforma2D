using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFell : MonoBehaviour
{
    public float fallMultipler = 2.5f;
    public float lowJumpMultipler = 2f;

    private Rigidbody2D m_Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Rigidbody2D.velocity.y < 0)
        {
            m_Rigidbody2D.gravityScale = fallMultipler;
        }
        else if(m_Rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            m_Rigidbody2D.gravityScale = lowJumpMultipler;
        }
        else
        {
            m_Rigidbody2D.gravityScale = 1f;
        }
    }
}
