using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter_trap : MonoBehaviour
{
    public GameObject Black_UI;
    private void OnTriggerEnter()
    {
        StartCoroutine(Woodcutter());

    }

    IEnumerator Woodcutter()
    {
        print("踩到陷阱撥樵夫過來解救的動畫");
        Black_UI.SetActive(true);
        yield return new WaitForSeconds(2f);
        Black_UI.SetActive(false);
        yield return new WaitForSeconds(1f);
        print("樵夫把主角放下來撥對話");
        Black_UI.SetActive(true);
        yield return new WaitForSeconds(1f);
        Black_UI.SetActive(false);
        print("傳回去樵夫家");
        Destroy(gameObject);

    }
}
