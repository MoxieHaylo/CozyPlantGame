using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GetLocalWeatherData : MonoBehaviour
{
    #region Weather API Key
    const string OpenWeatherAPIKey = "";
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
    public Text timeText;
    public int sysHour = System.DateTime.Now.Hour;

    public bool DayTime = true;

    public EPhase Phase { get; private set; } = EPhase.NotStarted;

    string PublicIP;
    geoPluginResponse GeographicData;
    OpenWeatherResponse WeatherData;
    bool ShownWeatherInfo = false;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($"This is what we are printing{sysHour}");
        if (string.IsNullOrEmpty(OpenWeatherAPIKey))
        {
            Debug.LogError("No API key set for https://openweathermap.org/ probs go fix that?");
            return;
        }

        StartCoroutine(GetWeatherStage_1_GetIP());
    }

    // Update is called once per frame
    void Update()
    {
        if (Phase == EPhase.Succeeded && !ShownWeatherInfo)
        {
            ShownWeatherInfo = true;

            foreach (var condition in WeatherData.WeatherConditions)
            {
                Debug.Log($"{condition.Group}: {condition.Description}");

                if (condition.Description == "clear sky")
                {
                    weatherText.text = "Clear skys";
                }
                //clouds
                else if (DayTime=true &&
                         condition.Description == "few clouds" ||
                         condition.Description == "scattered clouds" ||
                         condition.Description == "broken clouds" ||
                         condition.Description == "few clouds: 11-25%" ||
                         condition.Description == "scattered clouds: 25-50%" ||
                         condition.Description == "broken clouds: 51-84%" ||
                         condition.Description == "overcast clouds: 85-100%")
                {
                    weatherText.text = "There are clouds today";
                    Debug.Log("this should be daytime clouds true");
                }
                else if (DayTime=false &&
                         condition.Description == "few clouds" ||
                         condition.Description == "scattered clouds" ||
                         condition.Description == "broken clouds" ||
                         condition.Description == "few clouds: 11-25%" ||
                         condition.Description == "scattered clouds: 25-50%" ||
                         condition.Description == "broken clouds: 51-84%" ||
                         condition.Description == "overcast clouds: 85-100%")
                {
                    weatherText.text = "There are clouds tonight";
                    Debug.Log("this should be night clouds true");
                }
                //light rain
                else if (DayTime=true &&
                         condition.Description == "shower rain" ||
                         condition.Description == "light intensity drizzle" ||
                         condition.Description == "drizzle" ||
                         condition.Description == "heavy intensity drizzle" ||
                         condition.Description == "light instensity drizzle rain" ||
                         condition.Description == "shower and rain drizzle" ||
                         condition.Description == "shower drizzle")
                {
                    weatherText.text = "It's drizzling today";
                }
                else if (DayTime==false &&
                         condition.Description == "shower rain" ||
                         condition.Description == "light intensity drizzle" ||
                         condition.Description == "drizzle" ||
                         condition.Description == "heavy intensity drizzle" ||
                         condition.Description == "light instensity drizzle rain" ||
                         condition.Description == "shower and rain drizzle" ||
                         condition.Description == "shower drizzle")
                {
                    weatherText.text = "It's drizzling tonight";
                }
                //rain
                else if (DayTime==true &&
                         condition.Description == "light rain" ||
                         condition.Description == "moderate rain" ||
                         condition.Description == "heavy intensity rain" ||
                         condition.Description == "very heavy rain" ||
                         condition.Description == "extreme rain" ||
                         condition.Description == "freezing rain" ||
                         condition.Description == "light intensity shower rain" ||
                         condition.Description == "shower rain" ||
                         condition.Description == "heavy intensity shower rain" ||
                         condition.Description == "ragged shower rain")
                {
                    weatherText.text = "It's raining today";
                }
                else if (DayTime==false &&
                         condition.Description == "light rain" ||
                         condition.Description == "moderate rain" ||
                         condition.Description == "heavy intensity rain" ||
                         condition.Description == "very heavy rain" ||
                         condition.Description == "extreme rain" ||
                         condition.Description == "freezing rain" ||
                         condition.Description == "light intensity shower rain" ||
                         condition.Description == "shower rain" ||
                         condition.Description == "heavy intensity shower rain" ||
                         condition.Description == "ragged shower rain")
                {
                    weatherText.text = "It's raining tonight";
                }
                //thunderstorm
                else if (DayTime == true &&
                         condition.Description == "thunderstorm with light rain" ||
                         condition.Description == "thunderstorm with rain" ||
                         condition.Description == "thunderstorm with heavy rain" ||
                         condition.Description == "light thunderstorm" ||
                         condition.Description == "thunderstorm" ||
                         condition.Description == "heavy thunderstorm" ||
                         condition.Description == "ragged thunderstorm" ||
                         condition.Description == "thunderstorm with light drizzle" ||
                         condition.Description == "thunderstorm with drizzle" ||
                         condition.Description == "thunderstorm with heavy drizzle")
                {
                    weatherText.text = "There is a thunderstorm today";
                }
                else if (DayTime == false &&
                         condition.Description == "thunderstorm with light rain" ||
                         condition.Description == "thunderstorm with rain" ||
                         condition.Description == "thunderstorm with heavy rain" ||
                         condition.Description == "light thunderstorm" ||
                         condition.Description == "thunderstorm" ||
                         condition.Description == "heavy thunderstorm" ||
                         condition.Description == "ragged thunderstorm" ||
                         condition.Description == "thunderstorm with light drizzle" ||
                         condition.Description == "thunderstorm with drizzle" ||
                         condition.Description == "thunderstorm with heavy drizzle")
                {
                    weatherText.text = "There is a thunderstorm tonight";
                }
                //snow
                else if (DayTime == true &&
                         condition.Description == "snow" ||
                         condition.Description == "light snow" ||
                         condition.Description == "heavy snow" ||
                         condition.Description == "sleet" ||
                         condition.Description == "light shower sleet" ||
                         condition.Description == "shower sleet" ||
                         condition.Description == "light rain and snow" ||
                         condition.Description == "rain and snow" ||
                         condition.Description == "light shower snow" ||
                         condition.Description == "shower snow" ||
                         condition.Description == "heavy shower snow")
                {
                    weatherText.text = "It's snowing today";
                }
                else if (DayTime == false &&
                         condition.Description == "snow" ||
                         condition.Description == "light snow" ||
                         condition.Description == "heavy snow" ||
                         condition.Description == "sleet" ||
                         condition.Description == "light shower sleet" ||
                         condition.Description == "shower sleet" ||
                         condition.Description == "light rain and snow" ||
                         condition.Description == "rain and snow" ||
                         condition.Description == "light shower snow" ||
                         condition.Description == "shower snow" ||
                         condition.Description == "heavy shower snow")
                {
                    weatherText.text = "It's snowing tonight";
                }
                //atmospheric
                else if (DayTime == true &&
                         condition.Description == "mist" ||
                         condition.Description == "smoke" ||
                         condition.Description == "haze" ||
                         condition.Description == "sand/dust whirls" ||
                         condition.Description == "fog" ||
                         condition.Description == "sand" ||
                         condition.Description == "dust" ||
                         condition.Description == "volcanic ash" ||
                         condition.Description == "squalls" ||
                         condition.Description == "tornado")

                {
                    weatherText.text = "You have unusual weather today";
                }
                else if (DayTime == false &&
                         condition.Description == "mist" ||
                         condition.Description == "smoke" ||
                         condition.Description == "haze" ||
                         condition.Description == "sand/dust whirls" ||
                         condition.Description == "fog" ||
                         condition.Description == "sand" ||
                         condition.Description == "dust" ||
                         condition.Description == "volcanic ash" ||
                         condition.Description == "squalls" ||
                         condition.Description == "tornado")
                {
                    weatherText.text = "You have unusual weather tonight";
                }


            }

        }

    }
        IEnumerator GetWeatherStage_1_GetIP()
        {
            Phase = EPhase.GetPublicIP;

            // attempt to retrieve our public IP address
            using (UnityWebRequest request = UnityWebRequest.Get(URL_GetPublicIP))
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
