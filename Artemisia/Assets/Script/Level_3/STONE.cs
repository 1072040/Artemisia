using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STONE : MonoBehaviour
{
    private GameObject eventsystem;
    private void Start() {
        eventsystem = GameObject.Find("EventSystem");
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            eventsystem.GetComponent<DIALOG>().StartDialog(0,2,1);
        }
    }
}
