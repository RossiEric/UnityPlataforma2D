using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour
{
    //https://www.youtube.com/watch?v=dwcT-Dch0bA&t=671s

    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    public float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    //mira player
    private GameObject[] Miras;

    [Header("Dash")]
    [Space]
    public float DashForce;
    public float DashCooldown;
    public float DashCooldownTimer;
    public GameObject DashEffect;
    public Image DashIndicator;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;
    public Animator anime;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded) { 
                    OnLandEvent.Invoke();
                }
            }
        }

        LandingOrJumpung(m_Grounded);

        if (DashCooldownTimer > 0)
        {
            DashCooldownTimer -= Time.deltaTime;
        }

    }

    /// <summary>
    /// Visualizar areas dinamicas para abaixar e ground
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);

        Gizmos.DrawWireSphere(m_CeilingCheck.position, k_CeilingRadius);
    }

    public void Move(float move, bool crouch, bool jump, int? DashIng, bool doubleJump)
    {
        if (DashIng != null && DashCooldownTimer <= 0)
        {
            Dash(DashIng);
        }

        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }

        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            //Debug.Log("if (m_Grounded || m_AirControl)");

            // If crouching
            if (crouch)
            {
                

                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null) {

                    //Debug.Log("crouch: " + crouch.ToString());

                    m_CrouchDisableCollider.enabled = false;
                }

                crouchState(crouch);

                if (move != 0f)
                {
                    anime.SetBool("run", true);
                }
                else
                {
                    anime.SetBool("run", false);
                }

            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null) {
                    m_CrouchDisableCollider.enabled = true;
                }
                    

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            

            if (move != 0f)
            {
                anime.SetBool("run", true);
            }
            else
            {
                anime.SetBool("run", false);
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If the player should jump...
        if (m_Grounded && jump)
        {
            LandingOrJumpung(m_Grounded);

            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

        // If the player should double jump...
        if (!m_Grounded && doubleJump)
        {
            LandingOrJumpung(m_Grounded);

            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.AddForce(new Vector2(0f, (m_JumpForce)));
        }

        crouchState(crouch);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //transform.Rotate(0f, 180f, 0f);
        Miras = GameObject.FindGameObjectsWithTag("MiraPlayer");
        foreach (var item in Miras)
        {
            item.transform.Rotate(0f, 180f, 0f);
        }

    }

    #region Dash
    public void Dash(int? DashIng)
    {
        DashCooldownTimer = DashCooldown;

        Instantiate(DashEffect, transform.position, Quaternion.identity);

        if (DashIng == 1)
        {
            m_Rigidbody2D.velocity = Vector2.right * DashForce;
        }
        else
        {
            m_Rigidbody2D.velocity = Vector2.left * DashForce;
        }

        StartCoroutine(Dashend());
    }

    IEnumerator Dashend()
    {
        //DashIndicator
        SetTransparency(DashIndicator, 0.3f);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);
        SetTransparency(DashIndicator, 1f);
        m_Rigidbody2D.velocity = Vector2.zero;
    }

    /// <summary>
    /// Set transparece in images UI
    /// </summary>
    /// <param name="p_image"></param>
    /// <param name="p_transparency"></param>
    public static void SetTransparency(Image p_image, float p_transparency)
    {
        if (p_image != null)
        {
            Color __alpha = p_image.color;
            __alpha.a = p_transparency;
            p_image.color = __alpha;
        }
    }
    #endregion

    #region Animates
    public void LandingOrJumpung(bool jump)
    {
        anime.SetBool("jump", !jump);
    }

    public void crouchState(bool _crouch)
    {
        anime.SetBool("crouch", _crouch);
    }
    #endregion
}
