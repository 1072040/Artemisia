using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BAG_UI_CONTROL : MonoBehaviour
{
    public Image item_image;
    public Text item_name;
    public Text item_information;

    bool can_pressE = false;
    public GameObject pressE_image;
    bool is_checking_up = false;
    int now_check_tool;
    int now_check_page;
    int now_max_page;
    public Image checkup_image;
    public GameObject checkup_panel ,checkup_tip1,left,right,Rpage,Lpage;
    public int[] id;//id編號
            //0-0 , 0-1 , 0-2
    public int[] max_page;//最多頁數
            //1-0 , 1-1 , 1-2
    public List<int[]> check_page = new List<int[]>();
    Animator BagUI_anim;
    public GameObject[] inventory_slots;

    public AudioSource audio_source;
    public AudioClip switch_chose,turn_page,check;
    //-----------------------------------------------------------------------------//
    int current_selected = 0;
    int bag_count = 0;                         //紀錄目前擁有的工具總數
    static public List<PUBLIC_VALUE.tool> owned_tool;            //儲存目前擁有的全部工具
    private void Start() {
        id = new int[]{108,109,110,111};
        max_page = new int[]{8,4,3,3};

        check_page.Add(id);
        check_page.Add(max_page);

        BagUI_anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();
    }
    public void OpenBag(){
        //取得目前有的全部工具

        owned_tool = PUBLIC_VALUE.tool_operator.GetOwnedTool();
        bag_count = owned_tool.Count;
        print("目前道具總數"+bag_count);
        int i = 0;
        foreach (var tool in owned_tool)
        {
            var sprite = Resources.Load<Sprite>(tool.img + "S");
            inventory_slots[i].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = sprite;
            inventory_slots[i].transform.GetChild(2).gameObject.GetComponent<Image>().color = Color.white;
            i++;
        }
    
        for(i = bag_count ; i<inventory_slots.Length ; i++){
            inventory_slots[i].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = null;
            inventory_slots[i].transform.GetChild(2).gameObject.GetComponent<Image>().color = Color.clear;
        }

        inventory_slots[0].transform.GetChild(1).gameObject.SetActive(true);
        current_selected = 0;

        if(bag_count == 0){
            item_image.GetComponent<Image>().color = Color.clear;
            item_name.text = null;
            item_information.text = null;
        }

        checkup_panel.SetActive(false);//關閉放大檢視面版
    }

    private void Update() {
        
        if(bag_count > 0){
            PressECheckUp();
            if(!is_checking_up){
                SelectItem();
                ShowSelectedInfo();
            }else{
                CheckUpControl();
            }
        }
    }

    public void SelectItem(){
        //wasd控制選擇的道具
        if(Input.GetKeyDown(KeyCode.D)){
            if(current_selected+1<bag_count)
            current_selected +=1;
            audio_source.PlayOneShot(switch_chose);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            if(current_selected > 0)
            current_selected -=1;
            audio_source.PlayOneShot(switch_chose);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(bag_count > current_selected+3)
            current_selected +=3;
            audio_source.PlayOneShot(switch_chose);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            if(current_selected-3 >= 0)
            current_selected -=3;
            audio_source.PlayOneShot(switch_chose);
        }
        //全部換成未選擇的圖片
        for(int i = 0 ; i< inventory_slots.Length ;i++){
            inventory_slots[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        //將選擇的換成以選擇的圖片
        inventory_slots[current_selected].transform.GetChild(1).gameObject.SetActive(true);
    }

    public void ShowSelectedInfo(){
        //取得工具資料
        PUBLIC_VALUE.tool tool = new PUBLIC_VALUE.tool();
        tool = PUBLIC_VALUE.tool_operator.GetToolInfo(owned_tool[current_selected].id);
        item_name.text = tool.name;
        item_information.text = tool.introduce;
        item_image.GetComponent<Image>().color = Color.white;
        item_image.GetComponent<Image>().sprite = Resources.Load<Sprite>(tool.img+"L");

        //判斷是否能放大檢視，顯示提示
        if(tool.id >=108 && tool.id <=111){
            can_pressE = true;
            pressE_image.SetActive(can_pressE);
            now_check_tool = tool.id;
        }else{
            can_pressE = false;
            pressE_image.SetActive(can_pressE);
        }
    }

    public void PressECheckUp(){
        //開關放大檢視面板 checkup_panel 與檢視模式 is_checking_up
        if(can_pressE){
            if(Input.GetKeyDown(KeyCode.E)){
                audio_source.PlayOneShot(check);
                is_checking_up = !is_checking_up;
                checkup_panel.SetActive(is_checking_up);
                if(is_checking_up){
                    now_check_page = 1;//目前頁數重製
                    //開啟第一頁
                    checkup_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/CheckUp/"+ now_check_tool.ToString() + "/" + now_check_tool.ToString() + "-1");
                    //變更最大頁數
                    for(int i =0 ; i < 4 ;i++){
                        if(check_page[0][i] == now_check_tool){
                            now_max_page = check_page[1][i];
                        }
                    }
                    //Debug.Log(now_max_page);
                    if(now_max_page >1){
                        checkup_tip1.SetActive(true);
                        left.SetActive(false);
                    }else{
                        checkup_tip1.SetActive(false);
                        left.SetActive(false);
                        right.SetActive(false);
                    }
                    //切換翻頁書角
                    if(now_check_tool == 108 || now_check_tool == 109){
                        //開書頁
                        //Lpage.SetActive(true);
                        //Rpage.SetActive(true);
                        BagUI_anim.SetInteger("id",now_check_tool);

                    }else if(now_check_tool == 110 || now_check_tool == 111){
                        //關書頁
                        //Lpage.SetActive(false);
                        //Rpage.SetActive(false);
                        BagUI_anim.SetInteger("id",now_check_tool);
                    }
                }
                

            }
        }
    }

    public void CheckUpControl(){
        //控制檢視面板，之後再做翻頁
        if(now_max_page > 1){

            if(Input.GetKeyDown(KeyCode.D) && now_check_page < now_max_page){
                Debug.Log("右翻");
                audio_source.PlayOneShot(turn_page);
                now_check_page++;
                checkup_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/CheckUp/"+ now_check_tool.ToString() + "/" + now_check_tool.ToString() + "-" + now_check_page.ToString());
                //右翻動畫
                BagUI_anim.SetTrigger("pageR");
                //翻到最後一頁關閉右箭頭
                if(now_check_page == now_max_page)  right.SetActive(false);
                left.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.A) && now_check_page > 1){
                Debug.Log("左翻");
                audio_source.PlayOneShot(turn_page);
                now_check_page--;
                checkup_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/CheckUp/"+ now_check_tool.ToString() + "/" + now_check_tool.ToString() + "-" + now_check_page.ToString());
                //左翻動畫
                BagUI_anim.SetTrigger("pageL");
                //翻到第一頁關閉左箭頭
                if(now_check_page == 1)  left.SetActive(false);
                right.SetActive(true);
            }
        }
    }

}


