using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{
    public GameObject script;
    public void ClickB(){
        //呼叫點擊件
        print("enter click_B");
        script.GetComponent<DIALOG>().StartDialog(2,1,1);
    }
}
