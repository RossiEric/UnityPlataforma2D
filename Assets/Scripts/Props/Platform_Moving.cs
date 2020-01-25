using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Moving : MonoBehaviour
{
    public Transform Post1, Post2;
    public float Speed;
    public Transform startPos;

    Vector3 NextPos;

    private void Start()
    {
        NextPos = startPos.position;
    }

    private void FixedUpdate()
    {
        if (transform.position == Post1.position)
        {
            //NextPos = Post2.position;
            StartCoroutine(Gotoway(Post2));
        }

        if (transform.position == Post2.position)
        {
            //NextPos = Post1.position;
            StartCoroutine(Gotoway(Post1));
        }

        transform.position = Vector3.MoveTowards(transform.position, NextPos, Speed * Time.fixedDeltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Post1.position, Post2.position);
    }

    IEnumerator Gotoway(Transform _Goto)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1f);
        NextPos = _Goto.position;
    }

}
