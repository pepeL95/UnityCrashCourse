using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;
public class SaveData : MonoBehaviour
{
    public WeatherDataScriptableObject weatherScriptableObject;
     public void SaveJson()
    {
        string json = JsonConvert.SerializeObject(weatherScriptableObject, Formatting.None);
        
        if (Application.dataPath.Contains("http"))
        {
            Debug.Log("Calling save server api");
            StartCoroutine(SaveToWebGL(json));
            return;
        }
        File.WriteAllText(Application.dataPath + "/Data/WeatherInfo.json", json);
        Debug.Log("Data Saved Successfully");
    }
    public void SavePlayerPrefs() // stores first period. That is, the forecast for the day requested
    {
        PlayerPrefs.SetInt("number", weatherScriptableObject.properties.periods[0].number);
        PlayerPrefs.SetString("name", weatherScriptableObject.properties.periods[0].Name);
        PlayerPrefs.SetString("city", weatherScriptableObject.properties.city);
        PlayerPrefs.SetString("endTime", weatherScriptableObject.properties.periods[0].endTime);
        PlayerPrefs.SetString("isDayTime", weatherScriptableObject.properties.periods[0].isDaytime.ToString());
        PlayerPrefs.SetInt("temperature", weatherScriptableObject.properties.periods[0].temperature);
        PlayerPrefs.SetString("temperatureTrend", weatherScriptableObject.properties.periods[0].temperatureTrend);
        PlayerPrefs.SetString("temperatureUnit", weatherScriptableObject.properties.periods[0].temperatureUnit);
        PlayerPrefs.SetString("windSpeed", weatherScriptableObject.properties.periods[0].windSpeed);
        PlayerPrefs.SetString("windDirection", weatherScriptableObject.properties.periods[0].windDirection);
        PlayerPrefs.SetString("icon", weatherScriptableObject.properties.periods[0].icon);
        PlayerPrefs.SetString("shortForecast", weatherScriptableObject.properties.periods[0].shortForecast);
        PlayerPrefs.SetString("detailedForecast", weatherScriptableObject.properties.periods[0].detailedForecast);
        //PlayerPrefs.DeleteAll();
        Debug.Log("Data Saved Successfully");
    }
    IEnumerator SaveToWebGL(string json)
    {
        string uri = "http://localhost:8000/save-data";
        using (UnityWebRequest req = UnityWebRequest.Post(uri, ""))
        { // Request and wait for the desired page.
            req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            req.SetRequestHeader("Content-Type", "application/json");
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
                    //Debug.Log(req.downloadHandler.text);
                    break;
            }
        }
    }

}