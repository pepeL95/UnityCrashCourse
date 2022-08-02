using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesHandler : MonoBehaviour
{
    public Camera cam;
    public WeatherDataScriptableObject weather;
    public GameObject rain;
    public GameObject sun;
    public GameObject clouds;
    private GameObject toDestroy;
    public void handleBackgroundColor()
    {
        string state = weather.properties.periods[0].shortForecast.ToString();
        Debug.Log(state);
        if (state.Contains("Mostly Clear") || state.Contains("Partly Cloudy"))
        {
            if (toDestroy = GameObject.FindGameObjectWithTag("Rain"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Sun"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Clouds"))
                DestroyImmediate(toDestroy, true);
            Instantiate(sun);
            Instantiate(clouds);
            cam.backgroundColor = new Color(0.2616589f, 0.6000072f, 0.9433962f, 0f);
        }
        else if(state.Contains("Clear") || state.Contains("Sunny"))
        {
            if (toDestroy = GameObject.FindGameObjectWithTag("Rain"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Sun"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Clouds"))
                DestroyImmediate(toDestroy, true);
            Instantiate(sun);
            cam.backgroundColor = new Color(0.2616589f, 0.6000072f, 0.9433962f, 0f);
        }
        else if (state.Contains("Showers") || state.Contains("Rain"))
        {
            if (toDestroy = GameObject.FindGameObjectWithTag("Rain"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Sun"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Clouds"))
                DestroyImmediate(toDestroy, true);
            Instantiate(rain);
            cam.backgroundColor = new Color(0.2365254f, 0.3478684f, 0.4150943f, 0f);
        }
        else if (state.Contains("Cloudy") || state.Contains("Clouds"))
        {
            if (toDestroy = GameObject.FindGameObjectWithTag("Rain"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Sun"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Clouds"))
                DestroyImmediate(toDestroy, true);
            Instantiate(clouds);
            cam.backgroundColor = new Color(0.2365254f, 0.3478684f, 0.4150943f, 0f);
        }
        else // need to consider further scenarios and adjust accordingly
        {
            if (toDestroy = GameObject.FindGameObjectWithTag("Rain"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Sun"))
                DestroyImmediate(toDestroy, true);
            if (toDestroy = GameObject.FindGameObjectWithTag("Clouds"))
                DestroyImmediate(toDestroy, true);
            cam.backgroundColor = new Color(0.2365254f, 0.3478684f, 0.4150943f, 0f);
        }

    } 
}
