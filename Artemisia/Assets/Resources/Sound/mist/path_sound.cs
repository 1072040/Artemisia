using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path_sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Invoke("dead", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void dead()
    {
        Destroy(this.gameObject);
    }
}
