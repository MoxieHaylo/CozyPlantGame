using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SmallPlantGrowth : MonoBehaviour
{
    public float smallPlantInitializationTime;
    public bool smallPlantIsGrown = false;

    void Start()
    {
        smallPlantInitializationTime = Time.realtimeSinceStartup;

        //LoadGrowthStatus();
    }

    void Update()
    {
        float elapsedTime = Time.realtimeSinceStartup - smallPlantInitializationTime;
        if (!smallPlantIsGrown&& elapsedTime>=180f)
        {
            smallPlantIsGrown = true;
            Debug.Log("Grown");

            //SaveGrowthStatus();
        }
        float timeSinceInitialization = Time.realtimeSinceStartup - smallPlantInitializationTime;
        Debug.Log(timeSinceInitialization);
    }
}
