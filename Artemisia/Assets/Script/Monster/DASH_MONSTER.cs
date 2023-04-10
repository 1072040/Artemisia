using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DASH_MONSTER : MonoBehaviour
{
    public NavMeshAgent Agent;    //宣告NavMeshAgent
    public GameObject Main_player;    //要追蹤的物件
    public GameObject Eventcontrol;    //要追蹤的物件

    private bool In_range = false;//是否在範圍
    private bool Is_on_skill = false;//是否使用衝刺
    private bool Is_dash = false;//是否在衝刺途中
    private bool Is_track = true;//是否繼續追蹤
    private bool Is_skill = true;//是否衝刺中
    private bool Up_left = false;
    private bool Up_right = false;
    private bool Down_left = false;
    private bool Down_right = false;
    private bool Up = false;
    private bool Down = false;
    private bool Right = false;
    private bool Left = false;
    public float Count_num = 0; //兩點算直線斜率

    public Vector3 player_pos; //紀錄主角當時的位置
    void Start()
    {
        Eventcontrol=GameObject.Find("EventSystem");
        Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks=Is_track;
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false; //關閉怪物旋轉
    }
    IEnumerator Dash_ghosts()//使用技能並且計算往哪個方向
    {
        Is_on_skill = true;
        if (Is_on_skill == true && Is_skill)
        {
            yield return new WaitForSeconds(2.5f);
            Agent.isStopped = true;
            Count_num = (Main_player.transform.position.z - Agent.transform.position.z) / (Main_player.transform.position.x - Agent.transform.position.x);
            player_pos=Main_player.transform.position;
            if (Agent.transform.position.x < Main_player.transform.position.x && Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) <= 1&&Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) >= -1)
            {
                Right = true;
                print("right");
            }
            else if (Agent.transform.position.x > Main_player.transform.position.x && Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) <= 1&&Mathf.Abs(Main_player.transform.position.z) - Mathf.Abs(Agent.transform.position.z) >= -1)
            {
                Left = true;
                print("left");
            }
            else if (Agent.transform.position.z < Main_player.transform.position.z && Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) <= 1&&Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) >= -1)
            {
                Up = true;
                print("Up");
            }
            else if (Agent.transform.position.z > Main_player.transform.position.z && Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) <= 1&&Mathf.Abs(Main_player.transform.position.x) - Mathf.Abs(Agent.transform.position.x) >= -1)
            {
                Down = true;
                print("down");
            }
            else if (Agent.transform.position.x < Main_player.transform.position.x && Agent.transform.position.z < Main_player.transform.position.z)
            {
                Up_right = true;
                print("Upright");
            }
            else if (Agent.transform.position.x > Main_player.transform.position.x && Agent.transform.position.z < Main_player.transform.position.z)
            {
                Up_left = true;
                print("Upleft");
            }
            else if (Agent.transform.position.x < Main_player.transform.position.x && Agent.transform.position.z > Main_player.transform.position.z)
            {
                Down_right = true;
                print("downright");
            }
            else if (Agent.transform.position.x > Main_player.transform.position.x && Agent.transform.position.z > Main_player.transform.position.z)
            {
                Down_left = true;
                print("downleft");
            }
            //播準備衝刺動畫
            yield return new WaitForSeconds(1f);
            Agent.isStopped = false;
            Is_dash = true;
            yield return new WaitForSeconds(0.5f);
            Up_right = false;
            Up_left = false;
            Down_left = false;
            Down_right = false;
            Right = false;
            Left = false;
            Up = false;
            Down = false;
            Is_on_skill = false;

        }
        Is_dash = false;
    }
    IEnumerator Wait_second()//暈眩3秒
    {
        yield return new WaitForSeconds(3f);
        Agent.isStopped = false;
        Is_track = true;
        Is_skill = true;
        Is_on_skill = false;
        StartCoroutine(Dash_ghosts());//重新使用技能

    }
    //主角進入怪物追蹤範圍 怪物開始追蹤
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            In_range = true;
        }
    }

    // //主角離開怪物追蹤範圍 怪物消失
    // private void OnTriggerExit(Collider Player)
    // {
    //     if (Player.gameObject.tag == "Player")
    //     {
    //         In_range = false;
    //         Destroy(gameObject);
    //     }
    // }
    //怪物追到主角 主角死亡
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            //播死亡動畫並重新開始
            print("you dead");
        }
        else if (collision.gameObject.tag == "obstacle")//撞到怪物時暈眩
        {
            if (Is_dash == true)
            {
                Agent.isStopped = true;
                Is_track = false;
                Is_skill = false;
                StartCoroutine(Wait_second());//暈眩3秒
            }
        }
    }
    private void Active()
    {
        if (Is_dash == false) //當沒衝刺時怪物自動追蹤
        {
            Agent.SetDestination(Main_player.transform.position);

        }
        else if (Is_dash == true) //當衝刺時怪物沿著直線往前衝刺
        {
            if (Up_right)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(Mathf.Abs( player_pos.x * Count_num), 0, Mathf.Abs( player_pos.z * Count_num)), Time.deltaTime * 20);
            }
            else if (Up_left)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(0 - Mathf.Abs( player_pos.x * Count_num), 0, Mathf.Abs( player_pos.z * Count_num)), Time.deltaTime * 20);
            }
            else if (Down_right)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(Mathf.Abs( player_pos.x * Count_num), 0, 0 - Mathf.Abs( player_pos.z * Count_num)), Time.deltaTime * 20);
            }
            else if (Down_left)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(0 - Mathf.Abs( player_pos.x * Count_num), 0, 0 - Mathf.Abs( player_pos.z * Count_num)), Time.deltaTime * 20);
            }
            else if (Right)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(50, 0, 0), Time.deltaTime * 20);
            }
            else if (Left)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(-50, 0, 0), Time.deltaTime * 20);
            }
            else if (Down)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(0, 0, -50), Time.deltaTime * 20);
            }
            else if (Up)
            {
                Agent.transform.position = Vector3.MoveTowards(Agent.transform.position,  player_pos + new Vector3(0, 0, 50), Time.deltaTime * 20);
            }
        }
    }

    void FixedUpdate()
    {
        if (Is_on_skill == false && In_range)//當在怪物範圍內且沒有衝刺時
        {
            StartCoroutine(Dash_ghosts());
        }
        else if (In_range && Is_track)//當在怪物範圍內且正在追蹤時
        {
            Active();
        }
    }

}
