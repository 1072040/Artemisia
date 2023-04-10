using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOLLOW_PLAYER : MonoBehaviour
{
    public GameObject player;
    public float smooth=10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.transform.parent.GetComponent<HIDE_IN_TREE>().is_hide)
        {
            /*
            this.transform.position = Vector3.Lerp(this.transform.position,
               new Vector3(player.gameObject.transform.position.x,
               this.transform.position.y ,
               player.gameObject.transform.position.z - Mathf.Tan((90 - this.transform.eulerAngles.x) * Mathf.PI / 180) * this.transform.position.y),
               smooth * Time.deltaTime);*/
            Vector3 camera_position = this.transform.position;
            Vector3 player_position = player.gameObject.transform.position;
            this.transform.position = Vector3.Lerp(camera_position, player_position , smooth * Time.deltaTime);
        }
    }
}
