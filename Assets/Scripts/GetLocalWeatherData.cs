using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json; 

public class GetLocalWeatherData : MonoBehaviour
{
    #region Weather API Key
    const string OpenWeatherAPIKey = "ad376b17409c1eb42671a2592c7fd554 ";
    #endregion 

    public enum EPhase
    {
        NotStarted,
        GetPublicIP,
        GetGeographicData,
        GetWeatherData,

        Failed,
        Succeeded
    }
    public EPhase Phase { get; private set; } = EPhase.NotStarted;

    #region Classes to make Json info usuable
    //created by Iain McManus
    class geoPluginResponse
    {
        [JsonProperty("geoplugin_request")] public string Request { get; set; }
        [JsonProperty("geoplugin_status")] public int Status { get; set; }
        [JsonProperty("geoplugin_delay")] public string Delay { get; set; }
        [JsonProperty("geoplugin_credit")] public string Credit { get; set; }
        [JsonProperty("geoplugin_city")] public string City { get; set; }
        [JsonProperty("geoplugin_region")] public string Region { get; set; }
        [JsonProperty("geoplugin_regionCode")] public string RegionCode { get; set; }
        [JsonProperty("geoplugin_regionName")] public string RegionName { get; set; }
        [JsonProperty("geoplugin_areaCode")] public string AreaCode { get; set; }
        [JsonProperty("geoplugin_dmaCode")] public string DMACode { get; set; }
        [JsonProperty("geoplugin_countryCode")] public string CountryCode { get; set; }
        [JsonProperty("geoplugin_countryName")] public string CountryName { get; set; }
        [JsonProperty("geoplugin_inEU")] public int InEU { get; set; }
        [JsonProperty("geoplugin_euVATrate")] public bool EUVATRate { get; set; }
        [JsonProperty("geoplugin_continentCode")] public string ContinentCode { get; set; }
        [JsonProperty("geoplugin_continentName")] public string ContinentName { get; set; }
        [JsonProperty("geoplugin_latitude")] public string Latitude { get; set; }
        [JsonProperty("geoplugin_longitude")] public string Longitude { get; set; }
        [JsonProperty("geoplugin_locationAccuracyRadius")] public string LocationAccuracyRadius { get; set; }
        [JsonProperty("geoplugin_timezone")] public string TimeZone { get; set; }
        [JsonProperty("geoplugin_currencyCode")] public string CurrencyCode { get; set; }
        [JsonProperty("geoplugin_currencySymbol")] public string CurrencySymbol { get; set; }
        [JsonProperty("geoplugin_currencySymbol_UTF8")] public string CurrencySymbolUTF8 { get; set; }
        [JsonProperty("geoplugin_currencyConverter")] public double CurrencyConverter { get; set; }
    }


    public class OpenWeather_Coordinates
    {
        [JsonProperty("lon")] public double Longitude { get; set; }
        [JsonProperty("lat")] public double Latitude { get; set; }
    }

    // Condition Info: https://openweathermap.org/weather-conditions
    public class OpenWeather_Condition
    {
        [JsonProperty("id")] public int ConditionID { get; set; }
        [JsonProperty("main")] public string Group { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("icon")] public string Icon { get; set; }
    }

    public class OpenWeather_KeyInfo
    {
        [JsonProperty("temp")] public double Temperature { get; set; }
        [JsonProperty("feels_like")] public double Temperature_FeelsLike { get; set; }
        [JsonProperty("temp_min")] public double Temperature_Minimum { get; set; }
        [JsonProperty("temp_max")] public double Temperature_Maximum { get; set; }
        [JsonProperty("pressure")] public int Pressure { get; set; }
        [JsonProperty("sea_level")] public int PressureAtSeaLevel { get; set; }
        [JsonProperty("grnd_level")] public int PressureAtGroundLevel { get; set; }
        [JsonProperty("humidity")] public int Humidity { get; set; }
    }

    public class OpenWeather_Wind
    {
        //[JsonProperty("speed")] public double Speed { get; set; }
        //[JsonProperty("deg")] public int Direction { get; set; }
        //[JsonProperty("gust")] public double Gust { get; set; }
    }

    public class OpenWeather_Clouds
    {
        //[JsonProperty("all")] public int Cloudiness { get; set; }
    }

