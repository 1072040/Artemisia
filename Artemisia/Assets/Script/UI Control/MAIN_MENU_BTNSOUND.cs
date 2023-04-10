using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MAIN_MENU_BTNSOUND : MonoBehaviour, ISelectHandler
{
    public AudioClip Switch;
    public AudioClip Check;
    public AudioSource Audios;
    public int Status=0;

    public void Click_sound()
    {
        Audios.PlayOneShot(Check);
    }
    public void Switch_sound()
    {
        Audios.PlayOneShot(Switch);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (this.gameObject.name == "Guide")
        {
            Audios.enabled = true;
        }
        Audios.PlayOneShot(Switch);

    }


}
