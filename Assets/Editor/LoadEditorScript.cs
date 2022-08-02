using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadData))]
public class LoadEditorScrip : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LoadData loadScript = (LoadData)target;

        if (GUILayout.Button("Load Data"))
        {
            loadScript.LoadJson();
            loadScript.LoadPlayerPrefs();
        }
        if (GUILayout.Button("Compare Data"))
        {
            loadScript.CompareData();
        }
    }
}

