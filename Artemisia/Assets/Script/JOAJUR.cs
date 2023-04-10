using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JOAJUR : MonoBehaviour
{
    
    void Update()
    {
        if(PUBLIC_VALUE.tool_operator.GetToolState(4,1))
        {
            Destroy(gameObject);
        }
    }
}
