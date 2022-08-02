using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Game Settings")]
[Serializable]
public class GameSettingsScriptableObject : ScriptableObject
{
    [SerializeField] public float originSpeed;
    [SerializeField] public float playerSpeed;
    [SerializeField] public int originSpawnInstantiationLimit;
    [SerializeField] public string playerName;
}
