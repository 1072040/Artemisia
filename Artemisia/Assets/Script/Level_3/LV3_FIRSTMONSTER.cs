using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class LV3_FIRSTMONSTER : MonoBehaviour
{
    public NavMeshAgent Agent;    //�ŧiNavMeshAgent
    public GameObject Main_player;    //�n�l�ܪ�����
    private GameObject Player_save;    //�splayer(�W�����|��l�ܥؼ� �o�Ӥ��|)
    public GameObject Eventcontrol; //��D��
    public Animator[] Normal_animator; //�Ǫ�animator
    //public LEVEL_1 LEVEL_1;
    private bool Is_track; //�O�_�A�l�ܥD��
    private bool In_range = false;
    private bool Check_dialog = false;
    private float Save_pox; //�O�������U��X�y��
    private float Save_poz;//�O�������U��Y�y��
    private int state = 0;

    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;

        Agent = GetComponent<NavMeshAgent>();
        Agent.isStopped = true;
        Agent.updateRotation = false; //�����Ǫ�����
        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
        //LEVEL_1 = GameObject.Find("Level1(Clone)").GetComponent<LEVEL_1>();
        Player_save = GameObject.Find("Player");
    }

    //�D���i�J�Ǫ��l�ܽd�� �Ǫ��}�l�l��
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            Player_save.GetComponent<HIDE_IN_TREE>().is_wandering = true;
            print("enter");
            In_range = true;
            Is_track = true;
            Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = Is_track;
            Agent.isStopped = false;
            Check_dialog=true;

            // if (state <= 0)
            // {
            //     Eventcontrol.GetComponent<DIALOG>().StartDialog(0, 1, 8);
            //     state = 1;
            //     print("after while");
            //     if (Eventcontrol.GetComponent<DIALOG>().dialoging == true) StartCoroutine(Dialog());
            // }
        }
    }

    // IEnumerator Dialog()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     if (Eventcontrol.GetComponent<DIALOG>().dialoging == false)
    //     {
    //         StopAllCoroutines();
    //         After_dialogue();
    //         Check_dialog=true;
    //     }
    //     else
    //     {
    //         StartCoroutine(Dialog());
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Player_save.GetComponent<HIDE_IN_TREE>().is_hide == false)
        {
            //Time.timeScale = 0;
            //�����`�ʵe�í��s�}�l
            print("you dead");
            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Active()
    {
        
        Agent.SetDestination(Main_player.transform.position);

        if (Save_pox > transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(-1, 1, 1);
        }
        else if(Save_pox < transform.position.x)
        {
            transform.GetChild(0).GetChild(0).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(1).localScale = new Vector3(1, 1, 1);
        }
        if (Save_poz > transform.position.z)
        {
            transform.GetChild(0).GetChild(2).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(3).localScale = new Vector3(-1, 1, 1);
        }
        else if(Save_poz < transform.position.z)
        {
            transform.GetChild(0).GetChild(2).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(3).localScale = new Vector3(1, 1, 1);
        }

        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
    }
    private void After_dialogue() //��ܧ��I�s�o��function
    {
        Agent.isStopped = false;
    }

    void FixedUpdate()
    {
        if (In_range && Is_track)
        {
            Main_player = Eventcontrol.GetComponent<EVENTCONTROL>().Player;
            if (!IsInvoking("Active")&&Check_dialog==true)
            {
                foreach(Animator normal_animator in Normal_animator)
                {
                    normal_animator.SetBool("normal_attack1", true);
                }
                InvokeRepeating("Active", 0.01f, 0.02f);
            }

        }

    }
    private void OnDestroy()
    {
        CancelInvoke("Active");
        if (Eventcontrol != null)
        {
            Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = false;
        }
        if (Player_save != null)
        {
            Player_save.GetComponent<HIDE_IN_TREE>().is_wandering = false;
        }
        //LEVEL_1.first_monster = true;
    }
}
