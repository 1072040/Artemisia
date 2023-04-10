using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GROUND_BOOK : MonoBehaviour
{
    public GameObject Book;
    private GameObject eventsystem;

    //public GameObject Teach_Panel;
    private GameObject Player;
    public GameObject Leve_0;
    void Start()
    {
        Player = GameObject.Find("Player");
        
    }
    private void OnTriggerEnter()
    {
        eventsystem = GameObject.Find("EventSystem");
        Leve_0.GetComponent<LEVEL_0>().Status = 2;
        StartCoroutine(Throw_book());
    }

    IEnumerator Throw_book()
    {
        GameObject Book_clone =Instantiate(Book, Player.transform.position+new Vector3(0,1f,-0.5f), Quaternion.identity);
        Book_clone.GetComponent<Rigidbody>().AddForce(new Vector3(2,0,-1) * 1000);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.parent.gameObject.GetComponent<AudioSource>().Play();
        eventsystem.GetComponent<DIALOG>().StartDialog(0, 0, 7);
        Destroy(gameObject);
    }
}
