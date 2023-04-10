using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANGRY_MONSTER_ANIMATOR : MonoBehaviour
{
    public Animator Angry_monster_small;
    public GameObject Angry_monster_big;
    public AudioClip Grass_shaking;
    private AudioSource AudioSources;

    void Start()
    {
        AudioSources=GetComponent<AudioSource>();
    }
   public void Change_Angry_animator()
    {
        Angry_monster_small.SetBool("angry",false);
        Angry_monster_small.SetBool("hide",false);
        AudioSources.PlayOneShot(Grass_shaking);
        Instantiate(Angry_monster_big,transform.position+new Vector3(0,0,0.1f),transform.rotation);
        
    }
}
