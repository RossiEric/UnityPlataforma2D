using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimDirection : MonoBehaviour
{
    //https://www.youtube.com/watch?v=rhoQd6IAtDo

    public float speed;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //vai direto
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        //vai apenas Y
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target.position.y), speed * Time.deltaTime);

    }
}
