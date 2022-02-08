using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private GameObject boardRoot = null;
    private GameObject boardEffectRoot = null;
    private GameObject dialogRoot = null;
    private GameObject dialogEffectRoot = null;
    private GameObject tipsRoot = null;

    private UIData boardData = null;
    private Stack<UIData> dialogData = new Stack<UIData>();

    private UIWrapper curBoard = null;
    private List<UIWrapper> curDialog = null;

    public void init(GameObject uiRoot)
    {
        curBoard = null;
        curDialog = new List<UIWrapper>();
        boardRoot = createNode("boardRoot", uiRoot, 0);
        boardEffectRoot = createNode("boardEffectRoot", uiRoot, 1);
        dialogRoot = createNode("dialogRoot", uiRoot, 2);
        dialogEffectRoot = createNode("dialogEffectRoot", uiRoot, 3);
        tipsRoot = createNode("tipsRoot", uiRoot, 4);
    }

    private GameObject createNode(string name, GameObject parent, int siblingIndex)
    {
        GameObject obj = new GameObject();
        Transform objTransform = obj.transform;
        objTransform.SetParent(parent.transform);
        objTransform.localPosition = new Vector3(0, 0, 0);
        objTransform.SetSiblingIndex(siblingIndex);
        obj.name = name;
        return obj;
    }

    public void localUpdate()
    {
        if (this.boardData != null)
        {
            this.spawnBoard(this.boardData);
            this.boardData = null;
        }

        if (this.dialogData.Count > 0)
        {
            UIData dialogData = this.dialogData.Pop();
            this.spawnDialog(dialogData);
        }
    }

    public void showBoard(string url, object[] args, UIData.callback callback)
    {
        UIData uiData = new UIData(url, args, callback);
        this.boardData = uiData;
    }

    public void showDialog(string url, object[] args, UIData.callback callback)
    {
        UIData uiData = new UIData(url, args, callback);
    }

    private void spawnBoard(UIData uiData)
    {
        GameObject prefab = AppContext.instance.resManager.getPrefab(uiData.url);
        GameObject node = AppContext.instance.poolManager.getPool(prefab);
        Transform nodeTransform = node.transform;
        nodeTransform.SetParent(boardRoot.transform);
        nodeTransform.localPosition = new Vector3();
        BaseUI ui = node.GetComponent<BaseUI>();

        // 同时只允许一个Board，关闭上一个Board
        closeBoard();

        this.curBoard = new UIWrapper(uiData.url, node, ui, uiData.args);
        ui.OnShow(uiData.args);
        if (uiData.myCallback != null)
        {
            uiData.myCallback();
        }
    }

    private void spawnDialog(UIData uiData)
    {
        foreach (UIWrapper uiWrapper in curDialog)
        {
            if (uiWrapper.url != uiData.url)
            {
                continue;
            }

            Debug.LogError("重复显示UI：" + uiData.url);
            closeDialog(uiData.url);
        }

        GameObject prefab = AppContext.instance.resManager.getPrefab(uiData.url);
        GameObject node = AppContext.instance.poolManager.getPool(prefab);
        Transform nodeTransform = node.transform;
        nodeTransform.SetParent(dialogRoot.transform);
        nodeTransform.localPosition = new Vector3();
        BaseUI ui = node.GetComponent<BaseUI>();
        // uiWrapper = new UIWrapper(uiData.url, node, ui, uiData.args);
        ui.OnShow(uiData.args);
        if (uiData.myCallback != null)
        {
            uiData.myCallback();
        }
    }

    public void closeBoard()
    {
        if (this.curBoard == null)
        {
            return;
        }

        AppContext.instance.poolManager.setPool(this.curBoard.obj);
        this.curBoard = null;
    }

    public void closeDialog(string url)
    {
    }
}