using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.EventSystems;

public class PlantA : MonoBehaviour, IDataPersistence, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SmallPlanter smallPlanter;
    public DateTime creationTime;
    public DateTime CreationTime

    {
        get { return creationTime; }
    }

    private void Awake()
    {
        smallPlanter.isAGrowing = true;
        creationTime = DateTime.Now;
        Debug.Log(creationTime);
    }

    public float initializationTime;
    public bool isGrown = false;

    public void LoadData(GameData data)
    {
        this.initializationTime = data.smallPlantInitTime;
    }
    public void SaveData(GameData data)
    {
        data.smallPlantInitTime = this.initializationTime;
    }
    void Start()
    {
        initializationTime = Time.realtimeSinceStartup;
        //LoadGrowthStatus();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        smallPlanter.nothingIsGrowing = true;
        //smallPlanter.isAGrowing = false;
        //smallPlanter.isEGrowing = false;
        //smallPlanter.isIGrowing = false;
        //smallPlanter.isNGrowing = false;
        Debug.Log("I should be harvested");
        //StartCoroutine(HarvestPlant());
    }

        void Update()
    {
        DateTime dateTime = DateTime.Now;
        float floatTime = (float)dateTime.ToOADate();
        initializationTime = floatTime;
        float elapsedTime = Time.realtimeSinceStartup - initializationTime;
        if (!isGrown&& elapsedTime>=180f)
        {
            isGrown = true;
            Debug.Log("Grown");

            //SaveGrowthStatus();
        }
        float timeSinceInitialization = Time.realtimeSinceStartup - initializationTime;
        //Debug.Log(timeSinceInitialization);
    }

    public IEnumerator HarvestPlant()
    {
        Debug.Log("Plant should be harvested now");
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
        yield break;
    }
}
