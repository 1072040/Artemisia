using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SPIKE_TRAP : MonoBehaviour
{
    public GameObject palyer;
    public GameObject Eventcontrol;
    public GameObject trap_sound;
    private bool trap_sound_enable = false;
    Animator player_anim;
    public bool isActive = false;
    public GameObject bone;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Enabletrapsound", 2);
        palyer = GameObject.Find("Player");
        Eventcontrol = GameObject.Find("EventSystem");
        player_anim = palyer.GetComponent<PLAYER_MOVE>().player_anim;
    }
    private void Awake()
    {
        if(PUBLIC_VALUE.generate_operator.GetGenerateState(this.GetComponent<OBJECT_INFO>().cid, 3) == 1)
        {
            GameObject temp = Instantiate(bone, this.transform);
            temp.transform.localPosition = Vector3.zero;
        }
        else
        {
            GameObject temp = Instantiate(bone, this.transform);
            temp.transform.position = this.transform.GetChild(0).position;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(!isActive){
            if(other.tag == "Player"){
                //主角死亡
                player_anim.SetTrigger("Trap_die"); 
                palyer.GetComponent<PLAYER_MOVE>().move_able = false;
                if(trap_sound_enable) Instantiate(trap_sound);
                //噴血動畫
                Invoke("Die",0.5f);

            }else if(other.tag == "Item"){
                //開啟骨頭被插動畫
                other.GetComponent<BONE_ANIM>().BoneTrap();
                if(trap_sound_enable) Instantiate(trap_sound);
                PUBLIC_VALUE.generate_operator.SetGenerateState(this.GetComponent<OBJECT_INFO>().cid, 3, 1);
                isActive = true;
            }
        }
        
    }

    public void Enabletrapsound()
    {
        trap_sound_enable = true;
    }
    public void Die(){
        Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
    }
}
