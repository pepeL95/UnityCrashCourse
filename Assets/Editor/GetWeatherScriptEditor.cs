using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GetWeatherScript))]
public class GetWeatherScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();
        GetWeatherScript apiScript = (GetWeatherScript)target;

        if (GUILayout.Button("Build Object"))
        {
            apiScript.GetData();
        }

        //apiScript.lat = EditorGUILayout.FloatField("Latitude", apiScript.lat);
        //apiScript.lon = EditorGUILayout.FloatField("Longitude", apiScript.lon);

    }


}
