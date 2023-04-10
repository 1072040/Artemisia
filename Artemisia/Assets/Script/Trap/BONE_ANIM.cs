using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BONE_ANIM : MonoBehaviour
{
    public Animator bone_anim;
    public GameObject bone_trigger;
   
    public void BoneTrap(){
        bone_anim.SetTrigger("bone_trap");
        bone_trigger.SetActive(false);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;

        GetComponent<BoxCollider>().size = new Vector3(2f,1f,1f);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(GetComponent<Rigidbody>());
    }
}
