using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginManager : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private GameSettingsScriptableObject gameSettings;
    private int spawnCount;
    private int spawningLimit;

    private void Start()
    {
        // Default Values
        spawnCount = 0;
        spawningLimit = 5;
        SpawnOrigin();
    }
    public void SpawnOrigin()
    {
        if (origin == null)
            return;
        Instantiate(origin);
    }
    public void RespawnOrigin() => StartCoroutine(Respawn());
    IEnumerator Respawn()
    {
        // update origin speed from web browser
        if (gameSettings.originSpawnInstantiationLimit != 0)
            spawningLimit = gameSettings.originSpawnInstantiationLimit;
        
        if (spawnCount <= spawningLimit)
        {
            yield return new WaitForSeconds(5f);
            SpawnOrigin();
            spawnCount++;
        }
    }

    public void DestroyAll()
    {
        //Debug.Log(GameObject.FindGameObjectsWithTag("EnemyCube").Length);
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("EnemyCube"))
        {
            Destroy(o);

        }
    }
}

