using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemykamikazeInicialMove : MonoBehaviour
{
    //https://www.youtube.com/watch?v=rhoQd6IAtDo
    [Header("Objeto Parametros")]
    public float Speed;
    public float MinSpeed;
    public float MaxSpeed;

    Rigidbody2D rb2d;
    private Transform target;
    private bool Disparo;

    /// <summary>
    /// mira, dispara e não muda de direção
    /// </summary>
    void Start()
    {
        Disparo = false;
        Speed = Random.Range(MinSpeed, MaxSpeed);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target != null && Disparo == false)
        {
            rb2d.AddForce(target.position * Speed);
            Disparo = true;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
