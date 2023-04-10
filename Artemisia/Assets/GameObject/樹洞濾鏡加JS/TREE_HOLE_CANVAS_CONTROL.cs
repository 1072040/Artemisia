using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TREE_HOLE_CANVAS_CONTROL : MonoBehaviour
{
    public int which_hole;
    public SkeletonAnimation skeleton_anim;
    public AnimationReferenceAsset none, scare, hole1 , hole2 ,hole3 ,close;
    private bool temp = true;
    public GameObject eye_ui;

    void Start() {
        eye_ui = GameObject.Find("Eye_UI");

        skeleton_anim = GetComponent<SkeletonAnimation>();
        if (skeleton_anim == null){
            return;
        }

        switch (which_hole){
            case 1:
            skeleton_anim.AnimationState.SetAnimation(0 , hole1 , false);
            return;
            case 2:
            skeleton_anim.AnimationState.SetAnimation(0 , hole2 , false);
            return;
            case 3:
            skeleton_anim.AnimationState.SetAnimation(0 , hole3 , false);
            return;
        }
        
    }
    
    
    //測試用到時候註解掉
    private void Update() {
    }

    public void Scare(){
        if (temp)
        {
            skeleton_anim.AnimationState.SetAnimation(1, scare, false);
            this.GetComponent<AudioSource>().Play();
            temp = false;
        }
    }
    public void Close(){
        //呼叫這個就可以開啟動畫
        skeleton_anim.AnimationState.SetAnimation(1, close, false);
        this.GetComponent<AudioSource>().Play();
        Invoke("OpEye",0.2f);
    }
    
    public void OpEye(){
        eye_ui.GetComponent<TREE_HOLE_MONSTER_UI>().Open_eyes();
        eye_ui.GetComponent<AudioSource>().Play();
    } 

}
