using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    public static ObjectPooling SharedInstance;
    public ObjectPool<GameObject> pool;
    public bool collectionChecks = true;
    public int maxPoolSize = 10;

    private void Awake()
    {
        SharedInstance = this;
        pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
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