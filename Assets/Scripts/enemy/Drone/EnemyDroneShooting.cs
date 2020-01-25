using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneShooting : MonoBehaviour
{
    public float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;

    public bool AtivarArma;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        AtivarArma = GetComponent<EnemyDrone>().AtivarArma;

        if (timeBtwShots <= 0 && AtivarArma)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }
}
