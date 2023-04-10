using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HOT_KEY : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightControl)){
            if(Input.GetKeyDown(KeyCode.Alpha0)){
                //順移到迷霧森林第二張圖
                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(4);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha9)){
                //順移到迷霧森林第三張圖
                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(5);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha8)){
                //順移到迷霧森林第四張圖
                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(6);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha7)){
                //順移到迷霧森林第五張圖
                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(7);
            }
            else if(Input.GetKeyDown(KeyCode.P) ){
                //瞬移到迷霧森林最後一張圖
                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(8);
            }
            else if(Input.GetKeyDown(KeyCode.O)){
                //瞬移到第二關的隱藏道路旁
                Vector3 position = new Vector3((float)-96,(float)0,(float)-60);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(2);
            }
            else if(Input.GetKeyDown(KeyCode.I)){
                //階段一完成,未完成
                //教學觸發不生成
                PUBLIC_VALUE.generate_operator.SetNotGenerate(46,0);
                PUBLIC_VALUE.tool_operator.DeleteTool(46);
                PUBLIC_VALUE.tool_operator.SetObtainTool(30,0);

                PUBLIC_VALUE.status_operator.WriteState(0,1);

                Vector3 position = new Vector3((float)-68.9,(float)0,(float)-37.9);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(2); 

            }
            else if(Input.GetKeyDown(KeyCode.U)){
                //階段二完成
                PUBLIC_VALUE.status_operator.WriteState(1,4);
                //處理寶石
                PUBLIC_VALUE.tool_operator.DeleteTool(1);
                PUBLIC_VALUE.tool_operator.DeleteTool(2);
                PUBLIC_VALUE.tool_operator.DeleteTool(3);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(1,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(2,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(3,1);

                //處理祭壇
                PUBLIC_VALUE.generate_operator.SetGenerateState(8,1,1);
                PUBLIC_VALUE.generate_operator.SetGenerateState(9,1,1);
                PUBLIC_VALUE.generate_operator.SetGenerateState(10,1,1);

                //處理鬼怪
                PUBLIC_VALUE.tool_operator.DeleteTool(33);
                PUBLIC_VALUE.tool_operator.DeleteTool(31);
                PUBLIC_VALUE.tool_operator.DeleteTool(42);

                //處理進入墓園提示
                PUBLIC_VALUE.tool_operator.DeleteTool(6);

                //取得道具(靴子及鏟子)
                PUBLIC_VALUE.tool_operator.SetObtainTool(5,1);
                PUBLIC_VALUE.tool_operator.SetObtainTool(4,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(5,1);

                //處理墳墓(只刪除寶石的)
                PUBLIC_VALUE.generate_operator.SetGenerateState(14,1,1);
                PUBLIC_VALUE.generate_operator.SetGenerateState(18,1,1);
                PUBLIC_VALUE.generate_operator.SetGenerateState(23,1,1);

                //處理精靈
                PUBLIC_VALUE.generate_operator.SetGenerate(37,1);

                //移到沼澤前
                Vector3 position = new Vector3((float)-96,(float)0,(float)-20);
                PUBLIC_VALUE.record.SetPlayerPosition(position);
                SceneManager.LoadScene(2);
                
                print("第一關完成");

            }
            else if(Input.GetKeyDown(KeyCode.Y)){
                //階段三完成
                PUBLIC_VALUE.status_operator.WriteState(2,1);
                //處理日記
                PUBLIC_VALUE.tool_operator.SetObtainTool(55,2);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(55,2);

                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(3);


            }
            else if(Input.GetKeyDown(KeyCode.L)){
                //階段四完成
                PUBLIC_VALUE.status_operator.WriteState(3,6);

                //處裡日記跟草
                PUBLIC_VALUE.tool_operator.SetObtainTool(57,3);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(57,3);

                PUBLIC_VALUE.tool_operator.SetObtainTool(56,3);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(56,3);
                
                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);


                SceneManager.LoadScene(8);

            }
            else if(Input.GetKeyDown(KeyCode.K)){
                //基礎怪觸發，瞬移到中間區域前
                Vector3 position = new Vector3((float)-18.65,(float)0,(float)3);
                player.transform.position = position;

            }else if(Input.GetKeyDown(KeyCode.H)){
                //盯樹洞怪觸發，瞬移到盯樹洞怪旁邊
                Vector3 position = new Vector3((float)0.2,(float)0,(float)-30.8);
                player.transform.position = position;

            }
            else if(Input.GetKeyDown(KeyCode.M)){
                //拿到右邊兩顆寶石，瞬移到右邊

                //拿到鏟子
                PUBLIC_VALUE.tool_operator.SetObtainTool(5,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(5,1);

                //處理寶石
                PUBLIC_VALUE.tool_operator.SetObtainTool(2,1);
                PUBLIC_VALUE.tool_operator.SetObtainTool(3,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(2,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(3,1);

                //處理墳墓
                PUBLIC_VALUE.generate_operator.SetGenerateState(18,1,1);
                PUBLIC_VALUE.generate_operator.SetGenerateState(23,1,1);

                //處理第一隻怪
                PUBLIC_VALUE.tool_operator.DeleteTool(31);

                //處理進入墓園提示
                PUBLIC_VALUE.tool_operator.DeleteTool(6);

                //設定狀態
                PUBLIC_VALUE.status_operator.WriteState(1,1);
                Vector3 position = new Vector3((float)-13,(float)0,(float)-13.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);
                SceneManager.LoadScene(2);

            }
            else if(Input.GetKeyDown(KeyCode.N)){
                //拿到三顆寶石，瞬移到祭壇

                //拿到鏟子
                PUBLIC_VALUE.tool_operator.SetObtainTool(5,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(5,1);

                //處理寶石
                PUBLIC_VALUE.tool_operator.SetObtainTool(1,1);
                PUBLIC_VALUE.tool_operator.SetObtainTool(2,1);
                PUBLIC_VALUE.tool_operator.SetObtainTool(3,1);
                PUBLIC_VALUE.tool_operator.SetObtainTool(1,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(2,1);
                PUBLIC_VALUE.generate_operator.SetNotGenerate(3,1);

                //處理墳墓
                PUBLIC_VALUE.generate_operator.SetGenerateState(14,1,1);
                PUBLIC_VALUE.generate_operator.SetGenerateState(18,1,1);
                PUBLIC_VALUE.generate_operator.SetGenerateState(23,1,1);
                
                //處理第一隻怪
                PUBLIC_VALUE.tool_operator.DeleteTool(33);
                PUBLIC_VALUE.tool_operator.DeleteTool(31);
                PUBLIC_VALUE.tool_operator.DeleteTool(42);

                //處理進入墓園提示
                PUBLIC_VALUE.tool_operator.DeleteTool(6);

                //設定狀態
                PUBLIC_VALUE.status_operator.WriteState(1,2);
                
                Vector3 position = new Vector3((float)-81.1,(float)0,(float)3.3);
                PUBLIC_VALUE.record.SetPlayerPosition(position);
                SceneManager.LoadScene(2);

            }
            else if(Input.GetKeyDown(KeyCode.G)){
                //順移到迷霧森林第一張圖

                Vector3 position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                PUBLIC_VALUE.record.SetPlayerPosition(position);

                SceneManager.LoadScene(3);
            }
        }
        

       
        
        
    }
}
