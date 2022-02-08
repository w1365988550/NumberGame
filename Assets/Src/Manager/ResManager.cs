using System.Collections.Generic;
using UnityEngine;

public class ResManager
{
    private Dictionary<string, GameObject> prefabDic = null;

    public static Stack<GameObject> aaa = new Stack<GameObject>();

    public void init()
    {
        prefabDic = new Dictionary<string, GameObject>();
    }

    public GameObject getPrefab(string url)
    {
        GameObject test = new GameObject();
        if (!prefabDic.TryGetValue(url, out test))
        {
            test = Resources.Load<GameObject>(url);
            prefabDic.Add(url, test);
        }

        return test;
    }
}