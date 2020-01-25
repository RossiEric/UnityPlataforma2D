using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int KnockBackMagnitude;
    public int health = 100;
    public int DanoDeColisao;
    public GameObject deathEffect;

    FlashSprites Efect;

    private void Start()
    {
        Efect = GameObject.Find("MainGame").GetComponent<FlashSprites>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            StartCoroutine(Efect.FlashSpritesActive(sprites, 5, 0.05f));
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void KnockBack(Transform transformEnemy)
    {
        var force = transform.position - transformEnemy.transform.position;

        force.Normalize();
        GetComponent<Rigidbody2D>().AddForce(force * KnockBackMagnitude);
    }

}
