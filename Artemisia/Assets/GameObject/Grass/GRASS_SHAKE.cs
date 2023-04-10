using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class GRASS_SHAKE : MonoBehaviour
{
    public int which_grass;
    public SkeletonAnimation skeleton_anim;
    public AnimationReferenceAsset none, shake, grass1 , grass2 ,grass3;
    void Start()
    {
        switch (which_grass){
            case 1:
            skeleton_anim.AnimationState.SetAnimation(0 , grass1 , false);
            return;
            case 2:
            skeleton_anim.AnimationState.SetAnimation(0 , grass2 , false);
            return;
            case 3:
            skeleton_anim.AnimationState.SetAnimation(0 , grass3 , false);
            return;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            skeleton_anim.AnimationState.SetAnimation(1, shake, false);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
