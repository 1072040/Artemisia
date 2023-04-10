using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVE_TREE_CAMERA : MonoBehaviour
{
    public float smooth = 5f;
    public float moveDistance = 5f;
    private Vector3 og;
    private Vector3 up;
    private Vector3 down;
    private Vector3 left;
    private Vector3 right;
    private Vector3 top_left;
    private Vector3 top_right;
    private Vector3 bottom_left;
    private Vector3 bottom_right;
    // Start is called before the first frame update
    void Start()
    {
        og = CheckAngle(this.transform.eulerAngles);

        up = new Vector3(og.x - moveDistance,og.y,og.z);
        down = new Vector3(og.x + moveDistance, og.y, og.z);
        left = new Vector3(og.x, og.y - moveDistance, og.z);
        right = new Vector3(og.x,og.y + moveDistance, og.z);

        top_left = new Vector3(og.x - moveDistance, og.y - moveDistance, og.z);
        top_right = new Vector3(og.x - moveDistance, og.y + moveDistance, og.z);
        bottom_left = new Vector3(og.x + moveDistance, og.y - moveDistance, og.z);
        bottom_right = new Vector3(og.x + moveDistance, og.y + moveDistance, og.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 checkangle = this.transform.eulerAngles;
        checkangle = CheckAngle(checkangle);
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.eulerAngles = Vector3.Lerp(checkangle, top_left, smooth * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.transform.eulerAngles = Vector3.Lerp(checkangle, top_right, smooth * Time.deltaTime);
            }
            else
            {
                this.transform.eulerAngles = Vector3.Lerp(checkangle, up, smooth * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.eulerAngles = Vector3.Lerp(checkangle, bottom_left, smooth * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.transform.eulerAngles = Vector3.Lerp(checkangle, bottom_right, smooth * Time.deltaTime);
            }
            else
            {
                this.transform.eulerAngles = Vector3.Lerp(checkangle, down, smooth * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.eulerAngles = Vector3.Lerp(checkangle, right, smooth * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.eulerAngles = Vector3.Lerp(checkangle, left, smooth * Time.deltaTime);
        }
        else
        {
            this.transform.eulerAngles = Vector3.Lerp(checkangle, og, smooth * Time.deltaTime);
        }

    }

    private Vector3 CheckAngle(Vector3 value)
    {
        Vector3 angel = new Vector3 (value.x, value.y, value.z);

        if (angel.x > 360-moveDistance)
        {
            angel.x -= 360;
        }
        if (angel.y > 360 - moveDistance)
        {
            angel.y -= 360;
        }
        if (angel.z > 360 - moveDistance)
        {
            angel.z -= 360;
        }

        return angel;
    }
}