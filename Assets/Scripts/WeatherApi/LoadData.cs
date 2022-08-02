using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.Networking;
public class LoadData : MonoBehaviour
{
    private WeatherObject weatherDataFromJson;
    private WeatherObject weatherDataFromPPrefs;

    public TMP_Text cityTxt;
    public TMP_Text temperatureTxt;
    public TMP_Text forecastTxt;
    public void LoadJson()
    {
        if (Application.dataPath.Contains("http"))
        {
            Debug.Log("Load server api");
            StartCoroutine(LoadFromWebGL());
            return;
        }
        StreamReader file = File.OpenText(Application.dataPath + "/Data/WeatherInfo.json");
        weatherDataFromJson = JsonConvert.DeserializeObject<WeatherObject>(file.ReadToEnd());
        cityTxt.text = weatherDataFromJson.properties.city + "°";
        temperatureTxt.text = weatherDataFromJson.properties.periods[0].temperature.ToString();
        forecastTxt.text = weatherDataFromJson.properties.periods[0].shortForecast.ToString();
        Debug.Log("Json Loaded");
    }
    public void LoadPlayerPrefs()   
    {
        weatherDataFromPPrefs = new();
        weatherDataFromPPrefs.properties.periods = new Period[1];
        weatherDataFromPPrefs.properties.periods[0] = new Period();

        weatherDataFromPPrefs.properties.city = PlayerPrefs.GetString("city");
        weatherDataFromPPrefs.properties.periods[0].number = PlayerPrefs.GetInt("number");
        weatherDataFromPPrefs.properties.periods[0].Name = PlayerPrefs.GetString("name");
        weatherDataFromPPrefs.properties.periods[0].endTime = PlayerPrefs.GetString("endTime");
        weatherDataFromPPrefs.properties.periods[0].isDaytime = bool.Parse(PlayerPrefs.GetString("isDayTime"));
        weatherDataFromPPrefs.properties.periods[0].temperature = PlayerPrefs.GetInt("temperature");
        weatherDataFromPPrefs.properties.periods[0].temperatureUnit = PlayerPrefs.GetString("temperatureUnit");
        weatherDataFromPPrefs.properties.periods[0].windSpeed = PlayerPrefs.GetString("windSpeed");
        weatherDataFromPPrefs.properties.periods[0].windDirection = PlayerPrefs.GetString("windDirection");
        weatherDataFromPPrefs.properties.periods[0].icon = PlayerPrefs.GetString("icon");
        weatherDataFromPPrefs.properties.periods[0].shortForecast = PlayerPrefs.GetString("shortForecast");
        weatherDataFromPPrefs.properties.periods[0].detailedForecast = PlayerPrefs.GetString("detailedForecast");
       
        cityTxt.text = weatherDataFromPPrefs.properties.city + "°";
        temperatureTxt.text = weatherDataFromPPrefs.properties.periods[0].temperature.ToString();
        forecastTxt.text = weatherDataFromPPrefs.properties.periods[0].shortForecast.ToString();
        Debug.Log("PPrefs Loaded");
    }
    public void CompareData()
    {
        if (
            weatherDataFromPPrefs.properties.periods[0].number == weatherDataFromJson.properties.periods[0].number &&
            weatherDataFromPPrefs.properties.periods[0].Name == weatherDataFromJson.properties.periods[0].Name &&
            weatherDataFromPPrefs.properties.periods[0].endTime == weatherDataFromJson.properties.periods[0].endTime &&
            weatherDataFromPPrefs.properties.periods[0].isDaytime == weatherDataFromJson.properties.periods[0].isDaytime &&
            weatherDataFromPPrefs.properties.periods[0].temperature == weatherDataFromJson.properties.periods[0].temperature &&
            weatherDataFromPPrefs.properties.periods[0].temperatureUnit == weatherDataFromJson.properties.periods[0].temperatureUnit &&
            weatherDataFromPPrefs.properties.periods[0].windSpeed == weatherDataFromJson.properties.periods[0].windSpeed &&
            weatherDataFromPPrefs.properties.periods[0].windDirection == weatherDataFromJson.properties.periods[0].windDirection &&
            weatherDataFromPPrefs.properties.periods[0].icon == weatherDataFromJson.properties.periods[0].icon &&
            weatherDataFromPPrefs.properties.periods[0].shortForecast == weatherDataFromJson.properties.periods[0].shortForecast &&
            weatherDataFromPPrefs.properties.periods[0].detailedForecast == weatherDataFromJson.properties.periods[0].detailedForecast
         ) Debug.Log("Passed Check Point!!");
        else
            Debug.Log("Didn't Pass Checkpoint"); 
    }

    IEnumerator LoadFromWebGL()
    {
        string uri = "http://localhost:8000/load-data";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        { // Request and wait for the desired page.
            yield return req.SendWebRequest();  
            switch (req.result) // this fetches the forecast api based on lat lon
            {
                case UnityWebRequest.Result.ConnectionError:
                // fallthrough 
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error" + req.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + req.error);
                    break;
                case UnityWebRequest.Result.Success:
                    weatherDataFromJson = JsonConvert.DeserializeObject<WeatherObject>(req.downloadHandler.text);
                    break;
            }
            cityTxt.text = weatherDataFromJson.properties.city;
            temperatureTxt.text = weatherDataFromJson.properties.periods[0].temperature.ToString();
            forecastTxt.text = weatherDataFromJson.properties.periods[0].shortForecast.ToString();
        }
    }
}

