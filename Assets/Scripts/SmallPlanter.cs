using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmallPlanter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //public GameController gc;
    //public GetLocalWeatherData wd;
    public GameObject n;
    public GameObject i;
    public GameObject a;
    public GameObject e;
    public GameObject u;
    public GameObject x;
    public GameObject aa;
    public GameObject s;

    public bool isNight = false;
    public bool isClear = false;

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

        if(transform.childCount<1)
        {
            if (isNight == false && isClear == true)
            {
                Debug.Log("option 1");
                Instantiate(n, this.transform, worldPositionStays: false);
            }
            else if (isNight == false && isClear == false)
            {
                Debug.Log("option 2");
                Instantiate(i, this.transform, worldPositionStays: false);
            }
            else if (isNight == true && isClear == true)
            {
                Debug.Log("option 3");
                Instantiate(a, this.transform, worldPositionStays: false);
            }
            else if (isNight == true && isClear == false)
            {
                Debug.Log("option 4");
                Instantiate(e, this.transform, worldPositionStays: false);
            }
        }
        else
        {
            HarvestPlant();
        }

        void HarvestPlant()
        {

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
