using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public bool Active;
    public float TimeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public Transform attackBaixo;

    public Animator anime;
    public GameObject BulletPrefab;

    public PlayerMovement PlayerMovement;

    private void Awake()
    {
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimeBtwAttack <= 0)
        {
            Active = false;
            anime.SetBool("shot", false);

            //pode atacar
            if (Input.GetKeyUp(KeyCode.K) || Input.GetButtonDown("Fire3"))
            {
                //https://www.youtube.com/watch?v=1QfxdUpVh5I

                if (!Active)
                {
                    Active = true;

                    if (PlayerMovement.crouch == false)
                    {
                        //atira de pé
                        Shot(attackPos);
                    }
                    else
                    {
                        //atira baixo
                        Shot(attackBaixo);
                    }
                }
            }

            TimeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            TimeBtwAttack -= Time.fixedDeltaTime;
        }
    }

    public void Shot(Transform FirePoint) {
        Debug.Log("shot");
        anime.SetBool("crouch", PlayerMovement.crouch);
        anime.SetBool("shot", true);
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }

    private void AtackPose(bool _Active)
    {
        if (PlayerMovement.crouch == false)
        {
            anime.SetBool("melee", _Active);
        }
    }
}
