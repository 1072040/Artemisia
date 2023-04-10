using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FOG6 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject contiune;
    void Start()
    {
        PUBLIC_VALUE.status_operator.WriteState(3,SceneManager.GetActiveScene().buildIndex-2);
        if(PUBLIC_VALUE.tool_operator.GetToolState(57,3) == true){
            contiune.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PUBLIC_VALUE.tool_operator.GetToolState(57,3) == true){
            StartCoroutine(delay());
        }
    }
    IEnumerator delay(){
        yield return new WaitForSeconds(1.5f);
        contiune.SetActive(true);
        PUBLIC_VALUE.status_operator.WriteState(3,7);
        StopAllCoroutines();
    }

}
