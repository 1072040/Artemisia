using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEVEL_1 : MonoBehaviour
{
    public GameObject[] altar = new GameObject[3];

    public bool two_gem = false;
    private bool yellow = false, blue = false;

    public bool first_monster = false;
    private GameObject player;
    private GameObject Eventcontrol;
    // Start is called before the first frame update
    public int state = 0;
    void Start()
    {
        player = GameObject.Find("Player");
        Eventcontrol =GameObject.Find("EventSystem");


    }
    void Altar()
    {
        // for (int i = 0; i < altar.Length; i++)
        // {
        //     if ( altar[i].GetComponentInChildren<altar>().finished == false)
        //     {
        //         return;
        //     }
        // }
        for(int i = 8;i <= 10;i++) {
            //判斷的是有寶石的祭壇
            if(PUBLIC_VALUE.generate_operator.GetGenerateState(i,1) == 0){
                return;
            }
        }
        // elf.SetActive(true);
        
    }
    // IEnumerator GetShoe(){
    //    if(Eventcontrol.GetComponent<DIALOG>().words == "我可以給妳一樣好東西"){
    //        Eventcontrol.GetComponent<BAG>().GetItem(105,4);
    //        StopAllCoroutines();
    //    }else{
    //        yield return new WaitForSeconds(0.01f);
    //        StartCoroutine(GetShoe());
    //    }
    // }
    void get_shoes_animation()
    {
        player.GetComponent<PLAYER_MOVE>().player_anim.SetTrigger("get_shoes");
        player.GetComponent<PLAYER_MOVE>().move_able = false;
    }
    bool check_two_gem()
    {
        if(PUBLIC_VALUE.tool_operator.GetToolState(2,1) == true)
        {
            blue = true;
        }
        if (PUBLIC_VALUE.tool_operator.GetToolState(3,1) == true)
        {
            yellow = true;
        }
        return yellow && blue;
    }
    // Update is called once per frame
    void first_monster_animation()
    {
        if (!first_monster)
        {
            return;
        }

        if (player.GetComponent<HIDE_IN_TREE>().is_hide)
        {
            return;
        }

        player.GetComponent<PLAYER_MOVE>().player_anim.SetTrigger("first_monster");
        player.GetComponent<PLAYER_MOVE>().move_able = false;

        if (!player.GetComponent<PLAYER_MOVE>().player_anim.GetCurrentAnimatorStateInfo(0).IsName("first_monster_finished"))
        {
            return;
        }

        first_monster = false;
        player.GetComponent<PLAYER_MOVE>().player_anim.ResetTrigger("first_monster");
        player.GetComponent<PLAYER_MOVE>().move_able = true;
        Debug.Log("出樹洞動畫播完");

        //對話
        Eventcontrol.GetComponent<DIALOG>().StartDialog(0,1,9);
        //設定狀態
        PUBLIC_VALUE.status_operator.WriteState(1,1);
        //刪除第一支怪物生成json資料
        PUBLIC_VALUE.tool_operator.DeleteTool(31);
    }
    void Update()
    {
        Altar();
        two_gem = check_two_gem();
        if(PUBLIC_VALUE.status_operator.GetState(1) == 0){
            first_monster_animation();
        }
       else if(PUBLIC_VALUE.status_operator.GetState(1) == 3 && Eventcontrol.GetComponent<DIALOG>().words == "我可以給妳一樣好東西"){
           Eventcontrol.GetComponent<BAG>().GetItem(105,4);
           PUBLIC_VALUE.status_operator.WriteState(1, 4);
        }


        if (!player.GetComponent<PLAYER_MOVE>().player_anim.GetCurrentAnimatorStateInfo(0).IsName("get_shoes_finished"))
        {
            return;
        }
        player.GetComponent<PLAYER_MOVE>().player_anim.ResetTrigger("get_shoes");
        player.GetComponent<PLAYER_MOVE>().move_able = true;
    }
}
