using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ApiEditorScriptWindow : EditorWindow
/* This class will not be included in the build version. This is like dev tools */
{
    string lat = "42.39801";
    string lon = "-121.04287";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    [MenuItem("Window/Change API Location")]
    static void OpenWindow()
    {
        ApiEditorScriptWindow window = (ApiEditorScriptWindow)GetWindow(typeof(ApiEditorScriptWindow));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Location", EditorStyles.boldLabel);
        EditorGUILayout.TextField("Latitude", lat);
        EditorGUILayout.TextField("Longitude", lon);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }
}
