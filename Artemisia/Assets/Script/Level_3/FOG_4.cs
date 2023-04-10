using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOG_4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PUBLIC_VALUE.generate_operator.GetGenerate(56,3) == false){
            Destroy(GameObject.Find("Diary_3"));
        }
        PUBLIC_VALUE.status_operator.WriteState(3,4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
