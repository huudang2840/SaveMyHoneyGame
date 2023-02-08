using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour, IDataPersistence
{

    [Header("Menu Button") ]
    [SerializeField] private Button btnContinue; 
    [SerializeField] private Button btnNewgame; 
    public GameObject StartMenu;
    public GameObject OptionsMenu;
    public int indexScene;

    // private bool haveData = true;

    public void LoadData(GameData data) {
        this.indexScene = data.indexScene;
    }

    public void SaveData( ref GameData data) {
        data.indexScene = this.indexScene;
    }



    void Start(){
        if(!DataPersistenceManagement.instance.HasGameData()){
            btnContinue.interactable = false;
        }else {
            btnContinue.interactable = true ;
        }
    }

    public void OnNewGameClicked() {
        DisableMenuButtons();
        DataPersistenceManagement.instance.NewGame();
        SceneManager.LoadSceneAsync("Intro");
    }

    public void OnContinueGameClicked() {
        DisableMenuButtons();

        SceneManager.LoadSceneAsync(indexScene);
        
    }


    // public void PlayGame (){
    //     SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    // }

    // public void Continue (){
    //     // SceneManager.LoadScene("Map1.1", LoadSceneMode.Additive);
    // }
    
    public void isFullScreen(bool isFullScreen){
        Screen.fullScreen =  isFullScreen;

    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
        
    } 

    private void DisableMenuButtons () {
        btnContinue.interactable = false;
        btnNewgame.interactable = false;
    }

    // public void ContinueButton(){
    //     if (haveData){
    //         btnContinue.enabled = true;
    //     }
    //     else
    //     {
    //         btnContinue.enabled = false;
    //     }
    // }

    public void OpenOption(){

        StartMenu.SetActive(false);
        OptionsMenu.SetActive(true);

    }
    
    public void CloseOption(){
        OptionsMenu.SetActive(false);
        StartMenu.SetActive(true);
    }

 
    // public void ReturnHome (){
    //     Debug.Log("Return Home");
    // }
}
