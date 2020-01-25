using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public bool AtackActive;
    private float TimeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public float attackrange; //Para HitBox em circulo
    public GameObject BulletPrefab;
    //public Animator anime;

    Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        #region Flip Sprite
        if (target.position.x > transform.position.x)
        {
            //face left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (target.position.x < transform.position.x)
        {
            //face right
            transform.localScale = new Vector3(1, 1, 1);
        }
        #endregion

        #region Shoot Range, Active
        //hitBox em circulo
        Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(attackPos.position, attackrange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamege.Length; i++)
        {
            //Debug.Log("TARGETing !!!!!");
            //enemiesToDamege[i].GetComponent<Enemy>().TakeDamage(damage);

            if (TimeBtwAttack <= 0)
            {
                AtackActive = false;
                //anime.SetBool("melee", AtackActive);

                //https://www.youtube.com/watch?v=1QfxdUpVh5I

                if (!AtackActive)
                {
                    AtackActive = true;
                    //anime.SetBool("melee", AtackActive);

                    //Debug.Log("shot!!!");
                    //enemiesToDamege[i].GetComponent<Enemy>().TakeDamage(damage);


                    //GameObject newObj = Instantiate(BulletPrefab, attackPos.transform.position, Quaternion.identity) as GameObject;

                    Instantiate(BulletPrefab, attackPos.position, attackPos.rotation);
                }

                TimeBtwAttack = startTimeBtwAttack;
            }
            else
            {

                TimeBtwAttack -= Time.deltaTime;
            }

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
        Gizmos.DrawWireSphere(attackPos.position, attackrange);
    }

}
