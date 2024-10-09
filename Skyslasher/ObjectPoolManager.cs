using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPooledEmptyHolder;
    private static GameObject _ParticleSystemObjectEmpty;
    private static GameObject _BulletObjectEmpty;

    public enum PoolType
    {
        ParticleSystem,
        Bullets,
        None
    }

    private void Awake()
    {
        SetUpEmpties();
        InitializePools();
    }

    private void SetUpEmpties()
    {
        _objectPooledEmptyHolder = new GameObject("Pooled Objects");

        _BulletObjectEmpty = new GameObject("Bullets");
        _BulletObjectEmpty.transform.SetParent(_objectPooledEmptyHolder.transform);

        _ParticleSystemObjectEmpty = new GameObject("Particles");
        _ParticleSystemObjectEmpty.transform.SetParent(_objectPooledEmptyHolder.transform);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        string objectName = objectToSpawn.name;
        PooledObjectInfo pool = ObjectPools.Find(p => p.Prefab.name == objectName);

        if (pool == null)
        {
            pool = new PooledObjectInfo { Prefab = objectToSpawn };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            spawnableObj.name = objectName; // Ensure the name is consistent
            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name;
        PooledObjectInfo pool = ObjectPools.Find(p => p.Prefab.name == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not loaded: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _ParticleSystemObjectEmpty;
            case PoolType.Bullets:
                return _BulletObjectEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }
}

public class PooledObjectInfo
{
    public GameObject Prefab;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
