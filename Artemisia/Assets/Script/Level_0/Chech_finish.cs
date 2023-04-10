using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chech_finish : MonoBehaviour
{
    public GameObject Level_0;
    private GameObject Player;
    private GameObject Eventsystem;

    void Start()
    {
        Player=GameObject.Find("Player");
        Eventsystem=GameObject.Find("EventSystem");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Level_0.GetComponent<LEVEL_0>().Status ==1)
            {
                Eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 5);
            }
            else if (Level_0.GetComponent<LEVEL_0>().Status ==2)
            {
                Eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 7);
            }
            else if (Level_0.GetComponent<LEVEL_0>().Status ==3)
            {
                Eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 7);
            }
            else if (Level_0.GetComponent<LEVEL_0>().Status ==4)
            {
                Eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 9);
            }
            else if (Level_0.GetComponent<LEVEL_0>().Status ==5)
            {
                Eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 9);
            }
            else if (Level_0.GetComponent<LEVEL_0>().Status ==6)
            {
                Eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 10);
            }
            else if (Level_0.GetComponent<LEVEL_0>().Status ==8)
            {
                Eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 10);
            }
            else if(Level_0.GetComponent<LEVEL_0>().Status ==9)
            {
                Destroy(gameObject);
            }

        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            if(Level_0.GetComponent<LEVEL_0>().Status ==9)
            {
                Destroy(gameObject);
            }

        }
    }
}
