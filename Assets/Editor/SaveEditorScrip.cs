using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveData))]
public class SaveEditorScrip : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SaveData saveScript = (SaveData)target;

        if (GUILayout.Button("Save Data"))
        {
            saveScript.SaveJson();
            saveScript.SavePlayerPrefs();
        }
    }
}

