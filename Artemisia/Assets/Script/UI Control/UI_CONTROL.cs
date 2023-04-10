using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CONTROL : MonoBehaviour
{
    public GameObject menu_panel;
    public GameObject bag_panel;
    public GameObject map;
    public GameObject dig_tips;

    public bool menuisOpen;
    public bool bagisOpen;
    public bool dialogueisOpen;
    public bool mapisOpen;
    public bool uiisOpen;
    private bool flag;
    public GameObject Eventsystem;
    GameObject player;
    public Animator player_anim;
    int bag_count;

    void Start()
    {
        bagisOpen = false;
        menuisOpen = false;
        mapisOpen = false;
        Eventsystem = GameObject.Find("EventSystem");
        player = GameObject.Find("Player");
        player_anim = player.GetComponent<PLAYER_MOVE>().player_anim;
    }
    void Update()
    {
        dialogueisOpen = Eventsystem.GetComponent<DIALOG>().dialoging;
        dig_tips.SetActive(player.GetComponent<COMBO>().isComboing);
        if (bagisOpen || menuisOpen || dialogueisOpen || mapisOpen)
        {
            uiisOpen = true;
            player.GetComponent<PLAYER_MOVE>().move_able = false;
            player_anim.SetBool("is_moving", false);
            flag = true;
        }
        else if (flag)
        {
            uiisOpen = false;
            player.GetComponent<PLAYER_MOVE>().move_able = true;
            flag = false;
        }

        if (!menuisOpen)
        {
            //menu panel控制
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(bagisOpen){
                    bag_panel.SetActive(false);
                    bagisOpen = false;
                }
                else if(mapisOpen){
                    map.SetActive(false);
                    mapisOpen = false;
                }
                else{
                    menu_panel.SetActive(true);
                    menu_panel.GetComponent<MENU_UI_CONTROL>().OpenMenu();
                    menuisOpen = true;
                }
                
            }
            if (!dialogueisOpen)
            {
                if (Input.GetKeyDown(KeyCode.B) && !mapisOpen)
                {
                    bag_count = PUBLIC_VALUE.tool_operator.GetOwnedTool().Count;
                    if (!bagisOpen)
                    {
                        bag_panel.SetActive(true);
                        bag_panel.GetComponent<BAG_UI_CONTROL>().OpenBag();
                        bagisOpen = true;
                    }
                    else
                    {
                        bag_panel.SetActive(false);
                        bagisOpen = false;
                    }
                }
                if (Input.GetKeyDown(KeyCode.M) && !bagisOpen)
                {
                    map.SetActive(!map.activeSelf);
                    mapisOpen = !mapisOpen;
                }
            }


        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && menu_panel.GetComponent<MENU_UI_CONTROL>().Guide_page == false)
            {
                menu_panel.SetActive(false);
                menuisOpen = false;
            }
        }
    }
}
