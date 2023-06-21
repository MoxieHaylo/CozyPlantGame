using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPlantGrowth : MonoBehaviour
{
    private float initializationTime;

    void Start()
    {
        initializationTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float timeSinceInitialization = Time.realtimeSinceStartup - initializationTime;
        Debug.Log(timeSinceInitialization);
    }
}
