using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHECKGEM : MonoBehaviour
{

    public GameObject Level_1;
    public GameObject Lastmonster;
    public GameObject Watchmonster;
    private void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.tag=="Player")
        {
           // if(Level_1.GetComponent<LEVEL_1>().two_gem==true)
            if((PUBLIC_VALUE.tool_operator.GetToolState(2,1) == true || PUBLIC_VALUE.generate_operator.GetGenerateState(10,1) == 1) 
            && (PUBLIC_VALUE.tool_operator.GetToolState(3,1) == true || PUBLIC_VALUE.generate_operator.GetGenerateState(9,1) == 1))
            {
                Instantiate(Lastmonster, new Vector3(-4.16f, 0, -6.47f), new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
               

                Watchmonster=GameObject.Find("Watch_monster(Clone)");
                Destroy(Watchmonster);
                
            }
        }
    }
}
