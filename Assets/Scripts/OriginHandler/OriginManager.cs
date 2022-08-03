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
       
        for (int i = 0; i < spawningLimit; i++)
            SpawnOrigin();
        
    }
    private void Update()
    {
        if (gameSettings.originSpawnInstantiationLimit != 0)
            spawningLimit = gameSettings.originSpawnInstantiationLimit;
        if (spawnControl < spawningLimit)
        {
            RespawnOrigin();
            spawnControl++;
        }
    }
    public void SpawnOrigin() => ObjectPooling.SharedInstance.pool.Get();
    public void RespawnOrigin() => StartCoroutine(Respawn());
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f); 
        SpawnOrigin();
    }
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

    public int GetActiveEnemies()
    {
        int activeEnemies = 0;
        var enemies = GameObject.FindGameObjectsWithTag("EnemyCube");
        foreach (var enemy in enemies)
            if (enemy.active)
                activeEnemies++;
        return activeEnemies;
    }
}