    public class OpenWeather_Rain
    {
        //[JsonProperty("1h")] public int VolumeInLastHour { get; set; }
        //[JsonProperty("3h")] public int VolumeInLast3Hours { get; set; }
    }

    public class OpenWeather_Snow
    {
        //[JsonProperty("1h")] public int VolumeInLastHour { get; set; }
        //[JsonProperty("3h")] public int VolumeInLast3Hours { get; set; }
    }

    public class OpenWeather_Internal
    {
        [JsonProperty("type")] public int Internal_Type { get; set; }
        [JsonProperty("id")] public int Internal_ID { get; set; }
        [JsonProperty("message")] public double Internal_Message { get; set; }
        [JsonProperty("country")] public string CountryCode { get; set; }
        [JsonProperty("sunrise")] public int SunriseTime { get; set; }
        [JsonProperty("sunset")] public int SunsetTime { get; set; }
    }

    class OpenWeatherResponse
    {
        [JsonProperty("coord")] public OpenWeather_Coordinates Location { get; set; }
        [JsonProperty("weather")] public List<OpenWeather_Condition> WeatherConditions { get; set; }
        [JsonProperty("base")] public string Internal_Base { get; set; }
        [JsonProperty("main")] public OpenWeather_KeyInfo KeyInfo { get; set; }
        [JsonProperty("visibility")] public int Visibility { get; set; }
        [JsonProperty("wind")] public OpenWeather_Wind Wind { get; set; }
        [JsonProperty("clouds")] public OpenWeather_Clouds Clouds { get; set; }
        [JsonProperty("rain")] public OpenWeather_Rain Rain { get; set; }
        [JsonProperty("snow")] public OpenWeather_Snow Snow { get; set; }
        [JsonProperty("dt")] public int TimeOfCalculation { get; set; }
        [JsonProperty("sys")] public OpenWeather_Internal Internal_Sys { get; set; }
        [JsonProperty("timezone")] public int Timezone { get; set; }
        [JsonProperty("id")] public int CityID { get; set; }
        [JsonProperty("name")] public string CityName { get; set; }
        [JsonProperty("cod")] public int Internal_COD { get; set; }
    }
    #endregion

    const string URL_GetPublicIP = "https://api.ipify.org/";
    const string URL_GetGeographicData = "http://www.geoplugin.net/json.gp?ip=";
    const string URL_GetWeatherData = "http://api.openweathermap.org/data/2.5/weather";

    public Text weatherText;

    string PublicIP;
    geoPluginResponse GeographicData;
    OpenWeatherResponse WeatherData;
    bool ShownWeatherInfo = false;

    public bool Clear = false;
    public bool Clouds = false;
    public bool Drizzle = false;
    public bool Rain = false;
    public bool Thunder = false;
    public bool Snow = false;
    public bool Atmosphere = false;

    void Start()
    {
        if (string.IsNullOrEmpty(OpenWeatherAPIKey))
        {
            Debug.LogError("No API key set for https://openweathermap.org/ probs go fix that?");
            return;
        }

        StartCoroutine(GetWeatherStage_1_GetIP());
    }

