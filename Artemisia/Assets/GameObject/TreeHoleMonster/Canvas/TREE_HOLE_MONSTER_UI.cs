using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class TREE_HOLE_MONSTER_UI : MonoBehaviour
{
    public SkeletonGraphic skeleton_graphic;
    public AnimationReferenceAsset eye_none, open_eyes;
    public GameObject Eventcontrol;
    void Start()
    {
        Eventcontrol = GameObject.Find("EventSystem");
        skeleton_graphic = GetComponent<SkeletonGraphic>();
        if(skeleton_graphic == null){
            return;
        }
        skeleton_graphic.AnimationState.SetAnimation(0,eye_none,false);
    }
    public void Open_eyes(){
        skeleton_graphic.AnimationState.SetAnimation(0,open_eyes,false);
        Invoke("PlayDeath", 2.9f);
    }

    public void PlayDeath(){
        //噴血死亡動畫
        Eventcontrol.GetComponent<PLAYER_DIE>().DIE_burst(SceneManager.GetActiveScene().buildIndex);
    }

}
