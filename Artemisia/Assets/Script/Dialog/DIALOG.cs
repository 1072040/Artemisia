using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;

public class DIALOG : MonoBehaviour
{  
    public GameObject dialog_img; //對話框圖片
    public GameObject dialog_text;//對話
    public GameObject name_text;//說話者名字
    public GameObject headswap_img;//立繪圖片
    public GameObject end_img; //
    
    public GameObject dialog_text_mid;
    public GameObject main_dialog_text;
    public GameObject[] three_option_button = new GameObject[3]; //選項BUTTON
    public GameObject[] three_option_text = new GameObject[3];  //選項TEXT

    public GameObject[] two_option_button = new GameObject[2]; //選項BUTTON
    public GameObject[] two_option_text = new GameObject[2];  //選項TEXT

    //--------------------------------------------------------------------------
    int crrent_dialog = 0;   //目前對話索引
    int all_dialog_count;   //所有對話array數量
    private float chars_per_second = 0.1f;//打字特效速度
    private float timer; //计时器
    private int current_pos = 0; //當前打字位置
    private bool is_finished = false; //該句對話是否打完
    public string words;      //當下要顯示的所有內容
    public bool dialoging = false; //對話是否正在進行
    private bool making_selection = false; //進行選擇中
    private int operating_status;  //該句結束之後的操作狀態
    private int now_level;            //這段對話的level
    private int now_state;            //這段對話的state
    private int now_game_object;    //這段對話的觸發物件
    private int[] option_state = new int[3];            //選項選擇後回傳的state

    public AudioSource check_sound;
    public AudioSource switch_sound;
    public GameObject player;

