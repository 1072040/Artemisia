using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class COMBO : MonoBehaviour
{
    public int combo_value;//連擊數
    public GameObject combo_object; //正在互動的combo物件
    public bool isComboing;
    public bool firstCombo;
    private GameObject player;
    public Animator player_anim;

    private void Start() {
        isComboing = false;
        player = GameObject.Find("Player");
    }
    public void Active(int id){
        if(id > 400 && id < 499 && PUBLIC_VALUE.tool_operator.GetToolState(5,1) == true){
            player_anim.SetBool("dig_mode",true);
            isComboing = true;
            //開啟連打狀態iscomboing
            combo_value = combo_object.GetComponent<OBJECT_INFO>().combo_value;
            Debug.Log(combo_value);
        }
    }

    private void Update() {
        if(isComboing){
            if(Input.GetKeyDown(KeyCode.Space)){//按下空白減去combo value
                player_anim.SetBool("is_digging",true);
                combo_value--;
                Debug.Log(combo_value);
                Invoke("DigRest",0.79f);
            }else if(Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.Space)) && !(Input.GetKeyDown(KeyCode.E)))
            {
                //傳回combo_value
                player_anim.SetBool("is_digging",false);
                ReturnComboValue(combo_value);
            }
            if(combo_value == 0){
                player_anim.SetBool("is_digging",false);
                ComboFinished();
            }
        }
    }

    public void ReturnComboValue(int combo_value){
        combo_object.GetComponent<OBJECT_INFO>().combo_value = combo_value;
        player_anim.SetBool("dig_mode",false);
        isComboing = false;
    }
    
    public void ComboFinished(){
        combo_object.GetComponent<OBJECT_INFO>().combo_value = 0;
        player_anim.SetBool("dig_mode",false);
        isComboing = false;
        player.GetComponent<PLAYER_INTERACTIVE>().interactive_id = 0;
        Debug.Log("ocmbo is done");
    }

    public void DigRest(){
        player_anim.SetBool("is_digging",false);
    }
}
