using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CONTINUE : MonoBehaviour
{
   public void End_continue()
   {
       SceneManager.LoadScene(0);
   }
   void Update()
   {
       if(Input.GetKeyDown(KeyCode.Escape))
       {
           SceneManager.LoadScene(0);
       }
   }
}
