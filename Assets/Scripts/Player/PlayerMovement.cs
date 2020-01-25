using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    public float horizontalMove = 0f;
    public bool jump = false;
    public bool doubleJump = false;
    public bool crouch = false;
    public int? DashIng;

    public bool jumpCD = true;

    [Space]
    public bool rolarAtivo;
    public bool doubleJumpAtivo;

    //Sounds
    public AudioSource JumpaEffecty;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        #region Jump
        if (Input.GetButtonDown("Jump") && controller.m_Grounded && jumpCD)
        {
            jump = true;
            jumpCD = false; //libera pulo duplo
            controller.m_Grounded = false;
            JumpaEffecty.Play();
            //cancela efeito plataforma movel
            transform.transform.parent = null;
        } 

        if (Input.GetButtonDown("Jump") && !jump && !controller.m_Grounded && doubleJumpAtivo)
        {
            doubleJump = true;
            doubleJumpAtivo = false;
            JumpaEffecty.Play();
        }

        if (jump)
        {
            controller.m_Grounded = false;
        }

        if (controller.m_Grounded)
        {
            doubleJumpAtivo = true;
        }
        #endregion

        #region Move
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            crouch = true;
        }
        else if (Input.GetAxisRaw("Vertical") != -1)
        {
            crouch = false;
        }
        #endregion

        #region Dash
        if (Input.GetKeyUp(KeyCode.E))
        {
            DashIng = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            DashIng = -1;
        }
        else
        {
            DashIng = null;
        }
        #endregion

        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();
        Debug.Log("GetJoystickNames ::: " + temp.Length.ToString());

    }

    void FixedUpdate()
    {
        if (!rolarAtivo) //verifica se está liberado andar agachado 
        {
            if (crouch)
            {
                horizontalMove = 0f; //não permite mover enquanto agachado
            }
        }

        //moving
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, DashIng, doubleJump);

        if (jump)
        {
            jump = false;
            StartCoroutine(jumpCDrefresh());
        }
        
        doubleJump = false;        
    }

    IEnumerator jumpCDrefresh()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.2f);
        jumpCD = true;
    }


    #region Acompanha plataformas moveis: https://www.youtube.com/watch?v=DQYj8Wgw3O0
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Moving_Object"))
        {
            Debug.Log("@@@@ surfing!?");
            transform.transform.parent = other.transform;
        }
        else if (!other.gameObject.CompareTag("Moving_Object") && other.gameObject.layer == 8)
        {
            transform.transform.parent = null;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Moving_Object"))
        {
            Debug.Log("@@@@ surfing!?");
            transform.transform.parent = other.transform;
        }
        else if (!other.gameObject.CompareTag("Moving_Object") && other.gameObject.layer == 8)
        {
            transform.transform.parent = null;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Moving_Object"))
        {
            Debug.Log("@@@@ surfing OFF");
            transform.transform.parent = null;
        }
    }
    #endregion

}
