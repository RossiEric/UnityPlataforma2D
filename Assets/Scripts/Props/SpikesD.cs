using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesD : MonoBehaviour
{
    public int damage = 50;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerDamage Player = hitInfo.GetComponent<PlayerDamage>();
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (Player != null)
        {
            Player.TakeDamage(damage);
        }

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

    }

    void OnCollisionStay2D(Collision2D hitInfo)
    {
        PlayerDamage Player = hitInfo.gameObject.GetComponent<PlayerDamage>();
        Enemy enemy = hitInfo.gameObject.GetComponent<Enemy>();

        if (Player != null)
        {
            Player.TakeDamage(damage);
        }

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }



}
