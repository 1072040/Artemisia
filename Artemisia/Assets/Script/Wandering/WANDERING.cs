using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WANDERING : MonoBehaviour
{
    public GameObject Wandering_target;
    public GameObject left;
    public GameObject right;
    public float length;

    private EVENTCONTROL monster_track;
    // Start is called before the first frame update
    void Start()
    {
        monster_track = GameObject.Find("EventSystem").GetComponent<EVENTCONTROL>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void wandering()
    {
        left = Instantiate(Wandering_target, this.transform);
        left.name = "left_target";
        left.transform.localPosition = new Vector3(-length, 0, 0);

        right = Instantiate(Wandering_target, this.transform);
        right.name = "right_target";
        right.transform.localPosition = new Vector3(length, 0, 0);

        left.GetComponent<SWITCH_WANDERING_TARGET>().target = right;
        right.GetComponent<SWITCH_WANDERING_TARGET>().target = left;

        monster_track.Player = left;
    }
    public void wandering_end()
    {
        monster_track.Player = GameObject.Find("Player");
        Destroy(left);
        Destroy(right);
    }
}
