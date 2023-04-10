using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NORMAIL_MONSTER : MonoBehaviour
{
    public GameObject monster;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            //е═жий╟кл
            Instantiate(monster, player.transform.position + new Vector3(10, 0, 0), new Quaternion(0, 0, 0, 0));
            Destroy(this.gameObject);
        }
   }

}
