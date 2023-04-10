using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_ANIM : MonoBehaviour
{
    bool is_moving;
    int move_status;
    int forward;

    bool is_digging;

    void Update()
    {
        if(!is_moving){
            
            if(is_digging){//挖

            }else{//idle
                PlayIdle();
            }
        }else if(is_moving){
            switch(move_status){
                case 0 ://累
                PlayerTired();
                return;
                case 1 ://走
                PlayerWalk();
                return;
                case 2 ://跑
                PlayerRun();
                return;
            }
        }
    }

    public void PlayIdle(){
        switch (forward){
            case 1:

            return;
            case 2:
            
            return;
            case 3:
            
            return;
            case 4:
            
            return;
        }
    }

    public void PlayerTired(){
        switch (forward){
            case 1:

            return;
            case 2:
            
            return;
            case 3:
            
            return;
            case 4:
            
            return;
        }
    }
    public void PlayerWalk(){
        switch (forward){
            case 1:

            return;
            case 2:
            
            return;
            case 3:
            
            return;
            case 4:
            
            return;
        }
    }
    public void PlayerRun(){
        switch (forward){
            case 1:

            return;
            case 2:
            
            return;
            case 3:
            
            return;
            case 4:
            
            return;
        }
    }

    private void OnTriggerStay(Collider other) {
        
    }
}
