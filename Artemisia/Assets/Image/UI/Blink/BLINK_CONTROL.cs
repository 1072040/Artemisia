using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class BLINK_CONTROL : MonoBehaviour
{
    public bool isBlink;
    public bool anim_finish;

    public SkeletonGraphic skeleton_anim;
    public AnimationReferenceAsset blink ,idle;
    void Start()
    {
        skeleton_anim = GetComponent<SkeletonGraphic>();
        if (skeleton_anim == null){
            return;
        }
        skeleton_anim.AnimationState.SetAnimation(0 , idle , true);
        anim_finish = true;
    }
    void Update() {
        if(anim_finish){
            if(isBlink){
                Blink();
            }else{
            skeleton_anim.AnimationState.SetAnimation(0 , idle , true);
            }
        }
        
    }
   public void Blink(){
       Spine.TrackEntry animationEntry =  skeleton_anim.AnimationState.SetAnimation(0 , blink , false);
       anim_finish = false;
       animationEntry.Complete += BlindComplete;
   }
    private void BlindComplete(Spine.TrackEntry trackEntry){
        isBlink = false;
        anim_finish = true;
    }
}
