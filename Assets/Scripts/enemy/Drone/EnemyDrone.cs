using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private Transform Player;
    public bool Movendo;

    //Range
    public LayerMask whatIsEnemies;
    public float attackrange; //Para HitBox em circulo
    public bool AtivarArma;

    private AudioSource aEffecty;

    //
    public bool SonLimitador = false;

    // Start is called before the first frame update
    void Start()
    {
        aEffecty = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        #region Flip Sprite
        if (Player.position.x > transform.position.x)
        {
            //face left
            transform.localScale = new Vector3(-0.3f, 0.3f, 1);
        }
        else if (Player.position.x < transform.position.x)
        {
            //face right
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
        #endregion

        if (AtivarArma)
        {
            #region Movendo Drone
            if (Vector2.Distance(transform.position, Player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, Player.position) < stoppingDistance && Vector2.Distance(transform.position, Player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (true)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, -speed * Time.deltaTime);
            }
            #endregion
        }

        #region Shoot Range, Active
        //hitBox em circulo
        Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(transform.position, attackrange, whatIsEnemies);
        if (enemiesToDamege.Length > 0)
        {
            for (int i = 0; i < enemiesToDamege.Length; i++)
            {
                AtivarArma = true;

                if (!SonLimitador)
                {
                    SonLimitador = true;
                    aEffecty.Play();
                }
            }
        }
        else
        {
            AtivarArma = false;
            SonLimitador = false;
        }
        #endregion

    }

    /// <summary>
    /// Visualizar em interface UNITY area de ataque
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        //hitBox em circulo
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }

}
