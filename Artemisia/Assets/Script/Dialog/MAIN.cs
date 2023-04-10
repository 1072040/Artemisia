using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MAIN : MonoBehaviour
{
    private string dialog_url;
    private string figure_url;
    private string option_url;
    void Start()
    {
        print("main start");
        //讀取對話
        //dialog_url = Application.dataPath+"/StreamingAssets/dialog_level1.json";
        //PUBLIC_VALUE.dialog_list_level_1 =  GetComponent<JSON_OPERATOR>().ReadDialog(dialog_url);
        //PUBLIC_VALUE.all_json_list.ReadDialog(dialog_url);

        //讀取對話
        // figure_url = Application.dataPath+"/StreamingAssets/figure.json";
        // PUBLIC_VALUE.figures_list =  GetComponent<JSON_OPERATOR>().ReadFigure(figure_url);

        //讀取選項
        // option_url = Application.dataPath+"/StreamingAssets/option.json";
        // PUBLIC_VALUE.options_list =  GetComponent<JSON_OPERATOR>().ReadOption(option_url);

        //讀取工具
        // option_url = Application.dataPath+"/StreamingAssets/tool.json";
        // PUBLIC_VALUE.tools_list =  GetComponent<JSON_OPERATOR>().ReadTool(option_url);

        //讀取生成
        // option_url = Application.dataPath+"/StreamingAssets/generate_info_1.json";
        // PUBLIC_VALUE.generate_info_list1 =  GetComponent<JSON_OPERATOR>().ReadGenerateInfoJson(option_url);
        // option_url = Application.dataPath+"/StreamingAssets/generate_info_2.json";
        // PUBLIC_VALUE.generate_info_list2 =  GetComponent<JSON_OPERATOR>().ReadGenerateInfoJson(option_url);
      
    }

    // Update is called once per frame
     void Update()
    {
        
    }
}
