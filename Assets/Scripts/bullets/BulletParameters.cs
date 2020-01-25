using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParameters : MonoBehaviour
{
    public int damage = 40;
    public GameObject impactEffect;

    public float Speed;
    public Rigidbody2D rb2d;
    Transform target;

    private AudioSource aEffecty;

    void Start()
    {
        aEffecty = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (target.position.x > transform.position.x)
        {
            //face left
            rb2d.velocity = transform.right * 1 * Speed;
        }
        else if (target.position.x < transform.position.x)
        {
            //face right
            rb2d.velocity = transform.right * -1 * Speed;
        }

        aEffecty.Play();

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("@@@@@@- " + this.name + " - hit in -" + hitInfo.name);

        PlayerDamage enemy = hitInfo.GetComponent<PlayerDamage>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
