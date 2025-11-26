using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<GameObject, Queue<GameObject>> pools =
        new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject Get(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
            pools[prefab] = new Queue<GameObject>();

        if (pools[prefab].Count > 0)
        {
            GameObject obj = pools[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // Senão, cria um novo
        return Instantiate(prefab);
    }

    public void Release(GameObject prefab, GameObject instance)
    {
        instance.SetActive(false);
        pools[prefab].Enqueue(instance);
    }
}