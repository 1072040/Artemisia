using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEARTBEAT_CONTROL : MonoBehaviour
{
    public GameObject sound1;
    public GameObject sound2;
    public GameObject sound3;

    public GameObject Heart_sound1;
    public GameObject Heart_sound2;
    public GameObject Heart_sound3;
    public GameObject no_sound;

    void Update()
    {
        sound1.SetActive(Heart_sound1.transform.childCount > 0);
        sound2.SetActive(Heart_sound1.transform.childCount > 0 ? false : Heart_sound2.transform.childCount > 0);
        sound3.SetActive(Heart_sound1.transform.childCount > 0 ? false : Heart_sound2.transform.childCount > 0 ? false : Heart_sound3.transform.childCount > 0);
    }
}
