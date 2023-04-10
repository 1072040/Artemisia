using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_monster : MonoBehaviour
{
    public GameObject monster;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Player")
        {
            Instantiate(monster,new Vector3(-8.16f, 0, 0.66f), new Quaternion(0, 0, 0, 0));
            Destroy(this.gameObject);
        }
    }
}
