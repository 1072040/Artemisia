using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOMBSTONE : MonoBehaviour
{
    public bool Tombstone_empty;
    public bool Tombstone_item;
    public bool Tombstone_monster;
    public GameObject hole;
    public GameObject Is_Tombstone;
    public GameObject smoke;
    private GameObject Eventcontrol;
    private GameObject player;
    public GameObject Hand_monster;
    public GameObject object_spawn;

    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        player = GameObject.Find("Player");
    }
    private void Awake()
    {
        InvokeRepeating("check", 0.1f, 0.1f);
    }

    private void Tombstone()
    {                                        
        //修改該墳墓生成狀態
        PUBLIC_VALUE.generate_operator.SetGenerateState(player.GetComponent<PLAYER_INTERACTIVE>().interactive_cid,1,1);
        //PUBLIC_VALUE.tool_operator.DeleteTool(player.GetComponent<PLAYER_INTERACTIVE>().interactive_cid);
        if(Tombstone_empty)
        {
            
            Eventcontrol.GetComponent<EVENTCONTROL>().Count_tombstone_num++;
            Instantiate(smoke, hole.transform.position + new Vector3(0, 0, -1.3f), new Quaternion(0, 0, 0, 0));
            if (Is_Tombstone != null)
            {
                GameObject temp = Instantiate(Is_Tombstone, object_spawn.transform);
                temp.transform.position = object_spawn.transform.position;
            }
            hole.SetActive(true);
            Destroy(transform.parent.gameObject);
            // Destroy(this.gameObject.GetComponent<OBJECT_INFO>());
            // Destroy(this);
        }
        else if(Tombstone_item)
        {
            Instantiate(smoke, hole.transform.position + new Vector3(0, 0, -1.3f), new Quaternion(0, 0, 0, 0));
            Instantiate(Is_Tombstone, object_spawn.transform.position, new Quaternion(0, 0, 0, 0));
            Eventcontrol.GetComponent<EVENTCONTROL>().Count_tombstone_num++;
            //將寶石設成需要生成
             int cid = Is_Tombstone.GetComponentInChildren<OBJECT_INFO>().cid;
            PUBLIC_VALUE.generate_operator.SetGenerate(cid,1);
            // PUBLIC_VALUE.tool_operator.setToolGameobject(id,"Lv1_prefab/"+id.ToString());
            hole.SetActive(true);
            Destroy(transform.parent.gameObject);
            // Destroy(this.gameObject.GetComponent<OBJECT_INFO>());
            // Destroy(this);
        }
        else if(Tombstone_monster)
        {
            Eventcontrol.GetComponent<EVENTCONTROL>().Count_tombstone_num++;
            Instantiate(Hand_monster, player.transform.position+new Vector3(0,0,-0.1f), new Quaternion(0,0,0,0));
            Destroy(transform.parent.gameObject);
            // Destroy(this.gameObject.GetComponent<OBJECT_INFO>());
            // Destroy(this);
        }
    }

    void Update()
    {
        if(gameObject.GetComponent<OBJECT_INFO>().combo_value==0)
        {
            Tombstone();
        }    
    }

    void check()
    {
        if (this.GetComponent<OBJECT_INFO>().id == 0)
        {
            return;
        }
        CancelInvoke();
        if (PUBLIC_VALUE.generate_operator.GetGenerateState(this.GetComponent<OBJECT_INFO>().cid, 1) == 1)
        {
            if (Tombstone_empty)
            {
                if (Is_Tombstone != null)
                {
                    GameObject temp = Instantiate(Is_Tombstone, object_spawn.transform);
                    temp.transform.position = object_spawn.transform.position;
                }
                hole.SetActive(true);
                Destroy(transform.parent.gameObject);
                // Destroy(this.gameObject.GetComponent<OBJECT_INFO>());
                // Destroy(this);
            }
            else if (Tombstone_item)
            {
                print(this.GetComponent<OBJECT_INFO>().cid);
                int id = Is_Tombstone.GetComponentInChildren<OBJECT_INFO>().id;
                PUBLIC_VALUE.tool_operator.setToolGameobject(id, "Lv1_prefab/" + id.ToString());
                hole.SetActive(true);
                Destroy(transform.parent.gameObject);
                // Destroy(this.gameObject.GetComponent<OBJECT_INFO>());
                // Destroy(this);
            }
            else if (Tombstone_monster)
            {
                Debug.Log(this.GetComponent<OBJECT_INFO>().cid);
                this.hole.SetActive(true);
                Destroy(transform.parent.gameObject);
                //Debug.Log(this.GetComponent<OBJECT_INFO>().cid);
                // Destroy(this.gameObject.GetComponent<OBJECT_INFO>());
                // Destroy(this);
            }
        }
    }
}
