using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEAD_ANIMATOR : MonoBehaviour
{
    private GameObject Eventcontrol;
    private GameObject Player;
    public bool Leave_tree=false;
    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        Player = GameObject.Find("Player");
    }

    public void Normal_dead()
    {
        //Canvas打開撥動畫
    }

}