     void Update()
    {
       // print("three_option_button[0].activeSelf:"+three_option_button[0].activeSelf);
        //若對話框存在不可移動角色
        if(dialoging == false){
            return;
           
        }else{
            player.GetComponent<PLAYER_MOVE>().move_able = false;
        }
        
        OnStartWriter();
      
        if(Input.GetKeyDown(KeyCode.Space)){
            check_sound.Play();
            NextDialogue();
        }
        else if(making_selection == false) {
            return;
        }
        else if(Input.GetKeyDown(KeyCode.E)){
            NextDialogue();
        }

        
    }
    /// <summary>
    /// 開啟對話呼叫function
    /// </summary>
     public void StartDialog(int game_object,int level,int state){
        //判斷傳入參數是否正確，若有參數傳入錯誤PUBLIC_VALUE.current_dialog_list為null,同時避免對話重複開啟
       if(state >= 0 && dialoging == false){
            now_game_object = game_object;
            now_level = level;
            now_state = state;
            switch (level){
                case 0:
                    PUBLIC_VALUE.all_json_list.current_dialog_list = PUBLIC_VALUE.all_json_list.dialogs_0;
                    FindDialog(game_object,level,state);
                    break;
                case 1:
                    PUBLIC_VALUE.all_json_list.current_dialog_list = PUBLIC_VALUE.all_json_list.dialogs_1;
                    FindDialog(game_object,level,state);
                    break;
                case 2:
                    //PUBLIC_VALUE.current_dialog_list = PUBLIC_VALUE.dialog_list_level_1;
                    PUBLIC_VALUE.all_json_list.current_dialog_list = PUBLIC_VALUE.all_json_list.dialogs_2;
                    FindDialog(game_object,level,state);
                    break;
                case 3:
                    PUBLIC_VALUE.all_json_list.current_dialog_list = PUBLIC_VALUE.all_json_list.dialogs_3;
                    FindDialog(game_object,level,state);
                    break;
                default:
                    PUBLIC_VALUE.all_json_list.current_dialog_list = null;
                    break;
            }
        }
     } 
    private void FindDialog(int game_object,int level,int state){
        crrent_dialog = 0;
        print("FindDialog");
        print("game_object："+game_object);
        print("state："+state);
        //all_dialog_count = PUBLIC_VALUE.current_dialog_list.dialogs.Length-1;
        all_dialog_count = PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs.Count-1;
        print("all_dialog_count："+all_dialog_count);
        for(crrent_dialog = 0;crrent_dialog <= all_dialog_count;crrent_dialog++){
            // if(PUBLIC_VALUE.current_dialog_list.dialogs[crrent_dialog].Object == game_object
            // && PUBLIC_VALUE.current_dialog_list.dialogs[crrent_dialog].state == state){
                if(PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs[crrent_dialog].Object == game_object
            && PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs[crrent_dialog].state == state){
                print("find");
                is_finished = false;
                DialogFirstShow();
                break;
            }
        }
   }
    /// <summary>
    /// 該次對話的第一句
    /// </summary>
   private void DialogFirstShow(){
            print("DialogFirstShow");
            SetDialiogShowInfo();
            //顯示對話圖片
            dialog_img.SetActive(true);
            //headswap_img.SetActive(true);
            //對話進行中
            dialoging = true;
            making_selection = false;
    }
    /// <summary>
    /// 判斷該句對話完成之後要進行的操作
    /// </summary>
    private void NextDialogue(){
        //operating_status = PUBLIC_VALUE.current_dialog_list.dialogs[crrent_dialog].operating_status;    //該句對話結束之後要進行的操作代號
        operating_status = PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs[crrent_dialog].operating_status; 

        //if(making_selection == true) return;
        //若該句對話尚未結束(打字特效未結束)
        if(is_finished == false){   
//            print("not enter switch");
            SentenceFinish();
        }
        else if(making_selection == false){
            //對話結束
            switch(operating_status){
                case 0:
                    crrent_dialog++;
                    //確認無溢出錯誤
                    if(crrent_dialog <= all_dialog_count){
                        SetDialiogShowInfo();  
                    }else{
                        DialogEnd();   //若有溢出錯誤強制對話結束;
                    }
                    break;
                //該次對話結束
                case 1:
                    DialogEnd();
                    break;
                //該句對話結束後要有選項
                case 2:
                    ShowOption();
                    break;
            }  
        }
        else{
            print("甚麼都沒做");
        }
    }
    /// <summary>
    /// 設定對話顯示資訊
    /// </summary>
    private void SetDialiogShowInfo(){
        //取得該劇對話結束之後要進行的操作
        //operating_status = PUBLIC_VALUE.current_dialog_list.dialogs[crrent_dialog].operating_status;
        operating_status = PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs[crrent_dialog].operating_status;
        making_selection = (operating_status == 2) ? true : false;
        // making_selection = false; 
        // if(operating_status == 2){
        //     making_selection = true;
        //     // name_text.SetActive(false);
        //     // dialog_text.SetActive(false);
        // }

        // int speak_id = PUBLIC_VALUE.current_dialog_list.dialogs[crrent_dialog].figure_id;
        // int img_index = PUBLIC_VALUE.current_dialog_list.dialogs[crrent_dialog].img_index;

        int speak_id = PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs[crrent_dialog].figure_id;
        int img_index = PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs[crrent_dialog].img_index;
        //取得說話者資料
        List<string> speak_info = new List<string>(2);
        //speak_info = GetComponent<JSON_OPERATOR>().GetDialogFigureInfo(speak_id);
        main_dialog_text = dialog_text;
        if(speak_id == 0){
            //print("0000");
           headswap_img.SetActive(false);
           dialog_text.GetComponent<Text>().text = "";
           main_dialog_text = dialog_text_mid;
           name_text.GetComponent<Text>().text = "";
           
        }
        else{
            speak_info = PUBLIC_VALUE.all_json_list.GetDialogFigureInfo(speak_id);
            //設定立繪圖片(由)
            headswap_img.SetActive(true);
            headswap_img.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Image/Dialogue/"+speak_info[1])[img_index];
            name_text.GetComponent<Text>().text = speak_info[0];
        }
        //設定顯示內容
        //words = PUBLIC_VALUE.current_dialog_list.dialogs[crrent_dialog].content;
        words = PUBLIC_VALUE.all_json_list.current_dialog_list.dialogs[crrent_dialog].content;
        
        is_finished = false;  
    }

    /// <summary>
    /// 顯示選項
    /// </summary>
    private void ShowOption(){
        print("ShowOption");
        making_selection = true;
        // name_text.SetActive(false);
        // dialog_text.SetActive(false);
        name_text.GetComponent<Text>().text="";
        main_dialog_text.GetComponent<Text>().text="";
        // dialog_text.GetComponent<Text>().text="";
        // dialog_text_mid.GetComponent<Text>().text="";
        StopAllCoroutines();
        end_img.SetActive(false);
        int count = 0;
        for(int i = 0;i < PUBLIC_VALUE.all_json_list.options_list.options.Length;i++){
              
            if(PUBLIC_VALUE.all_json_list.options_list.options[i].level == now_level && PUBLIC_VALUE.all_json_list.options_list.options[i].state == now_state){
                if(PUBLIC_VALUE.all_json_list.options_list.options[i].count == 3){
                    int img_index = PUBLIC_VALUE.all_json_list.options_list.options[i].img_index;
                    List<string> speak_info = new List<string>(2);
                    speak_info = PUBLIC_VALUE.all_json_list.GetDialogFigureInfo(PUBLIC_VALUE.all_json_list.options_list.options[i].figure_id);


                    headswap_img.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Image/Dialogue/"+speak_info[1])[img_index];
                    three_option_text[count].GetComponent<Text>().text = PUBLIC_VALUE.all_json_list.options_list.options[i].content;

                    option_state[count] = PUBLIC_VALUE.all_json_list.options_list.options[i].return_state;
                    // print(i);
                    three_option_button[count].SetActive(true);
                    three_option_button[0].GetComponent<Button>().Select();
                    count++;
                }
                else if(PUBLIC_VALUE.all_json_list.options_list.options[i].count == 2){
                    int img_index = PUBLIC_VALUE.all_json_list.options_list.options[i].img_index;
                    List<string> speak_info = new List<string>(2);
                    speak_info = PUBLIC_VALUE.all_json_list.GetDialogFigureInfo(PUBLIC_VALUE.all_json_list.options_list.options[i].figure_id);


                    headswap_img.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Image/Dialogue/"+speak_info[1])[img_index];
                    two_option_text[count].GetComponent<Text>().text = PUBLIC_VALUE.all_json_list.options_list.options[i].content;

                    option_state[count] = PUBLIC_VALUE.all_json_list.options_list.options[i].return_state;
                    // print(i);
                    two_option_button[count].SetActive(true);
                    two_option_button[0].GetComponent<Button>().Select();
                    count++;
                }
            }
        }
    }


