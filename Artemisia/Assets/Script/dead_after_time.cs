using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead_after_time : MonoBehaviour
{
    public float time;
    public bool is_dead;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (is_dead)
        {
            Invoke("dead", time);
        }
        else
        {
            Invoke("turn_off", time);
        }
    }
    void dead()
    {
        Destroy(gameObject);
    }

    void turn_off()
    {
        gameObject.SetActive(false);
    }
}
