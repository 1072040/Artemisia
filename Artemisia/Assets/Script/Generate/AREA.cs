using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AREA : MonoBehaviour
{
    private GameObject player;
    private GameObject eventsystem;
    // private void OnTriggerStay(Collider other) {
    //     if(other.CompareTag("Player")){
    //          player.GetComponent<PLAYER_INTERACTIVE>().current_area = int.Parse(this.gameObject.tag);
           
    //     }
    // }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        eventsystem = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