    /// <summary>
    /// 該次對話已完成
    /// </summary>
    private void DialogEnd(){
        dialoging = false;
        //顯示對話圖片
        end_img.SetActive(false);
        dialog_img.SetActive(false);
        headswap_img.SetActive(false);
        //設定說話者名字
        name_text.GetComponent<Text>().text = "";
        //設定顯示內容
        main_dialog_text.GetComponent<Text>().text="";
        // dialog_text.GetComponent<Text>().text = "";
        // dialog_text_mid.GetComponent<Text>().text = "";
        crrent_dialog = 0;
        is_finished = false;

         player.GetComponent<PLAYER_MOVE>().move_able = true;
    }

    /// <summary>
    /// 執行打字機效果
    /// </summary>
    private void OnStartWriter()
    {
        timer += Time.deltaTime;
        if(is_finished == true || timer < chars_per_second) return;

        timer = 0f;
        current_pos++;
        //重新整理文字顯示內容
        main_dialog_text.GetComponent<Text>().text = words.Substring(0, current_pos);
        //.GetComponent<Text>().text = words.Substring(0, current_pos);
        if (current_pos >= words.Length){
            SentenceFinish();
        }
    }

    /// <summary>
    /// 結束該句打字，初始化資料(該次對話未完成)
    /// </summary>
    private void SentenceFinish()
    {
        timer = 0;
        current_pos = 0;
        is_finished = true;
        main_dialog_text.GetComponent<Text>().text = words;
        //dialog_text.GetComponent<Text>().text = words;

        if(operating_status != 2) StartCoroutine(EndImageControl());
        else ShowOption();
     
    }

    /// <summary>
    /// 結束該句對話後三角型閃爍
    /// </summary>
     IEnumerator EndImageControl(){
        end_img.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        end_img.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if(is_finished == true){
            // print("making_selection:"+making_selection);
            StartCoroutine(EndImageControl());
        }
        else{
            StopAllCoroutines();
        }
    }

    //選項CLICK事件
    public void ClickOption0(){
        now_state = option_state[0];
        print("now_state："+now_state);
        three_option_button[0].SetActive(false);
        three_option_button[1].SetActive(false);
        three_option_button[2].SetActive(false);
        two_option_button[0].SetActive(false);
        two_option_button[1].SetActive(false);
        check_sound.Play();
        // name_text.SetActive(false);
        // dialog_text.SetActive(false);
        FindDialog(now_game_object,now_level,now_state);
        
    }
    //選項CLICK事件
    public void ClickOption1(){
        now_state = option_state[1];
        print("now_state："+now_state);
        three_option_button[0].SetActive(false);
        three_option_button[1].SetActive(false);
         three_option_button[2].SetActive(false);
          two_option_button[0].SetActive(false);
        two_option_button[1].SetActive(false);
        check_sound.Play();
        FindDialog(now_game_object,now_level,now_state);
        //  name_text.SetActive(false);
        //  dialog_text.SetActive(false);
    }
    //選項CLICK事件
     public void ClickOption2(){
        now_state = option_state[2];
       print("now_state："+now_state);
        three_option_button[0].SetActive(false);
        three_option_button[1].SetActive(false);
        three_option_button[2].SetActive(false);
         two_option_button[0].SetActive(false);
        two_option_button[1].SetActive(false);
        check_sound.Play();
        FindDialog(now_game_object,now_level,now_state);
        //  name_text.SetActive(false);
        //  dialog_text.SetActive(false);
    }

    public void PlaySwitchSound(){
        switch_sound.Play();
    }


}
