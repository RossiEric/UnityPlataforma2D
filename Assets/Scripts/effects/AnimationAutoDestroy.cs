using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour
{
    public float delay = 0f;
    private AudioSource aEffecty;

    // Use this for initialization
    void Start()
    {
        aEffecty = GetComponent<AudioSource>();
        if (aEffecty != null)
        {
            aEffecty.Play();
        }
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
