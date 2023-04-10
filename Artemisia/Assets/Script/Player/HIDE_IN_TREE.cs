using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIDE_IN_TREE : MonoBehaviour
{
    public bool can_hide = true;
    public Camera main_camera;
    public bool is_hide;
    public bool camera_moving;
    public bool is_wandering;
    public float smooth;
    public float camera_size;

    public GameObject target;
    public GameObject target_camera;
    public GameObject cutscenes;
    public GameObject player_img;

    public GameObject normal_light;

    private PLAYER_MOVE player_Move;
    private Vector3 temp_main_camera_transform;
    private float temp_main_camera_size;
    private bool is_camera_switch;
    private bool flag;

    private EVENTCONTROL monster_track;
    private bool monster_track_once = true;
    // Start is called before the first frame update
    void Start()
    {
        player_Move = GetComponent<PLAYER_MOVE>();
        monster_track = GameObject.Find("EventSystem").GetComponent<EVENTCONTROL>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_moving)
        {
            if (is_hide)
            {
                Hide();
                if (Is_camera_finish())
                {
                    main_camera.gameObject.SetActive(false);
                    target_camera.SetActive(true);
                    cutscenes.SetActive(true);
                    is_camera_switch=true;
                    normal_light.SetActive(false);
                    camera_moving = !Is_camera_finish();
                }
            }
            else
            {
                cutscenes.GetComponent<Animator>().SetTrigger("Leave");
                if (cutscenes.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finish") && is_camera_switch)
                {
                    main_camera.gameObject.SetActive(true);
                    target_camera.SetActive(false);
                    cutscenes.SetActive(false);
                    is_camera_switch = false;
                    normal_light.SetActive(true);
                }
                if (!is_camera_switch)
                {
                    Leave();
                    camera_moving = !Is_camera_finish();
                }
            }
        }
        if (is_hide || camera_moving)
        {
            player_Move.move_able = false;
            flag = true;
        }
        else if(flag)
        {
            player_Move.move_able = true;
            flag = false;
        }

        can_hide = !monster_track.GetComponent<DIALOG>().dialoging;
    }

    public void Active(){
         if (!is_hide && Is_camera_finish() && target != null && can_hide)  
        {
            temp_main_camera_transform = main_camera.transform.position;
            temp_main_camera_size = main_camera.orthographicSize;
            //儲存躲藏時的攝影機位置，復原時會用到
            is_hide = true;
            camera_moving = true;
        }
       
        if (is_hide && Is_camera_finish())
        {
            is_hide = false;
            camera_moving = true;

            if (monster_track.Is_tracks)
            {
                Debug.LogError("怪物徘徊時出樹洞，死去");
            }
        }
    }

    private bool Is_camera_finish() //判斷攝影機是否縮放、移動完畢
    {
        if (is_hide)
        {
            if (main_camera.orthographicSize - camera_size < 0.01)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (temp_main_camera_size - main_camera.orthographicSize < 0.01)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private void Hide()
    {
        player_img.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        main_camera.transform.position = Vector3.Lerp(main_camera.transform.position,
               new Vector3(target.gameObject.transform.position.x,
               main_camera.transform.position.y + target.gameObject.transform.position.y,
               target.gameObject.transform.position.z - Mathf.Tan((90 - main_camera.transform.eulerAngles.x) * Mathf.PI / 180) * main_camera.transform.position.y),
               smooth * Time.deltaTime);
        //攝影機位置移動到樹洞

        main_camera.orthographicSize = Mathf.Lerp(main_camera.orthographicSize, camera_size, smooth * Time.deltaTime);
        //攝影機大小調整到1

        if (monster_track.Is_tracks && monster_track_once && is_wandering)
        {
            target.GetComponent<TREE_HOLE>().Tree_hole.GetComponent<WANDERING>().wandering();
            monster_track_once = false;
        }
        // GetComponent<Normal_monster>().player=GameObject.Find("");  躲進樹洞後改變怪物追蹤的對象if_track=true
    } //躲進樹洞
    private void Leave()//離開樹洞
    {
        if (main_camera.orthographicSize >= temp_main_camera_size/2)
        {
            player_img.SetActive(true);
        }
        this.gameObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionZ;

        main_camera.transform.position = Vector3.Lerp(main_camera.transform.position, temp_main_camera_transform, smooth * Time.deltaTime);
        //攝影機位置復原
        main_camera.orthographicSize = Mathf.Lerp(main_camera.orthographicSize, temp_main_camera_size, smooth * Time.deltaTime);
        //攝影機大小復原
        target.GetComponent<TREE_HOLE>().Tree_hole.GetComponent<WANDERING>().wandering_end();

        monster_track_once = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.GetComponent<OBJECT_INFO>().id >= 201 && other.GetComponent<OBJECT_INFO>().id <= 204)
            {
                target = other.gameObject;
                target_camera = other.transform.GetChild(0).gameObject;
            }
        }
        catch
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            if (other.GetComponent<OBJECT_INFO>().id == 201)
            {
                target = null;
            }
        }
        catch
        {

        }
    }
}
