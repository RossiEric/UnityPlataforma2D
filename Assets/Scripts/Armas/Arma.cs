using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    //https://github.com/Brackeys/2D-Shooting
    public bool Ativa;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private void Start()
    {
        Ativa = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
       
    }

    void Shoot()
    {

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
