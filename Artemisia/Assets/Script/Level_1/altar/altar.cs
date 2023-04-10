using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class altar : MonoBehaviour
{
    public GameObject gem;
    public GameObject elf;
    public GameObject smoke;
    public int gemCID;
    public bool finished = false;
    private GameObject player;
    private GameObject eventsystem;
    public AudioSource Alter_sound;
    void Start(){
        eventsystem = GameObject.Find("EventSystem");
        //player = GameObject.Find("Player");
    }
    void Awake()
    {
        if (PUBLIC_VALUE.generate_operator.GetGenerateState(this.GetComponent<OBJECT_INFO>().cid, 1) == 1)
        {
            finished = true;
            gem.SetActive(true);//把寶石放上去
        }
    }
    void Update()
    {
        // if (!Input.GetKeyDown(KeyCode.E))
        // {
        //     return;
        // }

        // //按下E鍵之後

        // if (player == null)
        // {
        //     return;
        // }

        // //玩家在附近

        // if (!PUBLIC_VALUE.tool_operator.GetToolState(gemCID))
        // {
        //     if (level1.GetComponent<LEVEL_1>().elf.activeSelf)
        //     {
        //         //如果玩家身上沒有寶石，且精靈已經出現
        //         //對話：...不可以拿走寶石...
        //          print("寶石已經放上去");
        //     }
        //     else
        //     {
        //         //如果玩家身上沒有寶石，且精靈還沒有出現
        //         eventsystem.GetComponent<DIALOG>().StartDialog(0,2,7);
        //     }
        //     return;
        // }

        // //如果玩家身上有寶石
        // PUBLIC_VALUE.tool_operator.DeleteTool(gemCID);//刪掉寶石
        // finished = true;
        // gem.SetActive(true);//把寶石放上去
    }
    void OnTriggerStay(Collider other)
    {
          if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        //按下E鍵之後

        if (player == null)
        {
            return;
        }
       // if(Input.GetKeyDown(KeyCode.E) && other.tag == "Player"){
            print("符合條件");
            //玩家在附近
            if (!PUBLIC_VALUE.tool_operator.GetToolState(gemCID,1))
            {
                //if (level1.GetComponent<LEVEL_1>().elf.activeSelf)
                if(PUBLIC_VALUE.generate_operator.GetGenerate(37,1) == true && PUBLIC_VALUE.status_operator.GetState(1)==4)
                {
                    //如果玩家身上沒有寶石，且精靈已經出現
                    eventsystem.GetComponent<DIALOG>().StartDialog(0,1,20);
                }
                else if(this.gameObject.GetComponent<OBJECT_INFO>().id <= -2 && this.gameObject.GetComponent<OBJECT_INFO>().id >= -4 && !finished)
                {
                    //如果玩家身上沒有寶石，且精靈還沒有出現
                    //對話：...可以放東西的樣子...
                     eventsystem.GetComponent<DIALOG>().StartDialog(0,1,7);
                }
                return;
            }
            else{
                //如果玩家身上有寶石
                PUBLIC_VALUE.tool_operator.DeleteTool(gemCID);//刪掉寶石

                finished = true;
                gem.SetActive(true);//把寶石放上去
                Alter_sound.Play();
                int id = this.gameObject.GetComponent<OBJECT_INFO>().id;
                switch(id){
                    case -2:
                        //this.gameObject.GetComponent<OBJECT_INFO>().id = -8;
                        //this.gameObject.GetComponent<OBJECT_INFO>().cid = 34;
                        //PUBLIC_VALUE.tool_operator.DeleteTool(8);
                        PUBLIC_VALUE.generate_operator.SetGenerateState(8,1,1);
                        //設定寶石不生成
                        PUBLIC_VALUE.generate_operator.SetNotGenerate(102,1);
                        break;
                    case -3:
                        //this.gameObject.GetComponent<OBJECT_INFO>().id = -9;
                        //this.gameObject.GetComponent<OBJECT_INFO>().cid = 35;
                        // PUBLIC_VALUE.tool_operator.DeleteTool(9);
                        // PUBLIC_VALUE.tool_operator.setToolGameobject(104,"");

                        PUBLIC_VALUE.generate_operator.SetGenerateState(9,1,1);
                        //設定寶石不生成
                        PUBLIC_VALUE.generate_operator.SetNotGenerate(104,1);
                        break;
                    case -4:
                        //this.gameObject.GetComponent<OBJECT_INFO>().id = -10;
                        //this.gameObject.GetComponent<OBJECT_INFO>().cid = 36;
                        // PUBLIC_VALUE.tool_operator.DeleteTool(10);
                        //  PUBLIC_VALUE.tool_operator.setToolGameobject(103,"");

                         PUBLIC_VALUE.generate_operator.SetGenerateState(10,1,1);
                        //設定寶石不生成
                        PUBLIC_VALUE.generate_operator.SetNotGenerate(103,1);
                        break;
                    
                }
            for (int i = 8; i <= 10; i++)
            {
                //判斷的是有寶石的祭壇
                if (PUBLIC_VALUE.generate_operator.GetGenerateState(i, 1) == 0)
                {
                    return;
                }
            }
            if (!PUBLIC_VALUE.generate_operator.GetGenerate(37, 1))
            {
                PUBLIC_VALUE.generate_operator.SetGenerate(37, 1);
            }

            if (PUBLIC_VALUE.generate_operator.GetGenerate(37, 1) == true)
            {
                eventsystem.GetComponent<GENERATE>().GenerateOnce(37, 1);
                //PUBLIC_VALUE.tool_operator.DeleteTool(37);
                elf = GameObject.Find("ELF(Clone)");
                Instantiate(smoke, elf.transform);
                smoke.transform.localPosition = new Vector3(0, 0, 0);
                elf.GetComponent<AudioSource>().Play();
                Debug.Log("AAAA");
                PUBLIC_VALUE.status_operator.WriteState(1, 3);
            }
            // else if (GameObject.Find("ELF(Clone)") != null)
            // {
            //     elf = GameObject.Find("ELF(Clone)");

            //     Debug.Log("YAYAYA");
            //     if (elf.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("elf_finished") && PUBLIC_VALUE.status_operator.GetState(1) != 4)
            //     {
            //         //Debug.Log("精靈出場動畫結束");
            //         //對話
            //         // Eventcontrol.GetComponent<DIALOG>().StartDialog(0, 1, 5);
            //         // //獲得防黏靴
            //         // StartCoroutine(GetShoe());
            //         //get_shoes_animation();
            //         //拿鞋子動畫播完
            //         Debug.Log("yayaya");

            //         PUBLIC_VALUE.status_operator.WriteState(1, 3);
            //     }
            // }
        }
       // }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        //如果碰到的是玩家

        player = other.gameObject;
        //player.GetComponent<PLAYER_INTERACTIVE>().tip_image.SetActive(true);//打開UI
        if (PUBLIC_VALUE.tool_operator.GetToolState(gemCID,1))
        {
            print("有寶石");
            //player.GetComponent<PLAYER_INTERACTIVE>().tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/hand");
            player.GetComponent<PLAYER_INTERACTIVE>().OpenTip(2);
        }
        else if(!finished)
        {
            print("沒寶石");
            //player.GetComponent<PLAYER_INTERACTIVE>().tip_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Tip/dialogue");
            player.GetComponent<PLAYER_INTERACTIVE>().OpenTip(1);
        }
        else if(PUBLIC_VALUE.generate_operator.GetGenerate(37, 1))
        {
            player.GetComponent<PLAYER_INTERACTIVE>().OpenTip(1);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        player.GetComponent<PLAYER_INTERACTIVE>().CloseTip();//關掉UI
        player = null;
    }
    
}
