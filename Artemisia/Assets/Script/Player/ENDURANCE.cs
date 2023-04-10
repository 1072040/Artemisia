using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ENDURANCE : MonoBehaviour
{
    private PLAYER_MOVE player_Move;
    private Animator player_anim;
    private bool temp;

    public bool is_run;
    public bool is_tired;
    public bool is_holding;

    public bool is_recover;
    public float run_time;
    public float recover_wait;
    public float recover_time;
    public Image image;

    public float monster_time;

    void Start()
    {
        player_Move = GetComponent<PLAYER_MOVE>();
        player_anim = player_Move.player_anim;
    }

    void Update()
    {
        if(player_Move.is_move && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !is_tired && !is_holding)
        {
            is_run = true;
            player_anim.SetInteger("move_status",2);

            CancelInvoke();
            is_recover = false;
            temp = true;
        }
        else
        {
            is_run = false;
            player_anim.SetInteger("move_status",1);
        }

        if (is_run)
        {
            image.fillAmount -= 1 / run_time * Time.deltaTime;
        }
        else
        {
            if (temp)
            {
                Invoke("Recover", recover_wait);
                temp = false;
            }
        }

        if (is_recover)
        {
            image.fillAmount += 1 / (recover_time + monster_time) * Time.deltaTime;
        }

        if (image.fillAmount == 0)
        {
            is_tired = true;
            player_anim.SetInteger("move_status" , 0);
            temp = true;
        }
        if(image.fillAmount == 1)
        {
            monster_time = 0;
            is_tired = false;
            is_recover = false;
            temp = false;
            player_anim.SetInteger("move_status" , 1);
        }
    }

    private void Recover()
    {
        is_recover = true;
    }
}
