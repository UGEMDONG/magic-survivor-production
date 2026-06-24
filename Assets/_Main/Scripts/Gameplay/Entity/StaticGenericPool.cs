using UnityEngine;
using System.Collections.Generic;

public static class StaticGenericPool<T> where T : MonoBehaviour
{
    static Queue<T> objectPool = new Queue<T>();

    public static void Preload(T prefab, int count, Transform parent)
    {
        for (int i = 0; i < count; i++)
        {
            T obj = Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }
    public static T GetPool(T prefab, Vector3 position, Transform parent)
    {
        T obj;
        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
            obj.transform.SetParent(parent);
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Object.Instantiate(prefab, parent);
        }
        obj.transform.position = position;
        return obj;
    }
    public static void ReturnPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objectPool.Enqueue(obj);
    }

    public static void ClearPool()
    {
        objectPool.Clear();
    }
}
