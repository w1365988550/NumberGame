using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AppContext : MonoBehaviour
{
    private static AppContext _instance = null;

    public static AppContext instance
    {
        get { return _instance; }
    }

    public UIManager uiManager = null;
    public PoolManager poolManager = null;
    public ResManager resManager = null;

    private void Awake()
    {
        _instance = this;
        initUIManager();
        initPoolManager();
        initResManager();
    }

    private void initUIManager()
    {
        // 创建UI根节点
        GameObject uiRoot = new GameObject();
        Transform uiRootTransform = uiRoot.transform;
        uiRootTransform.SetParent(this.transform);
        uiRootTransform.localPosition = new Vector3(0, 0, 0);
        uiRootTransform.SetSiblingIndex(10);
        uiManager = new UIManager();
        uiManager.init(uiRoot);

        uiManager.showBoard("Prefabs/UI/GameBoard", null, null);
    }

    private void initPoolManager()
    {
        poolManager = new PoolManager();
        poolManager.init();
    }

    private void initResManager()
    {
        resManager = new ResManager();
        resManager.init();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.uiManager != null)
        {
            this.uiManager.localUpdate();
        }
    }
}