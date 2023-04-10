using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading : MonoBehaviour
{
    public GameObject loading_panel;
    public GameObject loading_text_0;
    public GameObject loading_text_1;
    public GameObject loading_text_2;
    public GameObject loading_text_3;
    public GameObject player;
    int timecount,dead_timecount;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player");
        player.GetComponent<PLAYER_MOVE>().move_able=false;
        timecount = 0;
        dead_timecount=0;
        
        if(PUBLIC_VALUE.start_game == true){
            loading_panel.SetActive(true);
            StartCoroutine(loading1());
        }
        else{
            StartCoroutine(Dead_time());
        }
    }
    void Update()
    {
        if(dead_timecount==2)
        {
            //player.GetComponent<PLAYER_MOVE>().move_able=true;
        }
    }

    public IEnumerator Dead_time()
    {
        print("Dead_time");
        player.GetComponent<PLAYER_MOVE>().move_able = true;
        yield return new WaitForSeconds(1f);
        dead_timecount++;
        if(dead_timecount<2)
        {
            StartCoroutine(Dead_time());
        }
        else{
            StopAllCoroutines();
        }
    }
    public IEnumerator loading1(){
        loading_text_0.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        loading_text_1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        loading_text_2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        loading_text_3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        loading_text_0.SetActive(true);
        loading_text_1.SetActive(false);
        loading_text_2.SetActive(false);
        loading_text_3.SetActive(false);
        yield return new WaitForSeconds(0.5f);
       
        timecount++;

        if(timecount <= 2){
            // print("making_selection:"+making_selection);
            StartCoroutine(loading1());
        }
        else{
            StopAllCoroutines();
            PUBLIC_VALUE.start_game = false;
            loading_panel.SetActive(false);
            loading_text_0.SetActive(false);
            loading_text_1.SetActive(false);
            loading_text_2.SetActive(false);
            loading_text_3.SetActive(false);
            player.GetComponent<PLAYER_MOVE>().move_able=true;
        }
        
    }
}
