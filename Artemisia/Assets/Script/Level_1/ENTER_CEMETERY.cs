using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENTER_CEMETERY : MonoBehaviour
{
    private GameObject EventSystem;

    void Start(){
        EventSystem = GameObject.Find("EventSystem");
    }
    private void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.tag=="Player")
        {
            print("enter");
           EventSystem.GetComponent<DIALOG>().StartDialog(-1,1,6);

          EventSystem.GetComponent<BAG>().UseItem(6);

           Destroy(gameObject);
        }
    }
}
