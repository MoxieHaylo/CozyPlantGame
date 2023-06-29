using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        Debug.LogError("Found another Data Persistence Manager in the scene");
        instance = this;
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
    }
    public void SaveGame()
    {
        //pass data to other scripts
        //save data to file handler
    }
}
