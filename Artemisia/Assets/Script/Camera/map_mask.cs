using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class map_mask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2){
            for(int i = 62;i <= 77;i++){
                if(PUBLIC_VALUE.generate_operator.GetMistGenerateState(i) == false){
                    Destroy(GameObject.Find(i.ToString()));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        int cid = collision.gameObject.GetComponent<OBJECT_INFO>().cid;
        PUBLIC_VALUE.generate_operator.SetMistGenerateState(cid);
        collision.gameObject.SetActive(false);


    }
}
