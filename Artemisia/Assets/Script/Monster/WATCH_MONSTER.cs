using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class WATCH_MONSTER : MonoBehaviour
{

    public Animator[] watch_monster_animator;//怪物的animator
    public Animator Watch_monster_tree; //躲樹洞後面時的animator

    private GameObject Player; //抓主角

    public NavMeshAgent Watch_monster;

    private GameObject Eventcontrol;

    public GameObject Skull;
    private bool Watch_monster_is_hide = true;

    private bool Is_Skull = false;
    private bool Is_attack = false;
    private bool Is_watchmonster_leave = false;
    private float Save_monster_y;
    private bool audio_play_once = true;
    private bool monster_delete = false;

    private float Save_pox;
    private float Save_poz;


    void Start()
    {
        Player = GameObject.Find("Player");
        Eventcontrol = GameObject.Find("EventSystem");
        Watch_monster = GetComponent<NavMeshAgent>();
        Watch_monster.updateRotation = false; //關閉怪物旋轉
        Save_pox = transform.position.x;
        Save_poz = transform.position.z;
    }



    private void OnTriggerEnter(Collider Item) //判斷道具在怪物的哪個方位
    {
        if (Item.gameObject.tag == "Item")
        {
            StartCoroutine(Wait_second());
            IEnumerator Wait_second()
            {
                yield return new WaitForSeconds(1f);
                if (Item.transform.position.x < transform.position.x)
                {
                    Skull = Eventcontrol.GetComponent<EVENTCONTROL>().Skull301;
                    Watch_monster.isStopped = false;
                    // transform.localScale = new Vector3(-1, 1, 1);
                    if (Watch_monster_is_hide == true)
                    {
                        foreach (Animator temp in watch_monster_animator)
                        {
                            temp.SetBool("standup", true);
                        }
                        Destroy(transform.GetChild(0).transform.GetChild(1).gameObject);
                        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                        transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
                        transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("normal_idle", true);
                        transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("normal_idle", true);
                        transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<Animator>().SetBool("normal_idle", true);
                        yield return new WaitForSeconds(2f);
                        foreach (Animator temp in watch_monster_animator)
                        {
                            temp.SetBool("normal_walk", true);
                        }

                        Is_Skull = true;
                        Watch_monster_is_hide = false;
                        Is_watchmonster_leave = true;
                    }
                    else if (Skull == null)
                    {
                        Watch_monster.isStopped = true;
                    }
                    else
                    {
                        foreach (Animator temp in watch_monster_animator)
                        {
                            temp.SetBool("normal_idle", false);
                            temp.SetBool("normal_walk", true);
                        }
                        Is_Skull = true;
                    }
                }
                else if (Item.transform.position.x > transform.position.x)
                {
                    Skull = Eventcontrol.GetComponent<EVENTCONTROL>().Skull301;
                    Watch_monster.isStopped = false;
                    // transform.localScale = new Vector3(1, 1, 1);
                    if (Watch_monster_is_hide == true)
                    {
                        foreach (Animator temp in watch_monster_animator)
                        {
                            temp.SetBool("standup", true);
                        }
                        Destroy(transform.GetChild(0).transform.GetChild(1).gameObject);
                        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                        transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
                        transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("normal_idle", true);
                        transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("normal_idle", true);
                        transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<Animator>().SetBool("normal_idle", true);
                        yield return new WaitForSeconds(2f);
                        foreach (Animator temp in watch_monster_animator)
                        {
                            temp.SetBool("normal_walk", true);
                        }
                        Is_Skull = true;
                        Watch_monster_is_hide = false;
                        Is_watchmonster_leave = true;
                    }
                    else if (Skull == null)
                    {
                        Watch_monster.isStopped = true;
                    }
                    else
                    {
                        foreach (Animator temp in watch_monster_animator)
                        {
                            temp.SetBool("normal_idle", false);
                            temp.SetBool("normal_walk", true);
                        }
                        Is_Skull = true;
                    }
                }
            }

        }
        if (Item.CompareTag("Player"))
        {
            monster_delete = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster_delete = true;
        }
    }
    private void OnTriggerStay(Collider Treehole)
    {

        if (Treehole.gameObject.tag == "Treehole" && Treehole.gameObject == Player.GetComponent<HIDE_IN_TREE>().target)
        {
            monster_delete = false;
            if (Player.GetComponent<HIDE_IN_TREE>().is_hide == true)
            {
                Eventcontrol.GetComponent<EVENTCONTROL>().Is_tracks = true;
                StartCoroutine(Delay_animator());//進樹洞後兩秒判斷是否有中途離開樹洞
                IEnumerator Delay_animator()
                {
                    yield return new WaitForSeconds(1f);
                    if (Player.GetComponent<HIDE_IN_TREE>().is_hide == false)
                    {
                        Watch_monster.isStopped = true;
                        yield return new WaitForSeconds(1f);
                        Player.GetComponent<PLAYER_MOVE>().move_able = false;
                        transform.localScale = new Vector3(-1, 1, 1);
                        Save_monster_y = transform.position.y;
                        Watch_monster.enabled = false;
                        transform.position = Player.transform.position + new Vector3(2.3f, Save_monster_y, -0.1f);
                        //播attack動畫
                        transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().SetBool("attack", true);
                        Is_attack = true;
                        if (audio_play_once)
                        {
                            this.GetComponent<AudioSource>().PlayDelayed(0.5f);
                            audio_play_once = false;
                        }
                        yield return new WaitForSeconds(0.8f);
                        Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
                    }
                    else if (Is_watchmonster_leave)
                    {
                        //從主角身上的hide_in_tree找到target_camera，在抓子物件也就是樹洞的遮罩(包含嚇人動畫)，然後呼叫嚇人的function
                        StartCoroutine(Delay_watchmonster_animator());
                        IEnumerator Delay_watchmonster_animator()
                        {
                            //徘徊
                            foreach (Animator temp in watch_monster_animator)
                            {
                                temp.SetBool("normal_walk", true);
                                temp.SetBool("normal_idle", false);
                            }
                            if (Treehole.GetComponent<TREE_HOLE>().Tree_hole.transform.eulerAngles.y == 0)
                            {
                                transform.localScale = new Vector3(-1, 1, 1);
                                Watch_monster.enabled = false;
                                this.transform.Translate(Vector3.left * Time.deltaTime * 0.5f);
                            }
                            else
                            {
                                Watch_monster.enabled = false;
                                this.transform.Translate(Vector3.forward * Time.deltaTime * 0.5f);
                            }
                            yield return new WaitForSeconds(6f);
                            watch_monster_animator[0].gameObject.SetActive(false);
                            Player.GetComponent<HIDE_IN_TREE>().target_camera.transform.GetChild(0).GetComponent<TREE_HOLE_CANVAS_CONTROL>().Scare();
                            yield return new WaitForSeconds(1.7f);
                            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
                        }

                    }
                    else
                    {
                        Watch_monster.isStopped = true;
                        Watch_monster_tree.SetBool("ishide", true);
                        //Canvas打開撥動畫
                        if (Watch_monster_tree.GetCurrentAnimatorStateInfo(0).IsName("hide_finished"))//怪物躲起來之後再開動畫
                        {
                            //從主角身上的hide_in_tree找到target_camera，在抓子物件也就是樹洞的遮罩(包含嚇人動畫)，然後呼叫嚇人的function
                            Player.GetComponent<HIDE_IN_TREE>().target_camera.transform.GetChild(0).GetComponent<TREE_HOLE_CANVAS_CONTROL>().Scare();
                            yield return new WaitForSeconds(1.7f);
                            Eventcontrol.GetComponent<PLAYER_DIE>().DIE_splattered(SceneManager.GetActiveScene().buildIndex);
                        }
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        //print(Skull);
        if (monster_delete && Is_watchmonster_leave && Player.GetComponent<HIDE_IN_TREE>().is_hide)
        {
            Destroy(this.gameObject);
        }
        if (Skull == null && Watch_monster_is_hide == false)
        {
            foreach (Animator temp in watch_monster_animator)
            {
                temp.SetBool("normal_walk", false);
                temp.SetBool("normal_idle", true);
            }
        }
        else if (Is_Skull == true && Skull != null)
        {
            Player.GetComponent<PLAYER_MOVE>().move_able = false;
            Player.GetComponent<NavMeshObstacle>().enabled = true;
            if (Save_pox > transform.position.x)
            {
                print("1");
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Save_pox < transform.position.x)
            {
                print("2");
                transform.localScale = new Vector3(1, 1, 1);
            }
            Save_pox = transform.position.x;
            Save_poz = transform.position.z;
            if (Watch_monster.enabled == true && Watch_monster.isOnNavMesh)
            {
                Watch_monster.SetDestination(Skull.transform.position);
                if (Vector3.Distance(transform.position, Skull.transform.position) < 2.5f)
                {
                    if (Is_attack == false)
                    {
                        Player.GetComponent<PLAYER_MOVE>().move_able = true;
                        Player.GetComponent<NavMeshObstacle>().enabled = false;
                    }
                    foreach (Animator temp in watch_monster_animator)
                    {
                        temp.SetBool("normal_walk", false);
                        temp.SetBool("normal_idle", true);
                    }
                }
            }
        }

    }

}
