using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DELECT_BUTTON : MonoBehaviour
{
    public void OnMouseEnter() {
         print("enter OnSelect");
         MAIN_MENU.now_select = this.tag;
    }
}
