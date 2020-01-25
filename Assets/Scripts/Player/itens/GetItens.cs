using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItens : MonoBehaviour
{
    public int Heal;
    public GameObject deathEffect;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerDamage enemy = hitInfo.GetComponent<PlayerDamage>();
        if (enemy != null)
        {
            enemy.HealDamage(Heal);

            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
