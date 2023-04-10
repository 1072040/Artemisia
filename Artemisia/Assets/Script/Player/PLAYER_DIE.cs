using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAYER_DIE : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject diepanel;
    private GameObject player;
    private bool temp = true;
    private int play_time_count = 0;       //計算遊玩時長
    void Start()
    {
        player = GameObject.Find("Player");
        play_time_count = 0;
        StartCoroutine(CalPlayTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DIE_splattered(int scene_count)//濺血
    {
        if (temp)
        {
            //暫停遊玩時長計算
            StopAllCoroutines();
            //死亡次數+1
            PUBLIC_VALUE.record.AddDeathCount();
            //修改遊玩時長
            PUBLIC_VALUE.record.ChangePlayerTime(play_time_count);
            PUBLIC_VALUE.record.WriteRecordJson("0");


            diepanel.SetActive(true);
            diepanel.GetComponent<Animator>().SetInteger("die", 1);
            SceneManager.LoadScene(scene_count);
            temp = false;
        }
    }
    public void DIE_burst(int scene_count)//爆血
    {
        if (temp)
        {
            //暫停遊玩時長計算
            StopAllCoroutines();
            //死亡次數+1
            PUBLIC_VALUE.record.AddDeathCount();
            //修改遊玩時長
            PUBLIC_VALUE.record.ChangePlayerTime(play_time_count);
            PUBLIC_VALUE.record.WriteRecordJson("0");

            diepanel.SetActive(true);
            diepanel.GetComponent<Animator>().SetInteger("die", 0);
            SceneManager.LoadScene(scene_count);
            temp = false;
        }
    }

    //計匴遊玩時長
     IEnumerator CalPlayTime(){
        play_time_count++;
        yield return new WaitForSeconds(1);
        StartCoroutine(CalPlayTime());
    }

}
