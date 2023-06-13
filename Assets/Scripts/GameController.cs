using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GetLocalWeatherData weatherData;
    public bool DayTime = true;
    public GameObject dayLights;
    public GameObject nightNights;

    // Start is called before the first frame update
    void Start()
    {
        GetAndSetTime();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void SetBackground()
    {
        if(weatherData.Clear==true)
        {
            //
        }
        else if(weatherData.Clouds)
        {
            //
        }
        else if(weatherData.Drizzle)
        {
            //
        }
        else if(weatherData.Rain)
        {
            //
        }
        else if(weatherData.Thunder)
        {
            //
        }
        else if(weatherData.Snow)
        {
            //
        }
        else if (weatherData.Atmosphere)
        { 
            //
        }
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
