using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public bool MeleeActive;
    private float TimeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public Transform attackBaixo;
    public LayerMask whatIsEnemies;

    public float attackrange; //Para HitBox em circulo

    public float attackrangeX; //Para HitBox em retangulo / quadrado
    public float attackrangeY; //Para HitBox em retangulo / quadrado
    public float attackAngulo;

    public int damage;

    public Animator anime;
    public GameObject attackEffect;

    public PlayerMovement PlayerMovement;

    private void Awake()
    {
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        //MeleeActive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimeBtwAttack <= 0)
        {
            MeleeActive = false;
            AtackPose(MeleeActive);

            //pode atacar
            if (Input.GetKeyUp(KeyCode.F))
            {
                //https://www.youtube.com/watch?v=1QfxdUpVh5I

                if (!MeleeActive)
                {
                    //Debug.Log("attack!");
                    MeleeActive = true;
                    AtackPose(MeleeActive);

                    if (PlayerMovement.crouch == false)
                    {
                        Instantiate(attackEffect, attackPos.transform.position, Quaternion.identity); //ANIMACAO DE DANO, SANGUE

                        //hitBox em circulo
                        //Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(attackPos.position, attackrange, whatIsEnemies);

                        //HitBox em quadrado ou retangulo
                        Collider2D[] enemiesToDamege = Physics2D.OverlapBoxAll(new Vector2(attackPos.position.x, attackPos.position.y), new Vector2(attackrangeX, attackrangeY), attackAngulo, whatIsEnemies);

                        for (int i = 0; i < enemiesToDamege.Length; i++)
                        {
                            GetComponent<PlayerDamage>().KnockBack(enemiesToDamege[i].transform);
                            enemiesToDamege[i].GetComponent<Enemy>().TakeDamage(damage);
                        }

                    }
                    else
                    {
                        Instantiate(attackEffect, attackBaixo.transform.position, Quaternion.identity); //ANIMACAO DE DANO, SANGUE

                        //hitBox em circulo
                        //Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(attackPos.position, attackrange, whatIsEnemies);

                        //HitBox em quadrado ou retangulo
                        Collider2D[] enemiesToDamege = Physics2D.OverlapBoxAll(new Vector2(attackBaixo.position.x, attackBaixo.position.y), new Vector2(attackrangeX, attackrangeY), attackAngulo, whatIsEnemies);

                        for (int i = 0; i < enemiesToDamege.Length; i++)
                        {
                            GetComponent<PlayerDamage>().KnockBack(enemiesToDamege[i].transform);
                            enemiesToDamege[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }

                }

            }

            TimeBtwAttack = startTimeBtwAttack;
        }
        else
        {

            TimeBtwAttack -= Time.deltaTime;
        }
    }

    private void AtackPose(bool _MeleeActive) {

        if (PlayerMovement.crouch == false)
        {
            anime.SetBool("melee", _MeleeActive);
        }
        
    }

    /// <summary>
    /// Visualizar em interface UNITY area de ataque
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        //hitBox em circulo
        //Gizmos.DrawWireSphere(attackPos.position, attackrange);

        //HitBox em quadrado ou retangulo
        //Gizmos.DrawWireCube(attackPos.position, new Vector3(attackrangeX, attackrangeY, 1)); // Não apresenta angulo, apenas 1 no lugar de Z para posição em 2D

        Gizmos.DrawWireCube(new Vector2(attackPos.position.x, attackPos.position.y), new Vector3(attackrangeX, attackrangeY, 1)); // Não apresenta angulo, apenas 1 no lugar de Z para posição em 2D
    }


}
