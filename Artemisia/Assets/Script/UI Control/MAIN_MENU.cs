using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class MAIN_MENU : MonoBehaviour
{
    public GameObject Menu;//主頁面CANVAS
    public GameObject Menu_continue; //選擇存檔點CANVAS
    public GameObject Guide_panel;
    public UnityEvent On_start; //主選單時讓第一個button亮
    public UnityEvent On_click; //選擇存檔點時讓第一個button亮
    public GameObject main_first_btn,guide_btn , record_first_btn;
    public Button[] record_button;
    public GameObject OP;
    
    public GameObject[] record_text;
    List<string> new_game = new List<string>();

    static public string now_select;


    private bool Menu_page = true; //判斷是不是在主畫面
    private bool Guide_page = false;
    
    void Start()
    {

        this.On_start.Invoke();//讓主選單第一個Button亮
        
        EventSystem.current.SetSelectedGameObject(main_first_btn);

        foreach (Button btn in record_button)
        {
            Button choice = btn; // need to store in separate variable to have the right button in the lamda expression
            btn.onClick.AddListener(() => ClickPlayGame(choice));
        }
    }
    void Update()
    {
        if (Menu_page == false && Input.GetKeyDown(KeyCode.Escape)) //判斷是否在主畫面和是否有案esc 有的話返回主畫面
        {
            Menu_continue.SetActive(false);
            //Menu.SetActive(true);
            Menu_page = true;
            this.On_start.Invoke();
            EventSystem.current.SetSelectedGameObject(main_first_btn);
        }
        if(Guide_page && Input.GetKeyDown(KeyCode.Escape)){
            Guide_panel.SetActive(false);
            Guide_page = false;
            EventSystem.current.SetSelectedGameObject(guide_btn);
        }
        if(Menu_page == false && Input.GetKeyDown(KeyCode.Backspace)){
            DelectPlayRecord();
        }
        if(Input.GetKeyDown(KeyCode.P)){
            OP_Loop();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }
    public void OP_Loop(){
        OP.SetActive(true);
        GetComponent<AudioSource>().mute = true;
        OP.GetComponent<Animator>().SetTrigger("op_loop");

    }
    public void New_game() //新遊戲
    {
        //jason重新遊戲
        SceneManager.LoadScene(0); //Load遊戲SCENCE
    }
    public void Continue_game() //繼續遊戲
    {
        Menu_continue.SetActive(true); //選擇存檔點CANVAS打開

        //讀取歷史遊玩資料紀錄並顯示
        ReadAndSetPlayHistory();
        EventSystem.current.SetSelectedGameObject(record_first_btn);

        //Menu.SetActive(false); //主頁面CANVAS關閉
        this.On_click.Invoke(); //選擇存檔點的第一個button亮
        Menu_page = false;
    }
    public void Guide(){
        Guide_page = true;
        Guide_panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void Exit() //離開遊戲
    {
        Application.Quit();
    }
    public void Back()
    {
        new_game.Clear();
        Menu_continue.SetActive(false);
        Menu.SetActive(true);
        Menu_page = true;
        this.On_start.Invoke();
        EventSystem.current.SetSelectedGameObject(main_first_btn);

    }
     //讀取歷史遊玩資料紀錄並顯示
    void ReadAndSetPlayHistory(){
        
        //print(PUBLIC_VALUE.record.record_list.records[0].play_time);
        int i = 0; 
        foreach (var record_item in PUBLIC_VALUE.record.record_list.records)
        {   
            //非新遊戲
            if(record_item.is_new == false){
               record_button[i].tag = record_item.id;
                string str = "";
                switch (record_item.progress){
                    case 1:
                        str+="教學關";
                        break;
                    case 2:
                        str+="第一關";
                        break;
                    case 3:
                        str+="第二關";
                        break;
                    case 4:
                        str+="第三關";
                        break;
                }
                //時間計算
                // TimeSpan ts = new TimeSpan(0, 0, record_item.play_time);
                // str+="      " +ts.Hours + "小时" + ts.Minutes + "分钟" + ts.Seconds + "秒";
                record_text[i].GetComponent<Text>().text = str;
                i++;
            }else{
                new_game.Add(record_item.id);
            }
        }

        foreach (var item in new_game)
        {
            record_button[i].tag = item;
            record_text[i].GetComponent<Text>().text = "新遊戲";
            i++;
        }
    }
   
   void ClickPlayGame(Button choice){
        print(choice.tag);
        PUBLIC_VALUE.start_game = true;
        bool is_new_game = false;
        if(new_game.IndexOf(choice.tag) != -1) is_new_game = true;
        PUBLIC_VALUE.all_json_list.StarPlayJson(choice.tag,is_new_game);
        int stste = PUBLIC_VALUE.status_operator.GetState(3);
        if(stste >= 1)
        {
            if(stste == 7) stste = 6;
             SceneManager.LoadScene(stste+2);
        }
        else{
            if(PUBLIC_VALUE.record.record_list.records[(Int32.Parse(choice.tag))-1].is_new == true){
                PUBLIC_VALUE.record.SetStartPlay(choice.tag);
                PlayLeadingAnimation();

            }else{
                SceneManager.LoadScene(2);
             }
        }
   }
   public void PlayLeadingAnimation(){
       //要播動畫
        OP.SetActive(true);
        GetComponent<AudioSource>().mute = true;
        print("播動畫");
   }
   public void SelectRecord1(){
       now_select = record_button[0].tag;
   }
   public void SelectRecord2(){
       now_select = record_button[1].tag;
   }
   public void SelectRecord3(){
       now_select = record_button[2].tag;
      
   }

   public void DelectPlayRecord(){
       PUBLIC_VALUE.record.delectRecord(now_select);
        SceneManager.LoadScene(0);
   }
   
    
    public void JumpTo4(){
         PUBLIC_VALUE.all_json_list.StarPlayJson("1",false);
         Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
        PUBLIC_VALUE.record.SetPlayerPosition(position);
         SceneManager.LoadScene(3);
    }
}
