using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LV1_LASTMONSTER : MonoBehaviour
{
     public NavMeshAgent Agent;    //宣告NavMeshAgent
    public GameObject Main_player;    //要追蹤的物件
    public GameObject Eventcontrol; //抓主角
    private bool Is_track; //是否再追蹤主角
    private bool In_range = false;
    private float Save_pox; //記錄鬼當下的X座標
    private float Save_poz;//記錄鬼當下的Y座標

    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false; //關閉怪物旋轉
        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
        In_range = true;
        Is_track = true;
        Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = Is_track;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            //要改位置
             Vector3 position = new Vector3((float)-11.4062, (float)0,(float)-13);
             PUBLIC_VALUE.record.SetPlayerPosition(position);

            //PUBLIC_VALUE.monster_killed = true;
            //Time.timeScale = 0;
            //播死亡動畫並重新開始
            Destroy(this.gameObject);
            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_burst(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void Active()
    {
        Agent.SetDestination(Main_player.transform.position);

        //動畫方向
        if (Save_pox > transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(-1, 1, 1);
        }
        else if (Save_pox < transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(1, 1, 1);
        }
        if (Save_poz > transform.position.z)
        {
            transform.GetChild(0).GetChild(2).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(3).localScale = new Vector3(-1, 1, 1);
        }
        else if (Save_poz < transform.position.z)
        {
            transform.GetChild(0).GetChild(2).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(3).localScale = new Vector3(1, 1, 1);
        }

        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
    }
    void FixedUpdate()
    {
        if (In_range && Is_track)
        {
            Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
            if (!IsInvoking("Active"))
            {
                InvokeRepeating("Active", 0.01f, 0.02f);
            }
        }
        if(Main_player.GetComponent<HIDE_IN_TREE>().is_hide == true)
        {
            Vector3 position = new Vector3((float)-11.4062, (float)0,(float)-13);
             PUBLIC_VALUE.record.SetPlayerPosition(position);
             Eventcontrol.GetComponent<PLAYER_DIE>().DIE_burst(SceneManager.GetActiveScene().buildIndex);
             Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = false;
            
        }
    }

    private void OnDestroy()
    {
        CancelInvoke("Active");
        if (Eventcontrol != null)
        {
            Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = false;
        }
    }
}
