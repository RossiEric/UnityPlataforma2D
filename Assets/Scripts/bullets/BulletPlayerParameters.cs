using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerParameters : MonoBehaviour
{
    public int damage = 10;
    public GameObject impactEffect;

    public float Speed;
    public Rigidbody2D rb2d;

    public PlayerMovement PlayerMovement;

    private AudioSource aEffecty;

    private void Awake()
    {
        aEffecty = GetComponent<AudioSource>();
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        rb2d.velocity = transform.right * Speed * Time.deltaTime;
        aEffecty.Play();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("@@@@@@- " + this.name + " - hit in -" + hitInfo.name);

        Enemy enemy = hitInfo.GetComponent<Enemy>();
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
