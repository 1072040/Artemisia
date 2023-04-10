using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Spine.Unity;
public class MONSTER_STOP : MonoBehaviour
{
    private GameObject Player;
    
    void Start()
    {
        Player=GameObject.Find("Player");
    }
      private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="Monster")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().isStopped=true;
            collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("idle",true);
             //�R���̫�@����
            PUBLIC_VALUE.tool_operator.DeleteTool(33);
            //�R���n��}��
            PUBLIC_VALUE.tool_operator.DeleteTool(42);
        }
    }
      private void OnTriggerStay(Collider collision)
    {
        if(Vector3.Distance(Player.transform.position, collision.transform.position) > 10f&&collision.gameObject.tag=="Monster")
        {
            Destroy(collision.gameObject);
        }
    }
    
}
