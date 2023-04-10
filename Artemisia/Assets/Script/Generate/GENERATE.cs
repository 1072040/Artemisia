using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GENERATE : MonoBehaviour
{
    const int area_count = 3;
    private bool[] generated = new bool[area_count];
    private GameObject player;
    int timecount = 0;
    void Start(){
        player = GameObject.Find("Player");
        for(int i = 0;i < area_count;i++){
            generated[i] = false;
        }
        foreach (var tool in PUBLIC_VALUE.all_json_list.tools_list.tools){
            Resources.Load(tool.gameobject);
        }

    }
    public void Resurrection(int resurrection){
        Vector3 position = new Vector3((float)-64.3,(float)0,(float)-111.8);
        switch (resurrection){
            case 1:
                position = new Vector3((float)-64.3,(float)0,(float)-111.8);
                break;
            case 2:
                position = new Vector3((float)-13,(float)0,(float)-13.8);
                break;
            case 3:
                position = new Vector3((float)-56.4, (float)0,(float)5.654978);
                break;
             case 4:
                position = new Vector3((float)-55.5,(float)0,(float)-40.5);
                break;
             case 5:
                position = new Vector3((float)-125.8,(float)0,(float)-16.15);
                break;
            case 6:
                position = new Vector3((float)-156.8,(float)0,(float)-21.8);
                break;
            case 7: 
                position = new Vector3((float)-2.67522,(float)0,(float)0.8);
                break; 

        }
        PUBLIC_VALUE.record.SetPlayerPosition(position);
    }
   public IEnumerator Generate(int area,int last_area){
        string delect_area = "";
       //刪除上一區物件
        switch(last_area){
            case 0:
                delect_area = "Level_0";
                
                break;
            case 1:
               delect_area = "Level_1";
                break;
            case 2:
                delect_area = "Level_2";
                break;
        
        }
        if(delect_area != ""){
            if(generated[last_area] == true){
                GameObject delect_object = GameObject.Find(delect_area);
                Destroy(delect_object);
                GameObject level =new GameObject (delect_area);
                generated[last_area] = false;
            }
            GameObject g = GameObject.Find("short");
            int count = g.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GameObject go = g.transform.GetChild(i).gameObject;
                Destroy(go);
            }

            g = GameObject.Find("middle");
            count = g.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GameObject go = g.transform.GetChild(i).gameObject;
                Destroy(go);
            }

            g = GameObject.Find("long");
            count = g.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GameObject go = g.transform.GetChild(i).gameObject;
                Destroy(go);
            }

            g = GameObject.Find("so_far");
            count = g.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GameObject go = g.transform.GetChild(i).gameObject;
                Destroy(go);
            }
        }
       
        if(area<=2){
            if(generated[area] == false){
            PUBLIC_VALUE.generate_infoList generate_info_list = new PUBLIC_VALUE.generate_infoList();
            string fater_gameobject = "";
            switch(area){
                    case 0:
                    generate_info_list = PUBLIC_VALUE.all_json_list.generate_info_list0;
                    fater_gameobject = "Level_0";
                    break;
                case 1:
                    generate_info_list = PUBLIC_VALUE.all_json_list.generate_info_list1;
                    fater_gameobject = "Level_1";
                    break;
                case 2:
                    generate_info_list = PUBLIC_VALUE.all_json_list.generate_info_list2;
                    fater_gameobject = "Level_2";
                    break;
                
            }
            timecount = 0;
            
            foreach (var generate_info in generate_info_list.generate_infos)
            {
                double[] position_info = new double[3];
                double[] rotation_info = new double[3];
                int[] info = new int[3];
                if(generate_info.level == area && generate_info.generate == true){
                    info = new int[]{generate_info.id,generate_info.cid,generate_info.level};
                    position_info = new double[]{generate_info.position_x,generate_info.position_y,generate_info.position_z};
                    rotation_info = new double[]{generate_info.rotation_x,generate_info.rotation_y,generate_info.rotation_z};
   
                    Object prefabObj =null;
                    GameObject fater;
                    fater  = GameObject.Find(fater_gameobject);
                    foreach (var tool in PUBLIC_VALUE.all_json_list.tools_list.tools){
                        if(tool.id == info[0]){
                            prefabObj = Resources.Load(tool.gameobject);
                            break;
                        }
                    }
                    if(prefabObj!= null){
                        if(timecount<=3){
                            GenerateRun(fater,position_info,rotation_info,info,prefabObj);
                            timecount++;
                        }else{
                            timecount = 0;
                            yield return new WaitForEndOfFrame();
                            GenerateRun(fater,position_info,rotation_info,info,prefabObj);
                        }
                    }
                 
                }
            }

           
            generated[area] = true;
        }
        }
    }
       public void GenerateOnce(int cid,int area){
            PUBLIC_VALUE.generate_infoList generate_info_list = new PUBLIC_VALUE.generate_infoList();
            string fater_gameobject = "";
            switch(area){
                 case 0:
                    generate_info_list = PUBLIC_VALUE.all_json_list.generate_info_list0;
                    fater_gameobject = "Level_0";
                    break;
                case 1:
                    generate_info_list = PUBLIC_VALUE.all_json_list.generate_info_list1;
                    fater_gameobject = "Level_1";
                    break;
                case 2:
                    generate_info_list = PUBLIC_VALUE.all_json_list.generate_info_list2;
                    fater_gameobject = "Level_2";
                    break;
            }
            foreach (var generate_info in generate_info_list.generate_infos)
       {
           if(generate_info.level == 1 && generate_info.cid == cid){
                int id = generate_info.id;
                int level = generate_info.level;
                double p_x = generate_info.position_x;
                double p_y = generate_info.position_y;
                double p_z = generate_info.position_z;
                double r_x = generate_info.rotation_x;
                double r_y = generate_info.rotation_y;
                double r_z = generate_info.rotation_z;
                
                foreach (var tool in PUBLIC_VALUE.all_json_list.tools_list.tools)
                    if(tool.id == id){
                        if(tool.gameobject != ""){
                            //要生成
                            GameObject fater = GameObject.Find(fater_gameobject);
                            Object prefabObj = Resources.Load(tool.gameobject);
                            //設定position
                            Vector3 position = new Vector3();
                            position.x = (float)p_x;
                            position.y = (float)p_y;
                            position.z = (float)p_z;

                            //設定rotation
                            Quaternion rotation = Quaternion.Euler ((float)r_x, (float)r_y, (float)r_z);
                         
                            GameObject go = Instantiate(prefabObj,position,rotation) as GameObject;
                            go.transform.parent = fater.transform;
                            go.GetComponentInChildren<OBJECT_INFO>().id = id;
                            go.GetComponentInChildren<OBJECT_INFO>().cid = cid;
                            go.GetComponentInChildren<OBJECT_INFO>().level = level;
                        }
                       break;
                    }
                }
           }
       }

       void GenerateRun(GameObject fater,double[] position_info,double[] rotation_info,int[] info,Object prefabObj){
            //設定position
            Vector3 position = new Vector3();
            position.x = (float)position_info[0];
            position.y = (float)position_info[1];
            position.z = (float)position_info[2];

            //設定rotation
            Quaternion rotation = Quaternion.Euler ((float)rotation_info[0], (float)rotation_info[1], (float)rotation_info[2]);
            GameObject go = Instantiate(prefabObj,position,rotation) as GameObject;
            go.transform.parent = fater.transform;
            for(int i= 0;i< go.GetComponentsInChildren<OBJECT_INFO>().Length;i++){
                go.GetComponentsInChildren<OBJECT_INFO>()[i].id = info[0];
                go.GetComponentsInChildren<OBJECT_INFO>()[i].cid = info[1];
                go.GetComponentsInChildren<OBJECT_INFO>()[i].level = info[2];
            }
           
        }
    }