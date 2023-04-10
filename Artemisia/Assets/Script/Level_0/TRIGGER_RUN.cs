using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRIGGER_RUN : MonoBehaviour
{
    public GameObject Level_0;
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            Level_0.GetComponent<LEVEL_0>().Status=1;
            Destroy(gameObject);
        }
    }
}
