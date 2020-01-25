using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    //youtube.com/watch?v=_Z1t7MNk0c4

    public float speed;

    public int damage = 40;
    public GameObject impactEffect;
    public Rigidbody2D rg;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rg = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //mira
        transform.right = player.position - transform.position;

        //add force
        //rg.AddForce(transform.right * speed * Time.deltaTime);
        rg.velocity = transform.right * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
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
