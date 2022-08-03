using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
public class GetWeatherScript : MonoBehaviour
{
    public WeatherDataScriptableObject weatherScriptableObject;
    public StatesHandler weatherStateHandler;
    private WeatherObject weatherObject;
    //    public Camera cam;
    public string lat, lon;
    private string weatherApi;
    public TMP_Text cityTxt;
    public TMP_Text temperatureTxt;
    public TMP_Text forecastTxt;

    private void Start()
    {
        // defualt values
        //lat = "42.397985";
        //lon = "-121.042865";
        
    }
    public void GetData()
    {
        StartCoroutine(GetWeatherFromGPS(lat, lon));
    }
    IEnumerator GetWeatherFromGPS(string lat, string lon)
    {
        lat = "27.8661";
        lon = "-82.3265";
        string uri = "https://api.weather.gov/points/" + lat + "," + lon;
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
                    var parsedObj = JObject.Parse(req.downloadHandler.text);
                    weatherApi = parsedObj["properties"]["forecastHourly"].ToString(); // contains api to forecast
                    cityTxt.text = parsedObj["properties"]["relativeLocation"]["properties"]["city"].ToString() + ", " + parsedObj["properties"]["relativeLocation"]["properties"]["state"].ToString();
                    break;
            }
        }
        using (UnityWebRequest req = UnityWebRequest.Get(weatherApi)) // this fetches weather info given the forecast api
        { // Request and wait for the desired page.
            yield return req.SendWebRequest();
            switch (req.result)
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
                    weatherObject = JsonConvert.DeserializeObject<WeatherObject>(req.downloadHandler.text);
                    weatherObject.properties.city = cityTxt.text;
                    break;
            }
            weatherScriptableObject.properties = weatherObject.properties; // build scriptable object
            weatherStateHandler.handleBackgroundColor();
            temperatureTxt.text = weatherScriptableObject.properties.periods[0].temperature.ToString() + "°";
            forecastTxt.text = weatherScriptableObject.properties.periods[0].shortForecast.ToString();
        }
    }
}
