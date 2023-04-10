using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ANGRY_MONSTER_POPOUT : MonoBehaviour
{

    public NavMeshAgent Agent;    //宣告NavMeshAgent

    // Update is called once per frame
       public void After_animation()
    {
        Agent.isStopped=false;
       
    }
}
