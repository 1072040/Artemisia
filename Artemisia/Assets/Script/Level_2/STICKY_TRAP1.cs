using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STICKY_TRAP1 : MonoBehaviour
{
    public GameObject Eventcontrol;

    public GameObject Player;

    void Start()
    {
        Eventcontrol=GameObject.Find("EventSystem");
        Player=GameObject.Find("Player");
    }
    private void OnTriggerEnter(Collider player)
    {
        if(gameObject.tag=="Player")
        {
            // Player.GetComponent<Animator>().SetBool("",true); 被刺死動畫
            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
