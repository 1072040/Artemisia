using System.Collections;
using System.Collections.Generic;
using UB.Simple2dWeatherEffects.Standard;
using UnityEngine;
using UnityEngine.UI;

public class mist_scence : MonoBehaviour
{
    public Image filter;
    private bool temp = false;
    private float og_des;
    private D2FogsPE main_camera;
    private PLAYER_MOVE player;
    // Start is called before the first frame update
    void Start()
    {
        filter.color = new Color(filter.color.r, filter.color.g, filter.color.b, 0);
        Invoke("scene_start", 0.5f);
        main_camera = this.GetComponent<D2FogsPE>();
        og_des = main_camera.Density;
        main_camera.Density = 7f;
        player = GameObject.Find("Player").GetComponent<PLAYER_MOVE>();
        player.move_able = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (temp)
        {
            if (main_camera.Density >= og_des)
            {
                main_camera.Density -= 1.5f * Time.deltaTime;
                filter.color = new Color(filter.color.r, filter.color.g, filter.color.b, filter.color.a + 0.1f * Time.deltaTime);
            }
            else
            {
                player.move_able = true;
                Destroy(this);
            }
        }
    }

    void scene_start()
    {
        temp = true;
    }
}
