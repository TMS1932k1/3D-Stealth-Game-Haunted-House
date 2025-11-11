using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private int objCount;
    [SerializeField] private T prefabObj;

    private Queue<T> pools = new();


    private void Awake()
    {
        for (int i = 0; i < objCount; i++)
        {
            T obj = Instantiate(prefabObj, transform);
            obj.gameObject.SetActive(false);
            pools.Enqueue(obj);
        }
    }

    public T GetObjectPool()
    {
        if (pools.Count <= 0)
        {
            T obj = Instantiate(prefabObj, transform);
            obj.gameObject.SetActive(false);
            pools.Enqueue(obj);

            return GetObjectPool();
        }
        else
        {
            Debug.Log($"[{name}]: Get object from pool");

            T obj = pools.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public void ReturnPbjectPool(T obj)
    {
        Debug.Log($"[{name}]: Return object to pool");

        obj.gameObject.SetActive(false);
        obj.gameObject.transform.position = transform.position;
        pools.Enqueue(obj);
    }
}
