using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlataformProd : MonoBehaviour
{
    //https://www.youtube.com/watch?v=M_kg7yjuhNg

    private PlatformEffector2D effector;
    public float waitTime;

    //raio de uso
    public float range; //Para HitBox em circulo
    public LayerMask whatIsTarget;

    //Player
    public PlayerMovement playerMovement;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] PlayerIsHere = Physics2D.OverlapCircleAll(transform.position, range, whatIsTarget);
        for (int i = 0; i < PlayerIsHere.Length; i++)
        {
            //Debug.Log("Player is here");
            playerMovement = PlayerIsHere[i].GetComponent<PlayerMovement>();

            if (playerMovement.crouch == true) //&& playerMovement.jump == true
            {
                Debug.Log("playerMovement.crouch == true");
                effector.rotationalOffset = 180f;
                waitTime = 0.2f;
            }
            else if (waitTime <= 0 && effector.rotationalOffset != 0f) 
            {
                Debug.Log("else if (waitTime <= 0) ");
                effector.rotationalOffset = 0f;
            }

            if (playerMovement.jump == true)
            {
                Debug.Log("(playerMovement.jump == true)");
                effector.rotationalOffset = 0f;
            }

            if (waitTime > 0)
            {
                Debug.Log("if (waitTime > 0)");
                waitTime -= Time.deltaTime;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            effector.rotationalOffset = 0f;
        }
    }

    /// <summary>
    /// Visualizar em interface UNITY area de ataque
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        //hitBox em circulo
        Gizmos.DrawWireSphere(transform.position, range);
    }
}