using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public float changeTime;
    public string changeMap;
    
    private void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0){
           SceneManager.LoadScene(changeMap, LoadSceneMode.Single);
        }
    }
}
