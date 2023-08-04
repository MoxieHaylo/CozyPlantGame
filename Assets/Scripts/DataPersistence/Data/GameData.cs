using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class GameData
{

    public float smallPlantInitTime;
    public DateTime smallPlantInitDate;

    public GameData()
    {
        this.smallPlantInitTime = 0f;
    }


}
