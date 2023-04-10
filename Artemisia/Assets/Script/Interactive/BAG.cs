using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BAG : MonoBehaviour
{
    public GameObject get_item_tip_image; //獲得道具提示
    public Image item_image;//道具提示圖片
    public Text item_name_text;//道具提示名稱
    /*--------------------------------------------------------------*/
    private GameObject player;
    Animator get_item_tip_anim;
    void Start(){
        player = GameObject.Find("Player");
        get_item_tip_anim = get_item_tip_image.GetComponent<Animator>();
    }
    
    //獲得道具呼叫
    public void GetItem(int id,int cid){
        int area = player.GetComponent<PLAYER_INTERACTIVE>().current_area;
        PUBLIC_VALUE.tool_operator.SetObtainTool(cid,area);
        PUBLIC_VALUE.generate_operator.SetNotGenerate(cid,area);
        OpenTip(id);
    }

    //使用道具呼叫
    public void UseItem(int id){
        PUBLIC_VALUE.tool_operator.DeleteTool(id);
    }

    //開啟提示，顯示名稱與圖片
    public void OpenTip(int id){
        //取得工具資訊
        PUBLIC_VALUE.tool tool = new PUBLIC_VALUE.tool();
        tool = PUBLIC_VALUE.tool_operator.GetToolInfo(id);
        //設定
        item_name_text.text = tool.name;
        item_image.GetComponent<Image>().sprite = Resources.Load<Sprite>(tool.img + "T");
        get_item_tip_anim.gameObject.SetActive(true);
    }
}

