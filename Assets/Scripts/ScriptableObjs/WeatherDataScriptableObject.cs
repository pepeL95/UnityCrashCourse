using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Weather Data")]
[Serializable]
public class WeatherDataScriptableObject : ScriptableObject
{
    [SerializeField]
    public Properties properties = new();
}

[Serializable]
public class Properties
{
    public string city;
    public string updated;
    public string units;
    public string forecastGenerator;
    public string generatedAt;
    public string updateTime;
    public string validTimes;
    public Elevation elevation;
    public Period[] periods;
}

[Serializable]
public class Period
{
    public int number;
    public string Name;
    public string startTime;
    public string endTime;
    public bool isDaytime;
    public int temperature;
    public string temperatureUnit;
    public string temperatureTrend;
    public string windSpeed;
    public string windDirection;
    public string icon;
    public string shortForecast;
    public string detailedForecast;
}

[Serializable]
public class Elevation
{
    public string unitCode;
    public double value;
}