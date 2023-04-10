using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FOG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PUBLIC_VALUE.status_operator.WriteState(3,SceneManager.GetActiveScene().buildIndex-2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
