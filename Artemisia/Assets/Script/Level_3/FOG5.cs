using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOG5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PUBLIC_VALUE.generate_operator.GetGenerate(57,3) == false){
                Destroy(GameObject.Find("Artemisia"));
            }
            PUBLIC_VALUE.status_operator.WriteState(3,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
