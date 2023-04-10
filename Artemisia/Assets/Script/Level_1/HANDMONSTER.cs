using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HANDMONSTER : MonoBehaviour
{
    public Animator Hand_monster_atack;
    public GameObject Eventcontrol;
    private GameObject Player;
    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Player =GameObject.Find("Player");
        Player.GetComponent<PLAYER_MOVE>().move_able=false;
        Hand_monster_atack.SetBool("attack",true);
        Invoke("Wait_animator", 0.9f);
    }

    public void Wait_animator()
    {
        //Time.timeScale=0;
        Eventcontrol.GetComponent<PLAYER_DIE>().DIE_burst(SceneManager.GetActiveScene().buildIndex);
    }
}
