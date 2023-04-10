using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WATCHMONSTER_STOP : MonoBehaviour
{
    public GameObject Eventcontrol;
    void Start()
    {
        Eventcontrol=GameObject.Find("EventSystem");
    }

    private void OnTriggerEnter(Collider Skull) //判斷道具在怪物的哪個方位
    {
        if(Skull.gameObject.tag=="Item")
        {
            Eventcontrol.GetComponent<EVENTCONTROL>().Skull301=null;
        }
    }
}