    void FixedUpdate()
    {
        if (Phase == EPhase.Succeeded && !ShownWeatherInfo)
        {
            ShownWeatherInfo = true;

            foreach (var condition in WeatherData.WeatherConditions)
            {
                //Debug.Log($"{condition.Group}: {condition.Description}");

                if (condition.Group=="Clear")
                {
                    weatherText.text = "Clear skys";
                    Clear = true;
                    Clouds = false;
                    Drizzle = false;
                    Rain = false;
                    Thunder = false;
                    Snow = false;
                    Atmosphere = false;
                }
                else if(condition.Group=="Clouds")
                {
                    weatherText.text = "Clouds";
                    Clear = false;
                    Clouds = true;
                    Drizzle = false;
                    Rain = false;
                    Thunder = false;
                    Snow = false;
                    Atmosphere = false;
                }
                else if(condition.Group=="Drizzle")
                {
                    weatherText.text = "Drizzle";
                    Clear = false;
                    Clouds = false;
                    Drizzle = true;
                    Rain = false;
                    Thunder = false;
                    Snow = false;
                    Atmosphere = false;
                }
                else if(condition.Group=="Rain")
                {
                    weatherText.text = "Rain";
                    Clear = false;
                    Clouds = false;
                    Drizzle = false;
                    Rain = true;
                    Thunder = false;
                    Snow = false;
                    Atmosphere = false;
                }
                else if(condition.Group=="Thunderstorm")
                {
                    weatherText.text = "Thunder";
                    Clear = false;
                    Clouds = false;
                    Drizzle = false;
                    Rain = false;
                    Thunder = true;
                    Snow = false;
                    Atmosphere = false;
                }
                else if(condition.Group=="Snow")
                {
                    weatherText.text = "Snow";
                    Clear = false;
                    Clouds = false;
                    Drizzle = false;
                    Rain = false;
                    Thunder = false;
                    Snow = true;
                    Atmosphere = false;
                }
                else if(condition.Group=="Atmosphere")
                {
                    weatherText.text = "Atmosphere";
                    Clear = false;
                    Clouds = false;
                    Drizzle = false;
                    Rain = false;
                    Thunder = false;
                    Snow = false;
                    Atmosphere = true;
                }
            }

        }

    }
        IEnumerator GetWeatherStage_1_GetIP()
        {
            Phase = EPhase.GetPublicIP;

        // attempt to retrieve our public IP address
        string PublicIP = new WebClient().DownloadString(URL_GetPublicIP);
        StartCoroutine(GetWeatherStage_2_GetGeoInfo());
        weatherText.text = "Got your IP";
        #region not ready to delete yet
        /*using (UnityWebRequest request = UnityWebRequest.Get(URL_GetPublicIP))
        {
            request.timeout = 5;
            yield return request.SendWebRequest();

            // did the request succeed?
            if (request.result == UnityWebRequest.Result.Success)
            {
                PublicIP = request.downloadHandler.text.Trim();
                StartCoroutine(GetWeatherStage_2_GetGeoInfo());
                weatherText.text = "Got your IP";
            }
            else
            {
                Debug.LogError($"Failed to get public IP: {request.downloadHandler.text}");
                weatherText.text = "Couldn't get your IP";
                Phase = EPhase.Failed;
            }
        }
        */
        #endregion

        yield return null;
        }

        IEnumerator GetWeatherStage_2_GetGeoInfo()
        {
            Phase = EPhase.GetGeographicData;

            // attempt to retrieve the geographic data
            using (UnityWebRequest request = UnityWebRequest.Get(URL_GetGeographicData + PublicIP))
            {
                request.timeout = 1;
                yield return request.SendWebRequest();

                // did the request succeed?
                if (request.result == UnityWebRequest.Result.Success)
                {
                    GeographicData = JsonConvert.DeserializeObject<geoPluginResponse>(request.downloadHandler.text);
                    weatherText.text = "Got your location";
                    StartCoroutine(GetWeatherStage_3_GetWeather());
                }
                else
                {
                    Debug.LogError($"Failed to get geographic data: {request.downloadHandler.text}");
                    weatherText.text = "NFC where you are";
                    Phase = EPhase.Failed;
                }
            }

            yield return null;
        }

        IEnumerator GetWeatherStage_3_GetWeather()
        {
            Phase = EPhase.GetWeatherData;

            string weatherURL = URL_GetWeatherData;
            weatherURL += $"?lat={GeographicData.Latitude}";
            weatherURL += $"&lon={GeographicData.Longitude}";
            weatherURL += $"&APPID={OpenWeatherAPIKey}";

            // attempt to retrieve the geographic data
            using (UnityWebRequest request = UnityWebRequest.Get(weatherURL))
            {
                request.timeout = 1;
                yield return request.SendWebRequest();

                // did the request succeed?
                if (request.result == UnityWebRequest.Result.Success)
                {
                    WeatherData = JsonConvert.DeserializeObject<OpenWeatherResponse>(request.downloadHandler.text);
                
                    Debug.Log($"You are in {WeatherData.CityName}");

                Phase = EPhase.Succeeded;
                }
                else
                {
                    Debug.LogError($"Failed to get geographic data: {request.downloadHandler.text}");
                    weatherText.text = "Not sure what the weather is. Something broke";
                    Phase = EPhase.Failed;
                }
            }

            yield return null;
        }
}
