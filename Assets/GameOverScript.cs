using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{
    
    public void ExitGame(){
         Application.Quit();
    }
    public void ReturnHome (){
       SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
       Debug.Log("KKKKK");
    }
}
