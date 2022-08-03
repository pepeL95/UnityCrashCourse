using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OriginManager : MonoBehaviour
{
    [SerializeField] private GameSettingsScriptableObject gameSettings;
    public int spawningLimit;
    private int spawnControl;
    private void Start()
    {
        // Default Values
        spawningLimit = 5;
        spawnControl = spawningLimit;
        InitializeEnemyOrigins();
    }

    private void Update()
    {
        if (gameSettings.originSpawnInstantiationLimit != 0)
            spawningLimit = gameSettings.originSpawnInstantiationLimit;
        if (spawnControl < spawningLimit)
        {
            spawnControl = spawningLimit;
            FillOriginSpawningGap();
        }
    }
    public void InitializeEnemyOrigins() => StartCoroutine(InitializeEnemies());
    public void RespawnOrigin() => StartCoroutine(Respawn());
    public void FillOriginSpawningGap() => StartCoroutine(FillSpawningGap());
    public void SpawnOrigin() => ObjectPooling.SharedInstance.pool.Get();
    public int GetActiveEnemies() => ObjectPooling.SharedInstance.pool.CountActive;
    public void AtomicRespawn(GameObject toDestroy)
    {
        /* Destroys and respawns (if possible) in one atomic function */
        ObjectPooling.SharedInstance.pool.Release(toDestroy);
        if (GetActiveEnemies() < spawningLimit)
            RespawnOrigin();
    }
    public void DestroyAll()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyCube"))
            ObjectPooling.SharedInstance.pool.Release(go);
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f); 
        SpawnOrigin();
    }
    IEnumerator FillSpawningGap()
    {
        for (int i = ObjectPooling.SharedInstance.pool.CountAll; i < spawningLimit; i++)
        {
            yield return new WaitForSeconds(5.0f);
            SpawnOrigin();
        }
    }
    IEnumerator InitializeEnemies()
    {
        for (int i = 0; i < spawningLimit; i++)
        {
            yield return new WaitForSeconds(5.0f);
            SpawnOrigin();
        }
    }
}

