using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CAMERRASHAKE : MonoBehaviour
{

    float Shake = 1;
    float Setshake;
    bool Shakeswitch = false;
    public float time = 3;
    public bool is_dead = false;
    public bool Check_finish;
    public GameObject contiune;
    void Start()
    {
        Setshake = Shake;
        PUBLIC_VALUE.status_operator.WriteState(3, SceneManager.GetActiveScene().buildIndex - 2);
        if (PUBLIC_VALUE.tool_operator.GetToolState(57, 3) == true)
        {
            contiune.SetActive(true);
        }
    }
    public void Repect()
    {
        gameObject.transform.position = new Vector3(Random.Range(-3f, Shake * 2f) - Shake, transform.position.y, transform.position.z);
        // gameObject.transform.position=new Vector3(transform.position.x,transform.position.y,Random.Range(0f,Shake*1f));
        Shake = Shake / 1.05f;
        if (Shake < 0.05)
        {
            Shake = 0;
            Shakeswitch = false;
        }
    }

    void Update()
    {
        if (PUBLIC_VALUE.tool_operator.GetToolState(57, 3))
        {

            Shake = Setshake;
            Shakeswitch = true;
            StartCoroutine(delay());
            if (!IsInvoking("Repect"))
            {

                Invoke("Repect", 0.08f);
            }
        }

    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        contiune.SetActive(true);
        PUBLIC_VALUE.status_operator.WriteState(3, 7);
        StopAllCoroutines();
    }

}
