
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class SmallPlant : MonoBehaviour, IDataPersistence, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private SmallPlanter smallPlanter;
    public DateTime creationTime;
    public DateTime CreationTime
    {
        get { return creationTime; }
    }
    public float initializationTime;
    public bool isGrown = false;

    private void Awake()
    {
        smallPlanter = GetComponent<SmallPlanter>();
    }

    public void SaveData(GameData data)
    {
        data.smallPlantInitTime = this.initializationTime;
    }
    void Start()
    {
        initializationTime = Time.realtimeSinceStartup;
        //LoadGrowthStatus();

        
        smallPlanter = GetComponentInParent<SmallPlanter>();
        smallPlanter.isIGrowing = true;
        Debug.Log("I growing");
        creationTime = DateTime.Now;
        Debug.Log(creationTime);
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
        Debug.Log("I'm a plant and I'm here to grow");
        smallPlanter.isIGrowing = false;
    }

    void Update()
    {
        DateTime dateTime = DateTime.Now;
        float floatTime = (float)dateTime.ToOADate();
        initializationTime = floatTime;
        float elapsedTime = Time.realtimeSinceStartup - initializationTime;
        if (!isGrown && elapsedTime >= 60f)
        {
            isGrown = true;
            Debug.Log("Grown");
            //SaveGrowthStatus();
        }
        float timeSinceInitialization = Time.realtimeSinceStartup - initializationTime;

    }

    public void LoadData(GameData data)
    {
        this.initializationTime = data.smallPlantInitTime;
    }
}