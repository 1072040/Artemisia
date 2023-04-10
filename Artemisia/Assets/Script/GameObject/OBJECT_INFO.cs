using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OBJECT_INFO : MonoBehaviour
{
    /*
    id對照表     
    npc             1~99
    //////////////////////
    獲得道具        101~199
    ///////////////////////
    躲藏            201~299
    手持            301~399
    連打            401~499
    怪物            501~599
    */
    public int id;
    public int cid;
    public int combo_value = 0;
    public int level = 0;
    public int area = 0;
    public int resurrection = 0;

    public UnityEvent call_function;
}
