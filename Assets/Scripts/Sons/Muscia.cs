using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muscia : MonoBehaviour
{

    private static Muscia mp;

    private void Awake()
    {
        if (mp == null)
        {
            mp = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }            
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
