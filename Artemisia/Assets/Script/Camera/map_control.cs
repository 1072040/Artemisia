using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UB.Simple2dWeatherEffects.Standard;

public class map_control : MonoBehaviour
{
    public GameObject map;
    public float move_speed;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            D2FogsPE[] temp = this.GetComponents<D2FogsPE>();
            foreach (D2FogsPE p in temp)
            {
                p.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!map.activeSelf)
        {
            this.transform.position = new Vector3(this.transform.parent.position.x, this.transform.position.y, this.transform.parent.position.z);
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.up * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * move_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.down * move_speed * Time.deltaTime);
        }
    }
}
