using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_MOVE : MonoBehaviour
{
    public bool move_able = true; //玩家能不能移動，不能的話改成false
    public bool is_move;
    public float move_speed;
    public Animator player_anim;

    //儲存boxcollider角落位置的物件
    private GameObject top_left;
    private GameObject top_right;
    private GameObject bottom_left;
    private GameObject bottom_right;
    private void Start()
    {
        //設定玩家生成位置
        this.gameObject.transform.position = PUBLIC_VALUE.record.GetPlayerPosition();
        //生成空物件，將物件移動到boxcollider角落
        top_left = new GameObject("top_left");
        top_left.transform.parent = this.transform;
        top_left.transform.localPosition = new Vector3(this.GetComponent<BoxCollider>().center.x - this.GetComponent<BoxCollider>().size.x / 2, 0, this.GetComponent<BoxCollider>().center.z + this.GetComponent<BoxCollider>().size.z / 2);

        top_right = new GameObject("top_right");
        top_right.transform.parent = this.transform;
        top_right.transform.localPosition = new Vector3(this.GetComponent<BoxCollider>().center.x + this.GetComponent<BoxCollider>().size.x / 2, 0, this.GetComponent<BoxCollider>().center.z + this.GetComponent<BoxCollider>().size.z / 2);

        bottom_left = new GameObject("bottom_left");
        bottom_left.transform.parent = this.transform;
        bottom_left.transform.localPosition = new Vector3(this.GetComponent<BoxCollider>().center.x - this.GetComponent<BoxCollider>().size.x / 2, 0, this.GetComponent<BoxCollider>().center.z - this.GetComponent<BoxCollider>().size.z / 2);

        bottom_right = new GameObject("bottom_right");
        bottom_right.transform.parent = this.transform;
        bottom_right.transform.localPosition = new Vector3(this.GetComponent<BoxCollider>().center.x + this.GetComponent<BoxCollider>().size.x / 2, 0, this.GetComponent<BoxCollider>().center.z - this.GetComponent<BoxCollider>().size.z / 2);

    }
    void Update()
    {
        

        if (move_able)
        {

            /*
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * vertical * move_speed* Time.deltaTime);
            transform.Translate(Vector3.right * horizontal * move_speed* Time.deltaTime);
            
            if(vertical>0) player_anim.SetFloat("move_forward" , 1f);
            if(vertical<0) player_anim.SetFloat("move_forward" , 2f);
            if(horizontal<0) player_anim.SetFloat("move_forward" , 3f);
            if(horizontal>0) player_anim.SetFloat("move_forward" , 4f);
            */
            if (Input.GetKey(KeyCode.W)||Input.GetKey("up")) //前
            {
                if (!Physics.Raycast(top_left.transform.position, Vector3.forward, move_speed * Time.deltaTime) && !Physics.Raycast(top_right.transform.position, Vector3.forward, move_speed * Time.deltaTime))
                {
                    if(Input.GetKey(KeyCode.W)||Input.GetKey("up")) player_anim.SetFloat("move_forward" , 1f);
                    this.transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.S)||Input.GetKey("down")) //后
            {
                if (!Physics.Raycast(bottom_left.transform.position, Vector3.back, move_speed * Time.deltaTime) && !Physics.Raycast(bottom_right.transform.position, Vector3.back, move_speed * Time.deltaTime))
                {
                    if(Input.GetKey(KeyCode.S)||Input.GetKey("down")) player_anim.SetFloat("move_forward" , 2f);
                    this.transform.Translate(Vector3.forward * -move_speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.A)||Input.GetKey("left")) //左
            {
                if (!Physics.Raycast(top_left.transform.position, Vector3.left, move_speed * Time.deltaTime) && !Physics.Raycast(bottom_left.transform.position, Vector3.left, move_speed * Time.deltaTime))
                {
                    if(Input.GetKey(KeyCode.A)||Input.GetKey("left")) player_anim.SetFloat("move_forward" , 3f);
                    this.transform.Translate(Vector3.right * -move_speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D)||Input.GetKey("right")) //右
            {
                if (!Physics.Raycast(top_right.transform.position, Vector3.right, move_speed * Time.deltaTime) && !Physics.Raycast(bottom_right.transform.position, Vector3.right, move_speed * Time.deltaTime))
                {
                    if(Input.GetKey(KeyCode.D)||Input.GetKey("right")) player_anim.SetFloat("move_forward" , 4f);
                    this.transform.Translate(Vector3.right * move_speed * Time.deltaTime);
                }
            }


        }
        if (move_able)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)||Input.GetKey("up")||Input.GetKey("down")||Input.GetKey("left")||Input.GetKey("right"))
            {
                is_move = true;
                player_anim.SetBool("is_moving" ,is_move);
            }
            else
            {
                is_move = false;
                player_anim.SetBool("is_moving" ,is_move);
            }
        }
        else{
             player_anim.SetBool("is_moving" ,false);
        }
    }

    
}
