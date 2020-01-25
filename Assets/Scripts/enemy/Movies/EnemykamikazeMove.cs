using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemykamikazeMove : MonoBehaviour
{
    //https://www.youtube.com/watch?v=rhoQd6IAtDo
    [Header("Objeto Parametros")]
    public float Speed;
    public float MinSpeed;
    public float MaxSpeed;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(MinSpeed, MaxSpeed);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            //vai direto
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }

        

        //vai apenas Y
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target.position.y), speed * Time.deltaTime);
    }
}
