using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ON_HAND : MonoBehaviour
{
    public bool isHolding;
    bool isThrow;
    public int facing_side;
    public Transform hand_position;
    public GameObject throw_item;
    public GameObject EventSystem;
    public Animator player_anim;

    public Vector3 t_position = new Vector3(-0.59f,-0.2f, -0.2f);
    public Vector3 forace = new Vector3(-3,1,-2);
    public float power = 6000;
    
    private void Start() {
        isHolding = false;
        EventSystem=GameObject.Find("EventSystem");
    }
    private void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

            
        if(vertical>0) facing_side = 3;
        if(vertical<0) facing_side = 4;
        if(horizontal<0) facing_side = 1;
        if(horizontal>0) facing_side = 2;
    }
    public void Active(int id){
        isHolding = true;
        GetComponent<ENDURANCE>().is_holding = isHolding;
        LoadPrefabAndImage(id);
        if(id <350){ //可丟的東西
            isThrow = true;
            player_anim.SetBool("holding" , isHolding);
        }else {
            isThrow = false;//拖曳的東西
        }
    }
    public void PutOrThrow(){
        isHolding = false;
        GetComponent<ENDURANCE>().is_holding = isHolding;
        //判斷丟還是放
        if(isThrow){
            PlayThrowAnim();
        }else   {
            Put();
        }
    }
    public void Put(){
        
    }
    public void PlayThrowAnim(){
        GetComponent<PLAYER_MOVE>().move_able = false;
        GetComponent<Collider>().enabled = false;
        player_anim.SetBool("holding" , false);
        player_anim.SetBool("throw" , true);
        Invoke("Throw" , 0.01f);
    }
    public void Throw_sound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void Throw(){
        
        player_anim.SetBool("throw" , false);
        //生成在hand position
        GameObject clone = Instantiate(throw_item , hand_position.position , Quaternion.identity);
        EventSystem.GetComponent<EVENTCONTROL>().Skull301 = clone;
        
        Invoke("MoveAble" , 0.99f);
        //判斷方向
        switch(facing_side){
            //side 左右上下 -> 1234
            case 1:
            //左丟動畫
            clone.transform.position = hand_position.position + new Vector3(-0.132f,0.254f,0.253f);
            clone.GetComponent<Rigidbody>().AddForce(new Vector3(-3,2,-0.5f)* power);
            Invoke("Throw_sound",0.7f);
            return;

            case 2:
            //右丟動畫
            clone.transform.position = hand_position.position + new Vector3(0.132f,0.254f,0.253f);
            clone.GetComponent<Rigidbody>().AddForce(new Vector3(3,2,-0.5f)* power);
            Invoke("Throw_sound",0.7f);
            return;

            case 3:
            //上丟動畫
            clone.transform.position = hand_position.position+ new Vector3(0,0.254f,0.253f);
            clone.GetComponent<Rigidbody>().AddForce(new Vector3(0,2,3)* power);
            Invoke("Throw_sound",0.7f);
            return;

            case 4:
            //下丟動畫
            clone.transform.position = hand_position.position + new Vector3(0,0.254f,0.25f);
            clone.GetComponent<Rigidbody>().AddForce(new Vector3(0,2,-3)* power);
            Invoke("Throw_sound",0.7f);
            return;
        }
        
    }
    void MoveAble(){
        GetComponent<PLAYER_MOVE>().move_able = true;
        GetComponent<Collider>().enabled = true;
    }

    public void LoadPrefabAndImage(int id){
        //item_throw_image.sharedMaterial = Resources.Load<Material>("Image/Item/Materials/" + id.ToString());
        //切換拿的東西
        throw_item = Resources.Load<GameObject>("Lv1_prefab/"+id.ToString());
        //讀取拿的東西
    }
}
