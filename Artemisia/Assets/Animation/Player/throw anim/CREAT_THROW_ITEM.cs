using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CREAT_THROW_ITEM : MonoBehaviour
{
    public GameObject throw_item;
    public void Active(){
        Instantiate(throw_item , transform.position , Quaternion.identity);//在位置上生成道具
        gameObject.SetActive(false);//關閉handposition
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if(hit.gameObject.tag == "ground"){//碰撞地面時觸發
            Active();
        }
    }
}
