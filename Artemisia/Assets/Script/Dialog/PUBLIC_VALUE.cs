using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class PUBLIC_VALUE: MonoBehaviour
{
    //對話json結構
     [SerializeField]
    ///檔案路徑
    [Serializable]
    public class dialog{
        public int state;
        public int level;
        public int Object;
        public int figure_id;
        public int img_index;
        public int operating_status;
        public string content;
    }
    [Serializable]
    public class dialogList{
        public List<dialog> dialogs;
    }
    //----------------------------------------------------------------------------------------------------------------
    //狀態json結構
     [SerializeField]
    ///檔案路徑
    [Serializable]
    public class status{
        public int level;
        public int current_status;
        public int max_status;
        public bool isfinished;
    }
    [Serializable]
    public class statusList{
        public status[] status;
    }
    public class statusOperator:all_json{


        public bool GetFinished(int level){
            return all_json_list.status_list.status[level].isfinished;
        }
        /// <summary>
        /// 取得目前狀態
        /// </summary>   
        public int GetState(int level){
           foreach (var item in PUBLIC_VALUE.all_json_list.status_list.status)
           {
               if(item.level == level){
                   return item.current_status;
               }
           }
           return 0;
        } 
        /// <summary>
        /// 設定關卡狀態(未寫入json)
        /// </summary>  
        public void SetState(int level,int state){
           all_json_list.status_list.status[level].current_status = state;
        }
        /// <summary>
        /// 設定關卡狀態(寫入json)
        /// </summary>
        public void WriteState(int level,int state){
            all_json_list.status_list.status[level].current_status = state;
            string json="";

            if(state == all_json_list.status_list.status[level].max_status) {
                all_json_list.status_list.status[level].isfinished = true;
                record.record_list.records[(Int32.Parse(all_json_list.record_id))-1].progress = level+1;
                json = JsonUtility.ToJson(record.record_list);
                File.WriteAllText( Application.dataPath+"/StreamingAssets/record.json",json);

            }

            json = JsonUtility.ToJson(all_json_list.status_list);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/status.json",json);
        }

        public void ResetStateJson(string id){
            string url = Application.dataPath+"/StreamingAssets/Initial/status.json";
            statusList temp = new statusList();
            string json;
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<statusList>(json);
            }

            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+id+"/status.json",json);
        }
    }
    //----------------------------------------------------------------------------------------------------------------
    //角色json
    [SerializeField]
    ///檔案路徑
    [Serializable]
    public class figure{
        public int id;
        public string name;
        public string img;
    }
    [Serializable]
    public class figureList{
        public figure[] figures;
    }
    //---------------------------------------------------------------------------------------
     //對話json結構
     [SerializeField]
    ///檔案路徑
    [Serializable]
    public class option{
        public int state;
        public int level;
        public int figure_id;
        public int img_index;
        public int count;
        public string content;
        public int return_state;
        public bool last_one_false;
    }
    [Serializable]
    public class optionList{
        public option[] options;
    }
    //---------------------------------------------------------------------------------------
     //工具json結構
     [SerializeField]
    ///檔案路徑
    [Serializable]
    public class tool{
        public int id;
        //public int area;
        public string name;
        public string introduce;
        public bool is_one_time;
        //public bool owned;
        public string img;
        public string gameobject;
    }
    [Serializable]
    public class toolList{
        public List<tool> tools;
        //public tool[] tools;
    }

    public class toolOperator:all_json{
        /// <summary>
        /// 取得擁有工具數量
        /// </summary>    
        public int GetToolCount(){
            //若未讀取工具json(避免因未讀檔形況而發生錯誤)
            // if(PUBLIC_VALUE.all_json_list.tools_list == null){
            //     ReadTool(Application.dataPath+"/StreamingAssets/tool.json");
            // }
            int count = 0;
            foreach (var item in all_json_list.generate_info_list1.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                    count++;
                }
            }
            foreach (var item in all_json_list.generate_info_list2.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                    count++;
                }
            }
            foreach (var item in all_json_list.generate_info_list0.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                    count++;
                }
            }
             foreach (var item in all_json_list.generate_info_list3.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                    count++;
                }
            }
            
            // foreach (var tool in all_json_list.tools_list.tools)
            // {
            //     if(tool.owned == true){
            //         count++;
            //     }
            // }
            return count;
        }

        /// <summary>
        /// 將已擁有的工具合併回傳
        /// </summary>   
        public List<PUBLIC_VALUE.tool> GetOwnedTool(){
            List<tool> owned_tool = new List<tool>();
            tool tool_item = new tool();
            int count = all_json_list.tools_list.tools.Count;
            foreach (var item in all_json_list.generate_info_list1.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                  for(int i =0;i < count;i++){
                      if(item.id == all_json_list.tools_list.tools[i].id){
                        owned_tool.Add(all_json_list.tools_list.tools[i]);
                      }
                  }
                }
            }
            foreach (var item in all_json_list.generate_info_list2.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                  for(int i =0;i < count;i++){
                      if(item.id == all_json_list.tools_list.tools[i].id){
                        owned_tool.Add(all_json_list.tools_list.tools[i]);
                      }
                  }
                }
            }

            foreach (var item in all_json_list.generate_info_list3.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                  for(int i =0;i < count;i++){
                      if(item.id == all_json_list.tools_list.tools[i].id){
                        owned_tool.Add(all_json_list.tools_list.tools[i]);
                      }
                  }
                }
            }
              foreach (var item in all_json_list.generate_info_list0.generate_infos)
            {
                if(item.owned == true && item.id >= 101 && item.id <= 199){
                  for(int i =0;i < count;i++){
                      if(item.id == all_json_list.tools_list.tools[i].id){
                        owned_tool.Add(all_json_list.tools_list.tools[i]);
                      }
                  }
                }
            }
            
            // int count = all_json_list.tools_list.tools.Count;
            // for(int i = 0;i < count;i++){
            //     tool tool  = all_json_list.tools_list.tools[i];
            //     if(tool.owned == true){
            //         owned_tool.Add(tool);
            //     }
            // }
            return owned_tool;
        }
        
        /// <summary>
        /// 取得指定工具資料
        /// </summary>    
        public PUBLIC_VALUE.tool GetToolInfo(int id){
            //若未讀取工具json(避免因未讀檔形況而發生錯誤)
            // if(PUBLIC_VALUE.all_json_list.tools_list == null){
            //     ReadTool(Application.dataPath+"/StreamingAssets/tool.json");
            // }
            foreach (var tool in all_json_list.tools_list.tools)
            {
                if(tool.id == id){
                    return tool;
                }
            }
            return null;
        }

        /// <summary>
        /// 取得道具
        /// </summary>    
        public void SetObtainTool(int cid,int area){
            //若未讀取工具json(避免因未讀檔形況而發生錯誤)，如果每個都要確認(感覺有點多此一舉)的話可以改成function
            // if(PUBLIC_VALUE.all_json_list.tools_list == null){
            //     ReadTool(Application.dataPath+"/StreamingAssets/tool.json");
            // }
            generate_infoList generate_info_list = new generate_infoList();
            switch(area){
                case 0:
                    generate_info_list = all_json_list.generate_info_list0;
                    break;
                case 1:
                    generate_info_list = all_json_list.generate_info_list1;
                    break;
                case 2:
                    generate_info_list = all_json_list.generate_info_list2;
                    break;
                case 3:
                    generate_info_list = all_json_list.generate_info_list3;
                    break;
            }
            foreach (var tool in generate_info_list.generate_infos)
            {
                //改成cid才對
                if(tool.cid == cid){              
                    tool.owned = true;
                }
            }
            //WriteToolJson();
            PUBLIC_VALUE.generate_operator.WriteGenerateInfoJson(area);
        }

        /// <summary>
        /// 刪除道具(包括生成json中的資料)，並寫入json
        /// </summary>    
        public void DeleteTool(int cid){
            //PUBLIC_VALUE.toolList tool_list = tools_list;
            //若未讀取工具json(避免因未讀檔形況而發生錯誤)，如果每個都要確認(感覺有點多此一舉)的話可以改成function
            // if(PUBLIC_VALUE.all_json_list.tools_list == null){
            //     ReadTool(Application.dataPath+"/StreamingAssets/tool.json");
            // }
            for(int i = 0;i <= 2;i++){
                if(DeleteToolOperator(cid,i) == true) break;
            }
            
            //WriteToolJson();
           
        }

        bool DeleteToolOperator(int cid,int area){
            generate_infoList g = new generate_infoList();
            switch(area){
                case 0:
                    g = all_json_list.generate_info_list0;
                    break;
                 case 1:
                    g = all_json_list.generate_info_list1;
                    break;
                case 2:
                    g = all_json_list.generate_info_list2;
                    break;
                case 3:
                    g = all_json_list.generate_info_list3;
                    break;
            }
            int tool_count =  all_json_list.tools_list.tools.Count;
            int count = g.generate_infos.Count;
            for(int i = 0;i < count;i++){
                if(g.generate_infos[i].cid == cid){
                    print("find generate cid");
                    int g_id = g.generate_infos[i].id;
                    for(int j = 0;j < tool_count;j++){
                        if(all_json_list.tools_list.tools[j].id == g_id){
                            print("find tool id");
                            //需要區分是否為一次性道具，若為一次性道具需要將generate_info.json中該筆id資料刪除，不是則只要將owned改成false
                            if(all_json_list.tools_list.tools[j].is_one_time == true){
                                print("true");
                                generate_operator.DelectGenerateInfo(area,cid);
                            }
                            else{
                                switch(area){
                                    case 0:
                                        all_json_list.generate_info_list0.generate_infos[i].owned = false;  
                                        break;
                                    case 1:
                                        all_json_list.generate_info_list1.generate_infos[i].owned = false;  
                                        break;
                                    case 2:
                                        all_json_list.generate_info_list2.generate_infos[i].owned = false;  
                                        break;
                                    case 3:
                                        all_json_list.generate_info_list3.generate_infos[i].owned = false;  
                                        break;
                                }
                            }
                        }
                    }
                    PUBLIC_VALUE.generate_operator.WriteGenerateInfoJson(area);
                    //找到就break;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 取的指定工具狀態
        /// </summary>  
        public bool GetToolState(int cid,int level){
            generate_infoList g = new generate_infoList();
            switch(level){
                case 0:
                    g = all_json_list.generate_info_list0;
                    break;
                case 1:
                    g = all_json_list.generate_info_list1;
                    break;
                case 2:
                    g = all_json_list.generate_info_list2;
                    break;
                case 3:
                    g = all_json_list.generate_info_list3;
                    break;
            }
            int count = g.generate_infos.Count;
            bool owned = false;
            foreach (var item in g.generate_infos)
            {
                 if(item.cid == cid){
                    owned = item.owned;
                    break;
                }
            }
            return owned;
        }

        /// <summary>
        /// 取的指定工具數量
        /// </summary>  
        public int GetSingleToolCount(int id,int area){
            generate_infoList g = new generate_infoList();
            switch(area){
                case 0:
                    g = all_json_list.generate_info_list0;
                    break;
                case 1:
                    g = all_json_list.generate_info_list1;
                    break;
                case 2:
                    g = all_json_list.generate_info_list2;
                    break;
                case 3:
                    g = all_json_list.generate_info_list3;
                    break;
            }
            int count = 0;
            foreach (var item in g.generate_infos)
            {
                if(item.id == id) count++;
            }
            return count;
        }

        public void setToolGameobject(int id,string url){
            foreach (var item in all_json_list.tools_list.tools)
            {
                if(item.id == id){
                    item.gameobject = url;
                    break;
                }
            }
            string json = JsonUtility.ToJson(PUBLIC_VALUE.all_json_list.tools_list);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/tool.json",json);
        }

        /// <summary>
        /// 複寫道具json
        /// </summary>  
        // public void WriteToolJson(){
        //     string json = JsonUtility.ToJson(all_json_list.tools_list);
        //     File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/tool.json",json);
        // }

        /// <summary>
        /// 重設工具json
        /// </summary>  
        // public void ResetToolJson(){
        //     string url = Application.dataPath+"/StreamingAssets/Initial/tool.json";
        //     toolList temp = new toolList();
        //     string json;
        //     using(StreamReader stream = new StreamReader(url)){
        //         json = stream.ReadToEnd();
        //         temp = JsonUtility.FromJson<toolList>(json);
        //     }

        //     json = JsonUtility.ToJson(temp);
        //     File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/tool.json",json);
        // }

        //  public void ResetToolJson(string id){
        //     string url = Application.dataPath+"/StreamingAssets/Initial/tool.json";
        //     toolList temp = new toolList();
        //     string json;
        //     using(StreamReader stream = new StreamReader(url)){
        //         json = stream.ReadToEnd();
        //         temp = JsonUtility.FromJson<toolList>(json);
        //     }

        //     json = JsonUtility.ToJson(temp);
        //     File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+id+"/tool.json",json);
        // }

    }

    //---------------------------------------------------------------------------------------
     //自動生成json結構
     [SerializeField]
    ///檔案路徑
    [Serializable]
    public class generate_info{
        public int id;
        public int cid;
        public bool owned;
        public int level;
        public bool generate;
        public int generate_state;
        public double position_x;
        public double position_y;
        public double position_z;
        public double rotation_x;
        public double rotation_y;
        public double rotation_z;
    }
    [Serializable]
    public class generate_infoList{
        public List<generate_info> generate_infos;
    }
    public class generateOperator:all_json{
        //生成相關
        /// <summary>
        /// 刪除自動生成資料，並寫入json
        /// </summary>  
        public void DelectGenerateInfo(int area,int cid){         
            generate_infoList  generate_info_list = null;
            switch(area){
                 case 0:
                    generate_info_list = all_json_list.generate_info_list0;
                    break;
                case 1:
                    generate_info_list = all_json_list.generate_info_list1;
                    break;
                case 2:
                    generate_info_list = all_json_list.generate_info_list2;
                    break;
                case 3:
                    generate_info_list = all_json_list.generate_info_list3;
                    break;
            }
            int  count = generate_info_list.generate_infos.Count;
            for(int i = 0;i < count;i++){
                if(generate_info_list.generate_infos[i].cid == cid){
                generate_info_list.generate_infos.RemoveAt(i);
                break;
                }
            } 

           // WriteGenerateInfoJson(area,generate_info_list);
        }
        
        /// <summary>
        /// 複寫生成
        /// </summary>  
        public void WriteGenerateInfoJson(int area){
            switch(area){
                 case 0:
                    string json = JsonUtility.ToJson(PUBLIC_VALUE.all_json_list.generate_info_list0);
                    File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_0.json",json);
                    break;
                case 1:
                    json = JsonUtility.ToJson(PUBLIC_VALUE.all_json_list.generate_info_list1);
                    File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_1.json",json);
                    break;
                case 2:
                    json = JsonUtility.ToJson(PUBLIC_VALUE.all_json_list.generate_info_list2);
                    File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_2.json",json);
                    break;
                case 3:
                    json = JsonUtility.ToJson(PUBLIC_VALUE.all_json_list.generate_info_list3);
                    File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_3.json",json);
                    break;
            }
        
        }

        /// <summary>
        /// 重設生成json
        /// </summary>  
         public void ResetGenerateJson(){
            string url = Application.dataPath+"/StreamingAssets/Initial/generate_info_0.json";
            generate_infoList temp = new generate_infoList();
            string json;
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_0.json",json);
            
            
            url = Application.dataPath+"/StreamingAssets/Initial/generate_info_1.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_1.json",json);

            url = Application.dataPath+"/StreamingAssets/Initial/generate_info_2.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_2.json",json);

            url = Application.dataPath+"/StreamingAssets/Initial/generate_info_3.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_3.json",json);

            url = Application.dataPath+"/StreamingAssets/Initial/mist_generate_info.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/mist_generate_info.json",json);

        }

          public void ResetGenerateJson(string id){
            string url = Application.dataPath+"/StreamingAssets/Initial/generate_info_0.json";
            generate_infoList temp = new generate_infoList();
            string json;
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+id+"/generate_info_0.json",json);

            url = Application.dataPath+"/StreamingAssets/Initial/generate_info_1.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+id+"/generate_info_1.json",json);

            url = Application.dataPath+"/StreamingAssets/Initial/generate_info_2.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+id+"/generate_info_2.json",json);

            url = Application.dataPath+"/StreamingAssets/Initial/generate_info_3.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+id+"/generate_info_3.json",json);

            url = Application.dataPath+"/StreamingAssets/Initial/mist_generate_info.json";
            temp = new generate_infoList();
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<generate_infoList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+id+"/mist_generate_info.json",json);
        }

        /// <summary>
        /// 取得生成狀態
        /// </summary>  
        public int GetGenerateState(int cid,int area){
            generate_infoList temp = new generate_infoList();
            switch(area){
                case 0:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list0;
                    break;
                case 1:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list1;
                    break;
                case 2:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list2;
                    break;
                case 3:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list3;
                    break;
            }
            foreach (var item in temp.generate_infos)
            {
                if(item.cid == cid){
                    return item.generate_state;
                }
            }
            return 0;
        }
        /// <summary>
        /// 取得是否被生成
        /// </summary>  
        public bool GetGenerate(int cid,int area){
            generate_infoList temp = new generate_infoList();
            switch(area){
                case 0:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list0;
                    break;
                case 1:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list1;
                    break;
                case 2:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list2;
                    break;
                case 3:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list3;
                    break;
            }
            foreach (var item in temp.generate_infos)
            {
                if(item.cid == cid){
                    return item.generate;
                }
            }
            return false;
        }
        /// <summary>
        /// 將是否生成改成false
        /// </summary>  
        public void SetNotGenerate(int cid,int area){
            generate_infoList temp = new generate_infoList();
            switch(area){
                case 0:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list0;
                    break;
                case 1:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list1;
                    break;
                case 2:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list2;
                    break;
                case 3:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list3;
                    break;
            }
            foreach (var item in temp.generate_infos)
            {
                if(item.cid == cid){
                    item.generate = false;
                    break;
                }
            }
            string json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets//Record"+all_json_list.record_id+"/generate_info_"+area+".json",json);

        }
         /// <summary>
        /// 將是否生成改成true
        /// </summary>  
        public void SetGenerate(int cid,int area){
            generate_infoList temp = new generate_infoList();
            switch(area){
                case 0:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list0;
                    break;
                case 1:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list1;
                    break;
                case 2:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list2;
                    break;
                case 3:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list3;
                    break;
            }
            foreach (var item in temp.generate_infos)
            {
                if(item.cid == cid){
                    item.generate = true;
                    break;
                }
            }
            string json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets//Record"+all_json_list.record_id+"/generate_info_"+area+".json",json);

        }
        /// <summary>
        /// 設定generate_state
        /// </summary>  
        public void SetGenerateState(int cid,int area,int state){
            generate_infoList temp = new generate_infoList();
            switch(area){
                case 0:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list0;
                    break;
                case 1:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list1;
                    break;
                case 2:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list2;
                    break;
                case 3:
                    temp = PUBLIC_VALUE.all_json_list.generate_info_list3;
                    break;
            }
            foreach (var item in temp.generate_infos)
            {
                if(item.cid == cid){
                    item.generate_state = state;
                    break;
                }
            }
             string json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/generate_info_"+area+".json",json);
        }

        public bool GetMistGenerateState(int cid){
            foreach (var item in PUBLIC_VALUE.all_json_list.mist_generate_info_list.generate_infos)
            {
                if(item.cid == cid){
                    return item.generate;
                    //break;
                }
            }
            return false;
        }
        public void SetMistGenerateState(int cid){
             foreach (var item in PUBLIC_VALUE.all_json_list.mist_generate_info_list.generate_infos)
            {
                if(item.cid == cid){
                    item.generate = false;
                    break;
                }
            }
            string json = JsonUtility.ToJson(PUBLIC_VALUE.all_json_list.mist_generate_info_list);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Record"+all_json_list.record_id+"/mist_generate_info.json",json);
        }
        public void SetGeneratePosition(int level,int id,int cid,Vector3 position){
            generate_infoList temp = new generate_infoList();
            string json="";
            string url = "";
            switch(level){
                case 0:
                    url = Application.dataPath+"/StreamingAssets/Initial/generate_info_0.json";
                    using(StreamReader stream = new StreamReader(url)){
                        json = stream.ReadToEnd();
                        temp = JsonUtility.FromJson<generate_infoList>(json);
                    }
                    break;
                case 1:
                    url = Application.dataPath+"/StreamingAssets/Initial/generate_info_1.json";
                    using(StreamReader stream = new StreamReader(url)){
                        json = stream.ReadToEnd();
                        temp = JsonUtility.FromJson<generate_infoList>(json);
                    }
                    break;
                case 2:  
                    url = Application.dataPath+"/StreamingAssets/Initial/generate_info_2.json";
                    using(StreamReader stream = new StreamReader(url)){
                        json = stream.ReadToEnd();
                        temp = JsonUtility.FromJson<generate_infoList>(json);
                    }
                    break;
                case 3:
                    url = Application.dataPath+"/StreamingAssets/Initial/generate_info_3.json";
                    using(StreamReader stream = new StreamReader(url)){
                        json = stream.ReadToEnd();
                        temp = JsonUtility.FromJson<generate_infoList>(json);
                    }
                    break;
            }

             foreach (var item in temp.generate_infos)
            {
                if(item.cid == cid &&item.id == id && item.level == level){
                   item.position_x = position.x;
                   item.position_y = position.y;
                   item.position_z = position.z;
                }
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/Initial/generate_info_"+level+".json",json);
            
        }
    }

