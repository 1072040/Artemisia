using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PLAYER_INTERACTIVE : MonoBehaviour
{
    public GameObject intercative_object;//正在互動的物件
    public GameObject interactive_trigger;//正在互動的物件的trigger
    public int interactive_id = 0;//正在互動的道具id
    public int interactive_cid = 0;

    public int interactive_level = 0;
    public UnityEvent interactive_function = null;
    public int combo_value;//連擊數
    public GameObject EventSystem;
    public GameObject AllUI;
    public GameObject tip_image;
    public GameObject tip_image_BG;
    public int current_area;
    public int current_resurrection;

    void Start(){
        current_area = -100;
        
    }
    private void Update()
    {
        if (GetComponent<ON_HAND>().isHolding == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<ON_HAND>().PutOrThrow();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && interactive_id != 0 && EventSystem.GetComponent<DIALOG>().dialoging == false && AllUI.GetComponent<UI_CONTROL>().uiisOpen == false)
            {
                CloseTip();
               
                //對話
                if (interactive_id > 0 && interactive_id < 99)
                {
                    int state = PUBLIC_VALUE.status_operator.GetState(interactive_level);
                    EventSystem.GetComponent<DIALOG>().StartDialog(interactive_id, interactive_level, state);
                    //GetComponent<Open_Dialogue>().Start_dialog(interactive_id ,level , state);
                    Debug.Log("開啟對話");
                }
                else if(interactive_id > 100 && interactive_id < 199)
                {
                    if(PUBLIC_VALUE.tool_operator.GetToolState(interactive_cid,interactive_level) == false){
                        //撿起道具
                        Debug.Log("撿起道具" + interactive_id);
                        EventSystem.GetComponent<BAG>().GetItem(interactive_id,interactive_cid);
                        interactive_id = 0;//清空互動物品
                        intercative_object.SetActive(false);//關閉物件
                        // PUBLIC_VALUE.tool_operator.GetToolState(interactive_cid);
                        CallFunction();

                    }else{
                        //使用道具
                        EventSystem.GetComponent<BAG>().UseItem(interactive_cid);
                        Debug.Log("使用道具" + interactive_id);
                        interactive_id = 0;//清空互動物品
                        intercative_object.SetActive(false);//關閉物件
                        CallFunction();
                    }
                }
                // else if(interactive_id > -199 && interactive_id < -100){
                //         //觸發使用道具物件
                //         if(PUBLIC_VALUE.tool_operator.GetToolState(-(interactive_cid)) == true){
                //             EventSystem.GetComponent<BAG>().UseItem(-(interactive_cid));
                //             Debug.Log("使用道具" + (interactive_id));
                //             interactive_id = 0;//清空互動物品
                //             interactive_trigger.SetActive(false);//關閉碰撞體(無法再次互動)
                //             CallFunction();
                //         }
                // }
                else if (interactive_id > 200 && interactive_id < 299)
                {//躲藏點
                    GetComponent<HIDE_IN_TREE>().Active();
                    CallFunction();
                }
                else if (interactive_id > 300 && interactive_id < 399)
                {//手持道具
                    GetComponent<ON_HAND>().Active(interactive_id);
                    interactive_id = 0;//清空互動物品
                    Destroy(intercative_object);//關閉物件
                    Debug.Log("手持道具");
                    CallFunction();
                }
                else if (interactive_id > 400 && interactive_id < 499)
                {//Combo連打
                    GetComponent<COMBO>().Active(interactive_id);
                    Debug.Log("連打");
                    CallFunction();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<OBJECT_INFO>())
        {//判斷trigger要有object info
            intercative_object = other.gameObject.transform.parent.gameObject;
            interactive_trigger = other.gameObject;
            interactive_id = other.GetComponent<OBJECT_INFO>().id;
            interactive_cid = other.GetComponent<OBJECT_INFO>().cid;
            interactive_level = other.GetComponent<OBJECT_INFO>().level;
            interactive_function = other.GetComponent<OBJECT_INFO>().call_function;//取得物件互動後要呼叫的function
            
            if(other.tag == "ground"){
                if(other.GetComponent<OBJECT_INFO>().area!=current_area){
                    int last = current_area;
                    if(current_area <= 0)
                        last = other.GetComponent<OBJECT_INFO>().area;
                    current_area = other.GetComponent<OBJECT_INFO>().area;
                    StartCoroutine(EventSystem.GetComponent<GENERATE>().Generate(current_area,last));
                }
                 if(other.GetComponent<OBJECT_INFO>().resurrection!=current_resurrection){
                    int last = current_resurrection;
                    if(current_resurrection < 0)
                        last = other.GetComponent<OBJECT_INFO>().resurrection;
                    current_resurrection = other.GetComponent<OBJECT_INFO>().resurrection;
                    EventSystem.GetComponent<GENERATE>().Resurrection(current_resurrection);
                }
                

            }

            if (other.GetComponent<OBJECT_INFO>().id > 400 )
            {
                combo_value = other.GetComponent<OBJECT_INFO>().combo_value;
                GetComponent<COMBO>().combo_object = other.gameObject;
            }
            if(other.GetComponent<OBJECT_INFO>().id > 0) OpenTip();
        }
    }
    private void OnTriggerExit(Collider other)
        {
            //離開清空
            if (other.GetComponent<OBJECT_INFO>())
            {
                interactive_id = 0;
                intercative_object = null;
                interactive_function = null;//清空物件互動後要呼叫的function
            }
            CloseTip();
        }
    public void OpenTip(){
        tip_image_BG.SetActive(true);
        tip_image.SetActive(true);
        if (interactive_id > 0 && interactive_id < 99)
        {//對話
            //設定SPRITE為Dialogue_tip
            tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/dialogue");
        }
        else if (interactive_id > 100 && interactive_id < 199)
        {//道具
            tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/hand");
        }   
        else if (interactive_id > 200 && interactive_id < 299)
        {//躲藏點
            tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/exclamation_mark");
        }
        else if (interactive_id > 300 && interactive_id < 399)
        {//手持道具
            tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/hand");
        }
        else if (interactive_id > 400 && interactive_id < 499)
        {//Combo連打
            tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/exclamation_mark");
        }
    }

    public void OpenTip(int id){
        tip_image_BG.SetActive(true);
        tip_image.SetActive(true);
        switch (id){
            case 1:
                tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/dialogue");
                break;
            case 2:
                tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/hand");
                break;
            case 3:
                 tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/exclamation_mark");
                break;
        }
    }
    public void CloseTip(){
        tip_image_BG.SetActive(false);
        
    }

    public void CallFunction(){
         if(interactive_function != null){//互動後呼叫的function
            interactive_function.Invoke();
        }
    }
}
