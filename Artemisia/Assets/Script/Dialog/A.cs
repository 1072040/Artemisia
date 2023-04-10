using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    public GameObject script;

    public void ClickA(){
        //呼叫點擊件
        print("enter click_A");
        script.GetComponent<DIALOG>().StartDialog(1,1,1);
    }
        public void ClickB(){
        //呼叫點擊件
        print("enter click_B");
        script.GetComponent<DIALOG>().StartDialog(1,1,1);
    }
}
