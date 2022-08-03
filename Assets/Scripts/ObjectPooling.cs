using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

// This component returns the particle system to the pool when the OnParticleSystemStopped event is received.
/*[RequireComponent(typeof(ParticleSystem))]
public class ReturnToPool : MonoBehaviour
{
    public GameObject go;
    public IObjectPool<GameObject> pool;

    void Start()
    {
        go = GetComponent<ParticleSystem>();
        var main = go.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        pool.Release(go);
    }
}*/
public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public ObjectPool<GameObject> pool;
    [SerializeField] private GameObject gameObject;
    public bool collectionChecks = true;
    public int maxPoolSize = 10;

    private void Awake()
    {
        SharedInstance = this;
        pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
    }
    private void Start()
    {
        for (int i = 0; i < maxPoolSize; i++)
            pool.Get().SetActive(false);
    }
    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    GameObject CreatePooledItem()
    {

        return Instantiate(gameObject);
    }
}