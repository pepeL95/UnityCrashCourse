using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private GameSettingsScriptableObject gameSettings;
    [SerializeField] private OriginManager originManager;

    void UpdateOriginSpeed(float originSpeed)
    {
        gameSettings.originSpeed = originSpeed;

    }
    void UpdatePlayerSpeed(float playerSpeed)
    {
        gameSettings.playerSpeed = playerSpeed;
    }
    void UpdateSpawningInstantiationLimit(int originSpawnInstantiationLimit)
    {
        gameSettings.originSpawnInstantiationLimit = originSpawnInstantiationLimit;

    }
    void DestroyAllOriginObjects()
    {
        originManager.DestroyAll();
    }


}
