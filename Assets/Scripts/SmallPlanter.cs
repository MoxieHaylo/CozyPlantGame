using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmallPlanter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private PlantA aPlant;
    private PlantI iPlant;
    private bool hasTimePassed = false;
    //public GetLocalWeatherData wd;
    public GameObject n;
    public GameObject i;
    public GameObject a;
    public GameObject e;
    //public GameObject u;
    //public GameObject x;
    //public GameObject aa;
    //public GameObject s;
    public bool isNGrowing = false;
    public bool isIGrowing = false;
    public bool isAGrowing = false;
    public bool isEGrowing = false;
    public bool nothingIsGrowing = true;

    public bool isNight = false;
    public bool isClear = false;

    private void Awake()
    {
        aPlant = GetComponent<PlantA>();
        iPlant = GetComponent<PlantI>();
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

        if (isNGrowing == true || isIGrowing == true || isEGrowing == true || isAGrowing == true)
        {
            nothingIsGrowing = false;
        }
        else if (isNGrowing == false || isIGrowing == false || isEGrowing == false || isAGrowing == false)
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
            //n.SetActive(true);
            isNGrowing = true;
            Debug.Log("N Growing");
            Instantiate(n, this.transform, worldPositionStays: false);
        }
        else if (isNight == false && isClear == false&& nothingIsGrowing == true)
        {
            //i.SetActive(true);
            //isIGrowing = true;
            //Debug.Log("I Growing");
            Instantiate(i, this.transform, worldPositionStays: false);
        }

        else if (isNight == true && isClear == true&&nothingIsGrowing == true)
        {
            //a.SetActive(true);
            isAGrowing = true;
            Debug.Log("A Growing");
            Instantiate(a, this.transform, worldPositionStays: false);
        }
        else if (isNight == true && isClear == false&&nothingIsGrowing==true)
        {
            //e.SetActive(true);
            isEGrowing = true;
            Debug.Log("N Growing");
            Instantiate(e, this.transform, worldPositionStays: false);
        }

        else
        {
            Debug.Log("Don't touch me");
            //PlantsInactive();
        }

        void PlantsInactive()
        {
            nothingIsGrowing = true;
            isNGrowing = false;
            isIGrowing = false;
            isEGrowing = false;
            isAGrowing = false;

            //n.SetActive(false);
            //i.SetActive(false);
            //e.SetActive(false);
            //a.SetActive(false);
            Destroy(transform.GetChild(0).gameObject);
        }
    }



    //Detect if a click occurs

    /*
     * On click
     * --------
     * Play animation
     * Play SFX
     * Add to inventory
     * Delete self
     */
}
