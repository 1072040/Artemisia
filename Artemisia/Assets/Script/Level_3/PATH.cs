using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PATH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider Collision){
        if(Collision.tag == "Player"){
            //迷霧出現

            //換圖
            if(this.tag == "correct"){
                print("正確道路");
                int scene_count = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene_count+1);
            }
            else{
                SceneManager.LoadScene(3);
            }
        }
    }
}
