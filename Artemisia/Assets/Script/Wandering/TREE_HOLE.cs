using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TREE_HOLE : MonoBehaviour
{
    public bool tree_hole_monster;
    public GameObject Tree_hole;

    private GameObject player;

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        if(player.GetComponent<HIDE_IN_TREE>().is_hide == player.GetComponent<HIDE_IN_TREE>().camera_moving == true)
        {
            this.GetComponent<AudioSource>().Play();
        }

        if (!tree_hole_monster)
        {
            return;
        }

        Invoke("wait", 3f);
        tree_hole_monster = false;
    }

    private void wait()
    {
        this.transform.GetChild(0).GetChild(0).GetComponent<TREE_HOLE_CANVAS_CONTROL>().Close();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

}
