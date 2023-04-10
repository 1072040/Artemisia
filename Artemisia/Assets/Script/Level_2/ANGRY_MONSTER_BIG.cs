using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class ANGRY_MONSTER_BIG : MonoBehaviour
{
    private float Save_pox; //記錄鬼當下的X座標
    private float Save_poz;//記錄鬼當下的Y座標
    public NavMeshAgent Agent;    //宣告NavMeshAgent
    public GameObject Main_player;    //要追蹤的物件
    private GameObject Player_save;    //存player(上面的會改追蹤目標 這個不會)
    public GameObject Eventcontrol; //抓主角

    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = true;
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
        Main_player.GetComponent<HIDE_IN_TREE>().is_wandering = true;
        Player_save = GameObject.Find("Player");
        Agent.updateRotation = false; //關閉怪物旋轉
        Agent.isStopped=true;
        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Main_player.GetComponent<HIDE_IN_TREE>().is_hide == false)
        {
            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Active()
    {
        Agent.SetDestination(Main_player.transform.position);
        if (Save_pox > transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(1, 1, 1);
        }
        else if (Save_pox < transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(-1, 1, 1);
        }
        if (Save_poz > transform.position.z)
        {
            transform.GetChild(0).GetChild(2).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(3).localScale = new Vector3(1, 1, 1);
        }
        else if (Save_poz < transform.position.z)
        {
            transform.GetChild(0).GetChild(2).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(3).localScale = new Vector3(-1, 1, 1);
        }

        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
    }

    void FixedUpdate()
    {
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
        Debug.Log("abuabu");
        if (!IsInvoking("Active"))
        {
            InvokeRepeating("Active", 0.01f, 0.02f);
        }
    }
    private void OnDestroy()
    {
        CancelInvoke("Active");
        if (Eventcontrol != null)
        {
            Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = false;
        }
        if (Player_save != null)
        {
            Player_save.GetComponent<HIDE_IN_TREE>().is_wandering = false;
        }
    }
}
