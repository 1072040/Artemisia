using System.Collections;
using System.Collections.Generic;
using UB.Simple2dWeatherEffects.Standard;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mist : MonoBehaviour
{
    public D2FogsPE camera_mist;
    public Image filter;
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public float des;
    public GameObject correct,finish;
    private float og_des;
    private bool check = false;
    private GameObject player;
    private Vector3 self_size,box_size;
    // Start is called before the first frame update
    void Awake()
    {
        og_des = camera_mist.Density;
        self_size = this.transform.localScale;
        box_size = this.GetComponent<BoxCollider>().size;
    }

    // Update is called once per frame
    void Update()
    {
        if (!check)
        {
            return;
        }

        if (up)
        {
            float temp = this.transform.position.z - self_size.z * box_size.z / 2;
            camera_mist.Density = og_des + Mathf.Abs(player.transform.position.z - temp) / (self_size.z * box_size.z) * des;
            filter.color = new Color(filter.color.r, filter.color.g, filter.color.b, 0.4f-Mathf.Abs(player.transform.position.z - temp) / (self_size.z * box_size.z)/2);
        }
        else if (down)
        {
            float temp = this.transform.position.z + self_size.z * box_size.z / 2;
            camera_mist.Density = og_des + Mathf.Abs(player.transform.position.z - temp) / (self_size.z * box_size.z) * des;
            filter.color = new Color(filter.color.r, filter.color.g, filter.color.b, 0.4f - Mathf.Abs(player.transform.position.z - temp) / (self_size.z * box_size.z)/2);
        }
        else if (right)
        {
            float temp = this.transform.position.x - self_size.x * box_size.x / 2;
            camera_mist.Density = og_des + Mathf.Abs(player.transform.position.x - temp) / (self_size.x * box_size.x) * des;
            filter.color = new Color(filter.color.r, filter.color.g, filter.color.b, 0.4f - Mathf.Abs(player.transform.position.x - temp) / (self_size.z * box_size.z)/2);
        }
        else if (left)
        {
            float temp = this.transform.position.x + self_size.x * box_size.x / 2;
            camera_mist.Density = og_des + Mathf.Abs(player.transform.position.x - temp) / (self_size.x * box_size.x) * des;
            filter.color = new Color(filter.color.r, filter.color.g, filter.color.b, 0.4f - Mathf.Abs(player.transform.position.x - temp) / (self_size.z * box_size.z)/2);
        }

        if (camera_mist.Density >= des)
        {
            Debug.Log("切換場景");
            if(SceneManager.GetActiveScene().buildIndex == 2){
                //這一關完成
                PUBLIC_VALUE.status_operator.WriteState(2,1);
            }
            if(this.tag == "correct"){
                print("正確道路");
                int scene_count = SceneManager.GetActiveScene().buildIndex;
                if(scene_count == 7 && PUBLIC_VALUE.tool_operator.GetToolState(57,3) == false){
                    Instantiate(finish);
                    SceneManager.LoadScene(scene_count+1);
                }    
                else{
                    Instantiate(correct);
                    SceneManager.LoadScene(scene_count+1);
                    Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                    PUBLIC_VALUE.record.SetPlayerPosition(position);
                }
            }
            else{
                SceneManager.LoadScene(3);
                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                 PUBLIC_VALUE.record.SetPlayerPosition(position);
                 PUBLIC_VALUE.status_operator.WriteState(3,1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            check = true;
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            check = false;
            player = null;
        }
    }
}
