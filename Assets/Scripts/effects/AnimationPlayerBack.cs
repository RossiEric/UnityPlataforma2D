using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerBack : MonoBehaviour
{
    public float delay = 0f;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Use this for initialization
    void Start()
    {
        //player.GetComponent<PlayerMelee>().MeleeActive = false;

        Debug.Log(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length < 1)
        {
            Destroy(gameObject);
        }

        //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
