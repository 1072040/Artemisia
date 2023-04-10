using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class LV3_NORMALMONSTER : MonoBehaviour
{
  public NavMeshAgent Agent;    //宣告NavMeshAgent
    public GameObject Main_player;    //要追蹤的物件
    private GameObject Player_save;    //存player(上面的會改追蹤目標 這個不會)
    public GameObject Eventcontrol; //抓主角
    public Animator[] Normal_animator; //怪物animator
    
    private bool Is_track; //是否再追蹤主角
    private bool In_range = false;
    private bool Check_dialog = false;
    private float Save_pox; //記錄鬼當下的X座標
    private float Save_poz;//記錄鬼當下的Y座標

    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
        Agent = GetComponent<NavMeshAgent>();
        Agent.isStopped = true;
        Agent.updateRotation = false; //關閉怪物旋轉
        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
        Player_save = GameObject.Find("Player");
    }

    //主角進入怪物追蹤範圍 怪物開始追蹤
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            Player_save.GetComponent<HIDE_IN_TREE>().is_wandering = true;
            In_range = true;
            Is_track = true;
            Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = Is_track;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Player_save.GetComponent<HIDE_IN_TREE>().is_hide == false)
        {
            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
            Main_player.GetComponent<PLAYER_MOVE>().move_able=false;
        }
    }

    private void Active()
    {
        
        Agent.SetDestination(Main_player.transform.position);

        if (Save_pox > transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(-1, 1, 1);
        }
        else if(Save_pox < transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(1, 1, 1);
        }
        if (Save_poz > transform.position.z)
        {
            transform.GetChild(0).GetChild(2).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(3).localScale = new Vector3(-1, 1, 1);
        }
        else if(Save_poz < transform.position.z)
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
            
                foreach(Animator normal_animator in Normal_animator)
                {
                    normal_animator.SetBool("normal_attack1", true);
                }
                 if (!IsInvoking("Active"))
            {
                InvokeRepeating("Active", 0.01f, 0.02f);
            }
            

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
