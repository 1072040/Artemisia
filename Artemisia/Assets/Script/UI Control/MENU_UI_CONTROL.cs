using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MENU_UI_CONTROL : MonoBehaviour
{
    public GameObject Guide_panel;
    public GameObject UI_Control;
    public bool Guide_page = false;
    private bool pause_page = false;
    public GameObject continue_btn,guide_btn;
   
    void Update()
    {
        if(Guide_page && Input.GetKeyDown(KeyCode.Escape)){
            Guide_panel.SetActive(false);
            Guide_page = false;
            EventSystem.current.SetSelectedGameObject(guide_btn);
        }
    }

    public void OpenMenu(){
        pause_page = true;
        EventSystem.current.SetSelectedGameObject(continue_btn);
    }

    public void Back(){
        //reloadsence.mainmenu
        SceneManager.LoadScene(0);
    }
    public void Continue(){
        UI_Control.GetComponent<UI_CONTROL>().menuisOpen = false;
        pause_page =false;
        gameObject.SetActive(false);
    }
    public void Guide(){
        Guide_page = true;
        Guide_panel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
