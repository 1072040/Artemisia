using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ANGRY_MONSTER : MonoBehaviour
{
    public GameObject Main_player;    //要追蹤的物件
    public Animator Angrt_monster_small;
    public GameObject Eventcontrol;    //要追蹤的物件
    public bool Is_appear = false;
    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
    }
    //主角進入怪物追蹤範圍 怪物開始追蹤
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Is_appear == false && Main_player.GetComponent<ENDURANCE>().is_run)
        {
            Angrt_monster_small.SetBool("angry", true);
            Angrt_monster_small.SetBool("hide", false);
            Is_appear = true;
        }
        else if(collision.gameObject.tag == "Player" && Is_appear == false)
        {
            Angrt_monster_small.SetBool("hide", false);
            Angrt_monster_small.SetBool("idle", true);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Main_player.GetComponent<ENDURANCE>().is_run&& Is_appear == false)
            {
                Angrt_monster_small.SetBool("idle", false);
                Angrt_monster_small.SetBool("angry", true);
                Is_appear = true;
            }

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player"&& Is_appear ==false)
        {
            Angrt_monster_small.SetBool("idle", false);
            Angrt_monster_small.SetBool("hide", true);

        }
    }
    void Update()
    {
       // print(Is_appear);
    }

}

