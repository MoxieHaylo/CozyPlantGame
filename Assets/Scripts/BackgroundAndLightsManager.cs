using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAndLightsManager : MonoBehaviour
{
    public GetLocalWeatherData weatherData;
    public bool isDayTime = true;
    public GameObject dayLights;
    public GameObject nightNights;
    public bool isBackgroundSet = false;
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

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        GetAndSetTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBackgroundSet == false)
        {
            StartCoroutine(SetUpScene());
        }
    }

    IEnumerator SetUpScene()
    {
        StartCoroutine(SetBackground());
        yield break;
    }
    void GetAndSetTime()
    {
        int sysHour = System.DateTime.Now.Hour;
        if (sysHour == 6 || sysHour == 7 || sysHour == 8 || sysHour == 9 || sysHour == 10 || sysHour == 11 || sysHour == 12 || sysHour == 13 || sysHour == 14 || sysHour == 15 || sysHour == 16 || sysHour == 17)
        {
            isDayTime = true;
        }
        else if (sysHour == 18 || sysHour == 19 || sysHour == 20 || sysHour == 21 || sysHour == 22 || sysHour == 23 || sysHour == 24 || sysHour == 1 || sysHour == 2 || sysHour == 3 || sysHour == 4 || sysHour == 5)
        {
            isDayTime = false;
        }
        Debug.Log(sysHour);
        Debug.Log(isDayTime);
        if (isDayTime == true)
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
        ClearBackground();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Setting BG");
        if (weatherData.isClear == true && isDayTime == true)
        {
            clearDV.SetActive(true);
        }
        else if (weatherData.isClear == true && isDayTime == false)
        {
            clearNV.SetActive(true);
        }
        else if (weatherData.isCloudy == true && isDayTime == true)
        {
            cloudDV.SetActive(true);
        }
        else if (weatherData.isCloudy == true && isDayTime == false)
        {
            cloudNV.SetActive(true);
        }
        else if (weatherData.isDrizzlling == true && isDayTime == true)
        {
            drizDV.SetActive(true);
        }
        else if (weatherData.isDrizzlling == true && isDayTime == false)
        {
            drizNV.SetActive(true);
        }
        else if (weatherData.isRaining == true && isDayTime == true)
        {
            rainDV.SetActive(true);
        }
        else if (weatherData.isRaining == true && isDayTime == false)
        {
            rainNV.SetActive(true);
        }
        else if (weatherData.isThundering == true && isDayTime == true)
        {
            thunDV.SetActive(true);
        }
        else if (weatherData.isThundering == true && isDayTime == false)
        {
            thunNV.SetActive(true);
        }
        else if (weatherData.isSnowing == true && isDayTime == true)
        {
            snowDV.SetActive(true);
        }
        else if (weatherData.isSnowing == true && isDayTime == false)
        {
            snowNV.SetActive(true);
        }
        else if (weatherData.isAtmosphere == true && isDayTime == true)
        {
            atmoDV.SetActive(true);
        }
        else if (weatherData.isAtmosphere == true && isDayTime == false)
        {
            atmosNV.SetActive(true);
        }
        yield return new WaitForSeconds(1); 
        isBackgroundSet = true;
        canvas.SetActive(false);
        yield break;
    }

    void ClearBackground()    
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
