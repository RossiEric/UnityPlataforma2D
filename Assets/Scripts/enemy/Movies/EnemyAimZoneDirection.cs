using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimZoneDirection : MonoBehaviour
{
    //https://www.youtube.com/watch?v=rhoQd6IAtDo

    public float speed;
    public float AtaqueZone; //distancia que para do Player para atacar
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) > AtaqueZone)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }
}
