using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SURPRISINGLY_MONSTER : MonoBehaviour
{
    public GameObject scare_ui;
    public float monster_time;
    private bool way;

    private GameObject Player; //主角
    private bool Up_left = false;
    private bool Up_right = false;
    private bool Down_left = false;
    private bool Down_right = false;
    private bool Up = false;
    private bool Down = false;
    private bool Right = false;
    private bool Left = false;
    private float Count_num = 0; //算兩點直線斜率

    void Start()
    {
        Player = GameObject.Find("Player");//抓主角的gameobject
        // gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().SetBool();
    }

    private void OnTriggerEnter(Collider collision)//被怪物撞到疲勞
    {
        if (collision.gameObject.tag == "Player")
        {
            print("good");
            Player.GetComponent<ENDURANCE>().image.fillAmount = 0;
            Player.GetComponent<ENDURANCE>().monster_time = monster_time;
            Instantiate(scare_ui);
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 5f)
        {

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, Time.deltaTime * 20);
        }
        else
        {
            if (way)
            {
                if (!IsInvoking("change_way"))
                {
                    Invoke("change_way", 6);
                }
                transform.Translate(Vector3.right*Time.deltaTime);
            }
            else
            {
                if (!IsInvoking("change_way"))
                {
                    Invoke("change_way", 6);
                }
                transform.Translate(Vector3.left* Time.deltaTime);
            }
            
        }


    }

    void change_way()
    {
        way = !way;
        this.transform.localScale = new Vector3(this.transform.localScale.x*-1, this.transform.localScale.y, this.transform.localScale.z);
    }
}
