using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRAP : MonoBehaviour
{
    public GameObject Surprisingly_monster; //要生成的怪物prefab
    public GameObject Player;
    void Start()
    {
        Player=GameObject.Find("Player");
    }

    //主角走進指定區域生成怪物
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(Surprisingly_monster, Player.transform.position + new Vector3(8, 0, 0), new Quaternion(0, 0, 0, 0), gameObject.transform);
        }
    }
    //主角離開指定區域怪物跟陷阱消失
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject,0.5f);
        }
    }


}
