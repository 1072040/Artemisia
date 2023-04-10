using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWITCH_WANDERING_TARGET : MonoBehaviour
{
    public GameObject target;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lv1_normalmonster")
        {
            count++;
            if (count >= 2)
            {
                Destroy(other.gameObject);
            }
            if (other.GetComponent<LV1_FIRSTMONSTER>() != null)
            {
                other.GetComponent<LV1_FIRSTMONSTER>().Eventcontrol.GetComponent<EVENTCONTROL>().Player = target;
            }
            if(other.GetComponent<LV3_NORMALMONSTER>()!= null)
            {
                other.GetComponent<LV3_NORMALMONSTER>().Eventcontrol.GetComponent<EVENTCONTROL>().Player = target;

            }
            if (other.GetComponent<ANGRY_MONSTER_BIG>() != null)
            {
                other.GetComponent<ANGRY_MONSTER_BIG>().Eventcontrol.GetComponent<EVENTCONTROL>().Player = target;

            }
        }


    }
}
