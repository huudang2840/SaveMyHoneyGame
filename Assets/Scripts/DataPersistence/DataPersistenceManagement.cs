using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Utilities;
using UnityEngine.SceneManagement;


public class DataPersistenceManagement : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    
    private FileDataHandler dataHandler;

    public static DataPersistenceManagement instance { get; private set; }

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Found more than one DataPersistenceManagement in the scene");
            Destroy(this.gameObject);
            return;

        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

    }

    private void Start() {
        
    }
    public void NewGame() {
        this.gameData = new GameData();
    }

    public void LoadGame() {
        this.gameData = dataHandler.Load();
        if(this.gameData == null) {
            Debug.Log("No data was found");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Loaded HP = " + gameData.health);
    }


    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

     public void OnSceneUnloaded(Scene scene) { 
        SaveGame();
    }


    public void SaveGame() {
        if(this.gameData==null) {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be saved");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    // private void OnApplicationQuit() {
    //     // SaveGame();
    // }
    
    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData() {
        return gameData != null;
    }
}
