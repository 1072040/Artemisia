using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIEPANEL : MonoBehaviour
{
    public GameObject button;
    public GameObject player;
    public static DIEPANEL instance{get;set;}
    void Start() {
        player=GameObject.Find("Player");
        print("Awake");
        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Update()
    {
        if (Input.anyKeyDown && button.activeInHierarchy)
        {
            Destroy(this.gameObject);
        }
    }
}
