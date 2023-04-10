using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_trigger : MonoBehaviour
{
    private Animator temp;
    public bool none;
    public bool wood;
    public bool dirt;
    public bool joajur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            temp = other.GetComponent<PLAYER_MOVE>().player_anim;
            temp.SetBool("none", none);
            temp.SetBool("wood", wood);
            temp.SetBool("dirt",dirt);
            temp.SetBool("joajur",joajur);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            temp.SetBool("none", false);
            temp.SetBool("wood", false);
            temp.SetBool("dirt", false);
            temp.SetBool("joajur", false);
        }
    }
}
