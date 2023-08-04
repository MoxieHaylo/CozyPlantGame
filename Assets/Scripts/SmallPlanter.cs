using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmallPlanter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private SmallPlant smallPlant;
    private bool hasTimePassed = false;

    public GameObject[] smallPlantPrefabs;
    public Transform spawnPos;

    public bool isNGrowing = false;
    public bool isIGrowing = false;
    public bool isEGrowing = false;
    public bool nothingIsGrowing = true;

    public bool isNight = false;
    public bool isClear = false;

    private void Awake()
    {
        smallPlant = GetComponent<SmallPlant>();
    }

    private void Start()
    {
        Invoke("SetOneMinutePassed", 60f);
    }
    private void Update()
    {
        if(hasTimePassed)
        {
            Debug.Log("we got a minute old plant");
        }

        if (isNGrowing == true || isIGrowing == true || isEGrowing == true || isIGrowing == true)
        {
            nothingIsGrowing = false;
        }
        else if (isNGrowing == false || isIGrowing == false || isEGrowing == false || isIGrowing == false)
        {
            nothingIsGrowing = true;
        }
    }
    private void SetOneMinutePassed()
    {
        hasTimePassed = true;
    }
    //Detect if the Cursor starts to pass over the GameObject
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
        Debug.Log(name + "Game object clicked");
        int currentDate = System.DateTime.Now.Day;
        Debug.Log(currentDate);

        if (isNight == false && isClear == true&& nothingIsGrowing == true)
        {
            isNGrowing = true;
            Debug.Log("N Growing");
        }
        else if (isNight == false && isClear == false&& nothingIsGrowing == true)
        {
            //isIGrowing = true;

        }

        else if (isNight == true && isClear == true&&nothingIsGrowing == true)
        {
            isIGrowing = true;
            Debug.Log("A Growing");

        }
        else if (isNight == true && isClear == false&&nothingIsGrowing==true)
        {
            isEGrowing = true;
            Debug.Log("N Growing");

        }
        else if (nothingIsGrowing==false&&isIGrowing==true)
        {
            isIGrowing = false;
            Destroy(transform.GetChild(0).gameObject);
        }
        else if (nothingIsGrowing == false && isEGrowing == true)
        {
            isEGrowing = false;
            Destroy(transform.GetChild(0).gameObject);
        }
        else if (nothingIsGrowing == false && isIGrowing == true)
        {
            isIGrowing = false;
            Destroy(transform.GetChild(0).gameObject);
        }
        else if (nothingIsGrowing == false && isNGrowing == true)
        {
            isNGrowing = false;
            Destroy(transform.GetChild(0).gameObject);
        }

        else
        {
            Debug.Log("Don't touch me");
        }

    }

    public void SpawnPlantI(int index)
    {
        Debug.Log("Making an I plant");
        Instantiate(smallPlantPrefabs[2], spawnPos);
        isIGrowing = true;
    }
}
