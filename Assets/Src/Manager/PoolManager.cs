using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private List<GameObject> pool = null;
    private Dictionary<GameObject, Queue<GameObject>> currentPool = null;
    private Dictionary<GameObject, GameObject> relationPool = null;

    public void init()
    {
        pool = new List<GameObject>();
        currentPool = new Dictionary<GameObject, Queue<GameObject>>();
        relationPool = new Dictionary<GameObject, GameObject>();
    }

    private GameObject createPoolNode(GameObject obj)
    {
        GameObject node = GameObject.Instantiate(obj);
        relationPool.Add(node, obj);
        pool.Add(node);
        return node;
    }

    private Queue<GameObject> outQueueObj = null;

    public GameObject getPool(GameObject obj)
    {
        if (obj == null)
        {
            return null;
        }

        currentPool.TryGetValue(obj, out outQueueObj);
        if (outQueueObj == null)
        {
            outQueueObj = new Queue<GameObject>();
            currentPool.Add(obj, outQueueObj);
        }

        GameObject node = null;
        if (outQueueObj.Count > 0)
        {
            node = outQueueObj.Dequeue();
        }

        if (node == null)
        {
            node = createPoolNode(obj);
        }

        node.SetActive(true);

        return node;
    }

    private GameObject outObj = null;

    public void setPool(GameObject node)
    {
        if (node == null)
        {
            return;
        }

        node.transform.SetParent(null);
        node.SetActive(false);

        relationPool.TryGetValue(node, out outObj);

        if (outObj == null)
        {
            return;
        }

        currentPool.TryGetValue(outObj, out outQueueObj);
        if (outQueueObj == null)
        {
            return;
        }

        outQueueObj.Enqueue(node);
    }

    public void clearPool()
    {
        currentPool.Clear();
        relationPool.Clear();
        for (var i = 0; i < pool.Count; i++)
        {
            GameObject node = pool[i];
            pool[i] = null; // 解引用
            if (node.transform.parent != null)
            {
                node.transform.SetParent(null);
            }

            GameObject.Destroy(node); // 销毁所有
        }

        pool.Clear();
    }
}