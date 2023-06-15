using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GetLocalWeatherData weatherData;
    public bool DayTime = true;
    public GameObject dayLights;
    public GameObject nightNights;
    public bool BackgroundSet = false;
    public GameObject clearDV;
    public GameObject clearNV;
    public GameObject cloudDV;
    public GameObject cloudNV;
    public GameObject drizDV;
    public GameObject drizNV;
    public GameObject rainDV;
    public GameObject rainNV;
    public GameObject thunDV;
    public GameObject thunNV;
    public GameObject snowDV;
    public GameObject snowNV;
    public GameObject atmoDV;
    public GameObject atmosNV;

    // Start is called before the first frame update
    void Start()
    {
        GetAndSetTime();
        //SetBackground();
    }

    // Update is called once per frame
    void Update()
    {
        if (BackgroundSet == false)
        {
            StartCoroutine(SetBackground());
        }

        //if(BackgroundSet==true&&Input.GetKeyUp(KeyCode.Space))
        //{
        //    Debug.Log("Test refresh");
        //    BackgroundSet = false;
        //    StartCoroutine(SetBackground());        
        //}

    }

    IEnumerator SetUpScene()
    {
        SetBackground();
        yield break;
    }
    void GetAndSetTime()
    {
        int sysHour = System.DateTime.Now.Hour;
        if (sysHour == 6 || sysHour == 7 || sysHour == 8 || sysHour == 9 || sysHour == 10 || sysHour == 11 || sysHour == 12 || sysHour == 13 || sysHour == 14 || sysHour == 15 || sysHour == 16 || sysHour == 17)
        {
            DayTime = true;
        }
        else if (sysHour == 18 || sysHour == 19 || sysHour == 20 || sysHour == 21 || sysHour == 22 || sysHour == 23 || sysHour == 24 || sysHour == 1 || sysHour == 2 || sysHour == 3 || sysHour == 4 || sysHour == 5)
        {
            DayTime = false;
        }
        Debug.Log(sysHour);
        Debug.Log(DayTime);
        if (DayTime == true)
        {
            dayLights.SetActive(true);
            nightNights.SetActive(false);
        }
        else
        {
            dayLights.SetActive(false);
            nightNights.SetActive(true);
        }
    }

    IEnumerator SetBackground()
    {
        Debug.Log("Setting BG");
        if (weatherData.Clear == true && DayTime == true)
        {
            clearDV.SetActive(true);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Clear == true && DayTime == false)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(true);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Clouds == true && DayTime == true)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(true);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Clouds == true && DayTime == false)
        {
            Debug.Log("Night clouds");
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(true);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Drizzle == true && DayTime == true)
        {
            Debug.Log("Day drizzle");
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(true);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Drizzle == true && DayTime == false)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizNV.SetActive(true);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
            Debug.Log("night drizzle");
        }
        else if (weatherData.Rain == true && DayTime == true)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(true);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Rain == true && DayTime == false)
        {
            Debug.Log("Night Rain");
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(true);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Thunder == true && DayTime == true)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(true);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Thunder == true && DayTime == false)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(true);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Snow == true && DayTime == true)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(true);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Snow == true && DayTime == false)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Atmosphere == true && DayTime == true)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(true);
            atmosNV.SetActive(false);
        }
        else if (weatherData.Atmosphere == true && DayTime == false)
        {
            clearDV.SetActive(false);
            clearNV.SetActive(false);
            cloudDV.SetActive(false);
            cloudNV.SetActive(false);
            drizDV.SetActive(false);
            drizDV.SetActive(false);
            rainDV.SetActive(false);
            rainNV.SetActive(false);
            thunDV.SetActive(false);
            thunNV.SetActive(false);
            snowDV.SetActive(false);
            snowNV.SetActive(false);
            atmoDV.SetActive(false);
            atmosNV.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        BackgroundSet = true;
        yield break;
    }

    /*
     * Check DayTime
     * - set lighting 
     * 
     * Call weather data
     * -set background 
     * 
     * Run DayTime and weather every 15 minutes of playtime
     * 
     * Call inventory data
     * 
     * Instantiate plants
     */
}