//----------------------------------------------------------------------------------------------------------------
    //遊玩紀錄json
    [SerializeField]
    ///檔案路徑
    [Serializable]
    public class Record{
        public string id;
        public bool is_new;
        public int death_count;
        public int play_time;
        public int progress;
        public double position_x;
        public double position_y;
        public double position_z;
    }
     public class recordList{
        public List<Record> records;
    }
    public class recordOperator{
        public recordList record_list = new recordList();
        public recordOperator(){
            //進行json讀取
            string url = Application.dataPath+"/StreamingAssets/record.json";
            using(StreamReader stream = new StreamReader(url)){
                string json = stream.ReadToEnd();
                record_list = JsonUtility.FromJson<recordList>(json);
            }
        }
        /// <summary>
        /// 刪除遊戲紀錄(並寫入json)
        /// </summary> 
        public void delectRecord(string id){
            int record_id = (Int32.Parse(id))-1;
           // tool_operator.ResetToolJson(id);
           print("id:"+id);
            generate_operator.ResetGenerateJson(id);
            this.record_list.records[record_id].is_new = true;
            this.record_list.records[record_id].play_time = 0;
            this.record_list.records[record_id].death_count = 0;
            this.record_list.records[record_id].progress = 1;
            this.record_list.records[record_id].position_x = -64.3;
             this.record_list.records[record_id].position_y = 0;
              this.record_list.records[record_id].position_z = -111.8;
            status_operator.ResetStateJson(id);


            string url = Application.dataPath+"/StreamingAssets/Initial/tool.json";
            toolList temp = new toolList();
            string json;
            using(StreamReader stream = new StreamReader(url)){
                json = stream.ReadToEnd();
                temp = JsonUtility.FromJson<toolList>(json);
            }
            json = JsonUtility.ToJson(temp);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/tool.json",json);
            WriteRecordJson(id);
        }

        /// <summary>
        /// 設定開始遊戲狀態(並寫入json)
        /// </summary>  
        public void SetStartPlay(string id){
            this.record_list.records[(Int32.Parse(id))-1].is_new = false;
            string json = JsonUtility.ToJson(this.record_list);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/record.json",json);
        }

        /// <summary>
        /// 複寫紀錄
        /// </summary>  
        public void WriteRecordJson(string id){
            string json = JsonUtility.ToJson(this.record_list);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/record.json",json);
        }

        /// <summary>
        /// 取得主角生成座標
        /// </summary>
        public Vector3 GetPlayerPosition(){
            int id = Int32.Parse(PUBLIC_VALUE.all_json_list.record_id)-1;
            Vector3 position = new Vector3((float)record_list.records[id].position_x,(float)record_list.records[id].position_y,(float)record_list.records[id].position_z);
            return position;
        }
        /// <summary>
        /// 設定主角生成座標
        /// </summary>
        public void SetPlayerPosition(Vector3 position){
            int id = Int32.Parse(PUBLIC_VALUE.all_json_list.record_id)-1;
            record_list.records[id].position_x = position.x;
            record_list.records[id].position_y = position.y;
            record_list.records[id].position_z = position.z;
            string json = JsonUtility.ToJson(this.record_list);
            File.WriteAllText( Application.dataPath+"/StreamingAssets/record.json",json);

        }

        public void AddDeathCount(){
             int id = Int32.Parse(PUBLIC_VALUE.all_json_list.record_id)-1;
             record_list.records[id].death_count++;
        }

        public void ChangePlayerTime(int second){
            int id = Int32.Parse(PUBLIC_VALUE.all_json_list.record_id)-1;
            record_list.records[id].play_time += second;

        }
     }
    //---------------------------------------------------------------------
    public class all_json{      
        //遊玩紀錄的id
        public string record_id;  
        //第一關對話json儲存
        public dialogList dialogs_0 = new dialogList();
        public dialogList dialogs_1 = new dialogList();
        public dialogList dialogs_2 = new dialogList();

        public dialogList dialogs_3 = new dialogList();
        //目前進行對話
        public dialogList current_dialog_list = new dialogList();
        //選項json儲存
        public optionList options_list = new optionList();
        //角色json儲存
        public figureList figures_list = new figureList();
        //狀態json儲存
        public statusList status_list = new statusList();
        //工具json儲存
        public toolList tools_list = new toolList();
        //生成json儲存(會分區)
        public generate_infoList generate_info_list0 = new generate_infoList();
        public generate_infoList generate_info_list1 = new generate_infoList();
        public generate_infoList generate_info_list2 = new generate_infoList();
        public generate_infoList generate_info_list3 = new generate_infoList();
        public generate_infoList mist_generate_info_list = new generate_infoList();
        public all_json(){
            string url = Application.dataPath+"/StreamingAssets/Initial/dialog_level0.json";
            using(StreamReader stream = new StreamReader(url)){
                string json = stream.ReadToEnd();
                dialogs_0 = JsonUtility.FromJson<dialogList>(json);
            }

            url = Application.dataPath+"/StreamingAssets/Initial/dialog_level1.json";
            using(StreamReader stream = new StreamReader(url)){
                string json = stream.ReadToEnd();
                dialogs_1 = JsonUtility.FromJson<dialogList>(json);
            }

            url = Application.dataPath+"/StreamingAssets/Initial/dialog_level2.json";
            using(StreamReader stream = new StreamReader(url)){
                string json = stream.ReadToEnd();
                dialogs_2 = JsonUtility.FromJson<dialogList>(json);
            }

            url = Application.dataPath+"/StreamingAssets/Initial/dialog_level3.json";
            using(StreamReader stream = new StreamReader(url)){
                string json = stream.ReadToEnd();
                dialogs_3 = JsonUtility.FromJson<dialogList>(json);
            }
            url = Application.dataPath+"/StreamingAssets/Initial/option.json";
            using(StreamReader stream = new StreamReader(url)){
                string json = stream.ReadToEnd();
                options_list = JsonUtility.FromJson<optionList>(json);
            }

            url = Application.dataPath+"/StreamingAssets/Initial/figure.json";
            using(StreamReader stream = new StreamReader(url)){
                string json = stream.ReadToEnd();
                figures_list = JsonUtility.FromJson<figureList>(json);
            }

           
        }

        /// <summary>
        /// 設定開始遊戲的json
        /// </summary>
        public void StarPlayJson(string id,bool is_new_game){
             record_id = id;
            if(is_new_game == true){
                //tool_operator.ResetToolJson();
                generate_operator.ResetGenerateJson();
                //record.SetStartPlay(id);
                
            }
            //因為害怕新開遊戲的json不是初始的，所以新開的遊戲json要重新復寫
            if(id != ""){
                //新開遊戲json複寫
                //ResetToolJson();

                //進行各json讀取
               // string url = Application.dataPath+"/StreamingAssets/Record"+record_id+"/tool.json";
               string url = Application.dataPath+"/StreamingAssets/tool.json";
                using(StreamReader stream = new StreamReader(url)){
                    string json = stream.ReadToEnd();
                    tools_list = JsonUtility.FromJson<toolList>(json);
                }
                url = Application.dataPath+"/StreamingAssets/Record"+record_id+"/mist_generate_info.json";
                using(StreamReader stream = new StreamReader(url)){
                    string json = stream.ReadToEnd();
                    mist_generate_info_list = JsonUtility.FromJson<generate_infoList>(json);
                }

                url = Application.dataPath+"/StreamingAssets/Record"+record_id+"/generate_info_0.json";
                using(StreamReader stream = new StreamReader(url)){
                    string json = stream.ReadToEnd();
                    generate_info_list0 = JsonUtility.FromJson<generate_infoList>(json);
                }

                url = Application.dataPath+"/StreamingAssets/Record"+record_id+"/generate_info_1.json";
                using(StreamReader stream = new StreamReader(url)){
                    string json = stream.ReadToEnd();
                    generate_info_list1 = JsonUtility.FromJson<generate_infoList>(json);
                }

                url = Application.dataPath+"/StreamingAssets/Record"+record_id+"/generate_info_2.json";
                using(StreamReader stream = new StreamReader(url)){
                    string json = stream.ReadToEnd();
                    generate_info_list2 = JsonUtility.FromJson<generate_infoList>(json);
                }

                  url = Application.dataPath+"/StreamingAssets/Record"+record_id+"/generate_info_3.json";
                using(StreamReader stream = new StreamReader(url)){
                    string json = stream.ReadToEnd();
                    generate_info_list3 = JsonUtility.FromJson<generate_infoList>(json);
                }

                url = Application.dataPath+"/StreamingAssets/Record"+record_id+"/status.json";
                using(StreamReader stream = new StreamReader(url)){
                    string json = stream.ReadToEnd();
                    status_list = JsonUtility.FromJson<statusList>(json);
                }
            }
        }

        //角色相關
        /// <summary>
        /// 取得角色名稱及顏色及立繪圖片
        /// </summary>
        public List<string> GetDialogFigureInfo(int id){
         List<string> result = new List<string>();
            //如果未讀取過角色json或PUBLIC_VALUE.figures_list是空的，去讀取檔案(避免因未讀檔形況而發生錯誤)
            // if(PUBLIC_VALUE.all_json_list.figures_list == null){
            //     ReadFigure(Application.dataPath+"/StreamingAssets/figures.json");
            // }
            int count = figures_list.figures.Length-1;
            for(int i = 0;i<=count;i++){
                if(figures_list.figures[i].id == id){
                    result.Insert(0,figures_list.figures[i].name);
                    result.Insert(1,figures_list.figures[i].img);
                    return result;
                }
            }
            return null;
        }



    }

    static public bool start_game = false;
    static public all_json all_json_list = new all_json();  
    static public toolOperator tool_operator = new toolOperator();  
    static public generateOperator generate_operator = new generateOperator();  
    static public recordOperator record = new recordOperator();
    static public statusOperator  status_operator= new statusOperator();
   
   
}
