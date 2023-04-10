using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LV1_FIRSTMONSTERANIMATOR : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    public void Speed_slow()
    {
        agent.isStopped = true;
    }
    public void Speed_quick()
    {
        agent.isStopped = false;
    }
}
