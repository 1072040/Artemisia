using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BRIDGE_CONTROL : MonoBehaviour
{
    public SkeletonAnimation skeleton_anim;
    public AnimationReferenceAsset idle,shake;
    private AudioSource Bridge_sound;

    void Start()
    {
            Bridge_sound=GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
       
            //晃動開啟
            Debug.Log("晃開啟");
            skeleton_anim.state.SetAnimation(0,shake,true);
            Bridge_sound.Play();
        
    }
    private void OnTriggerExit(Collider other) {
        
            //晃動關閉
            Debug.Log("晃關閉");
            skeleton_anim.state.SetAnimation(0,idle,true);
            Bridge_sound.Stop();
        
    }
}
