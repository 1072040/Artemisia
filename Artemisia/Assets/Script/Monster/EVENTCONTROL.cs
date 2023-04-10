using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EVENTCONTROL : MonoBehaviour
{
    public bool Is_tracks=false;
    public bool Is_heartbeat=false;

    public int Count_tombstone_num=0;
    public GameObject Player;

    public GameObject Skull301;

    void Awake()
    {
        Player=GameObject.Find("Player");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {

        this.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
