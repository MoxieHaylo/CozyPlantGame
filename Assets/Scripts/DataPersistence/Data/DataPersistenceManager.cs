using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        Debug.LogError("Found another Data Persistence Manager in the scene");
        instance = this;
    }

    private void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //load data here
        if(this.gameData==null)
        {
            Debug.Log("No data found. Starting with defaults");
            NewGame();
        }
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded plant init is "+gameData.smallPlantInitTime);
    }
    public void SaveGame()
    {
        //pass data to other scripts
        //save data to file handler
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        Debug.Log("saved init time is "+gameData.smallPlantInitTime);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects= FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
