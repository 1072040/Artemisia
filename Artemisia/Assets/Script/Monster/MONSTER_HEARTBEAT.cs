using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MONSTER_HEARTBEAT : MonoBehaviour
{
    private GameObject Main_player;
    private GameObject Eventcontrol; //抓主角
    private float Count_distance = 0;
    private bool Is_heartsound;
    
    public HEARTBEAT_CONTROL HEARTBEAT_CONTROL;
    private GameObject Heart_sound1;
    private GameObject Heart_sound2;
    private GameObject Heart_sound3;
    private GameObject no_sound;
    void Start()
    {
        HEARTBEAT_CONTROL = GameObject.Find("HeartSound").GetComponent<HEARTBEAT_CONTROL>();
        Heart_sound1 = HEARTBEAT_CONTROL.Heart_sound1;
        Heart_sound2 = HEARTBEAT_CONTROL.Heart_sound2;
        Heart_sound3 = HEARTBEAT_CONTROL.Heart_sound3;
        no_sound = HEARTBEAT_CONTROL.no_sound;
        Eventcontrol = GameObject.Find("EventSystem");
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
    }

    private void Distance()
    {
        if(Count_distance<=6)
        {
            this.transform.parent = Heart_sound1.transform;
        }
        else if(Count_distance<=13 )
        {
            this.transform.parent = Heart_sound2.transform;
        }
        else if(Count_distance <= 20)
        {
            this.transform.parent = Heart_sound3.transform;
        }
        else
        {
            this.transform.parent = no_sound.transform;
        }
    }
    void Update()
    {
        Is_heartsound = Eventcontrol.GetComponent<EVENTCONTROL>().Is_heartbeat;
        Count_distance = Mathf.Sqrt(Mathf.Pow(Main_player.transform.position.x - gameObject.transform.position.x, 2) + Mathf.Pow(Main_player.transform.position.z - gameObject.transform.position.z, 2)) ;
        if (Is_heartsound==false)
        {
            Distance();

        }

    }
}
