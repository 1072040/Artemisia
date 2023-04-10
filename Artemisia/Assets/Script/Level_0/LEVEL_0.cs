using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LEVEL_0 : MonoBehaviour
{

    private GameObject Player;
    private GameObject eventsystem;
    private GameObject Bag_UI;

    public bool Check_dialog_1 = false;
    public bool Check_dialog_2 = false;

    public int Status;
    void Start()
    {
        Player = GameObject.Find("Player");
        eventsystem = GameObject.Find("EventSystem");
        Bag_UI = GameObject.Find("All_UI");
        Status = 0;
    }

    void Update()
    {
        if (Player.GetComponent<PLAYER_INTERACTIVE>().current_area != 0)
        {
            return;
        }
        if (PUBLIC_VALUE.status_operator.GetState(0) == 1)
        {
            return;
        }
        switch (Status)
        {
            case 0:
                //對話WASD移動
                if (Check_dialog_1 == false)
                {
                    eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 5);
                    Check_dialog_1 = true;
                }
                break;
            case 1:
                //對話SHIFT奔跑
                if (Check_dialog_2 == false)
                {
                    eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 6);
                    Check_dialog_2 = true;
                }
                break;
            case 2:
                if (Player.GetComponent<PLAYER_INTERACTIVE>().interactive_id == 108)
                {
                    eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 8);
                    Status = 3;
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    print("Status:" + Status);
                    print(PUBLIC_VALUE.tool_operator.GetToolState(30, 0));
                    Status = 4;
                    
                }
                break;
            case 4:
                if (PUBLIC_VALUE.tool_operator.GetToolState(30, 0) == true)
                {
                    eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 9);
                    Status = 5;
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.B))
                {
                    //點擊背包的故事書閱讀
                    Status=6;
                }
                break;
            case 6:
                if (Bag_UI.GetComponent<UI_CONTROL>().bagisOpen==false)
                {
                    Status = 7;
                }
                break;
            case 7:
               eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 10);
                Status = 8;
                break;
            case 8:
                 if (Input.GetKey(KeyCode.M))
                {
                    PUBLIC_VALUE.status_operator.WriteState(0, 1);
                    PUBLIC_VALUE.tool_operator.DeleteTool(46);
                    Status = 9;
                }
                break;
        }

    }
}
