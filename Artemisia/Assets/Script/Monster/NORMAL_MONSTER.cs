using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class NORMAL_MONSTER : MonoBehaviour
{
    public NavMeshAgent Agent;    //宣告NavMeshAgent
    public GameObject Main_player;    //要追蹤的物件
    public GameObject Eventcontrol; //抓主角
    private bool Is_track; //是否再追蹤主角
    private bool In_range = false;
    private float Save_pox; //記錄鬼當下的X座標
    private float Save_poz;//記錄鬼當下的Y座標

    public bool Is_leave = true;
    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false; //關閉怪物旋轉
        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
    }

    //主角進入怪物追蹤範圍 怪物開始追蹤
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            In_range = true;
            Is_track = true;
            Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = Is_track;
        }
    }
    //主角離開怪物追蹤範圍 怪物消失
    private void OnTriggerExit(Collider Player)
    {
        if (Is_leave == true)
        {
            if (Player.gameObject.tag == "Player")
            {
                In_range = false;
                Is_track = false;
                Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = Is_track;
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Main_player.GetComponent<HIDE_IN_TREE>().is_hide == false)
        {
            //Time.timeScale = 0;
            //播死亡動畫並重新開始
            print("you dead");
            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void Active()
    {
        Agent.SetDestination(Main_player.transform.position);

        //動畫方向
        if (Save_pox - transform.position.x > 0.01 && Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) <= 1 && Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) >= -1)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            Debug.Log("物体往左移动");
        }
        else if (Save_pox - transform.position.x < 0.01 && Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) <= 1 && Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) >= -1)
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            Debug.Log("物体往右移动");
        }
        else if (Save_poz - transform.position.z > 0.01 && Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) <= 1 && Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) >= -1)
        {
            Debug.Log("物体往下移动");
        }
        else if (Save_poz - transform.position.z < 0.01 && Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) <= 1 && Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) >= -1)
        {
            Debug.Log("物体往上移动");
        }
        else if (Save_pox - transform.position.x < 0.01 && Save_poz - transform.position.z < 0.01)
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            Debug.Log("物体往右上移动");
        }
        else if (Save_pox - transform.position.x > 0.01 && Save_poz - transform.position.z < 0.01)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            Debug.Log("物体左上移动");
        }
        else if (Save_pox - transform.position.x < 0.01 && Save_poz - transform.position.z > 0.01)
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            Debug.Log("物体右下移动");
        }
        else if (Save_pox - transform.position.x > 0.01 && Save_poz - transform.position.z > 0.01)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            Debug.Log("物体左下移动");
        }
        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
    }
    void FixedUpdate()
    {
        if (In_range && Is_track)
        {
            Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
            Active();
        }
    }

    private void OnDestroy()
    {
        Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = false;
    }
}
