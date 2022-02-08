using UnityEngine;

public class UIWrapper
{
    public string url = null;

    public GameObject obj = null;

    public BaseUI ui = null;

    public object[] args = null;

    public UIWrapper(string url, GameObject obj, BaseUI ui, object[] args)
    {
        this.url = url;
        this.obj = obj;
        this.ui = ui;
        this.args = args;
    }
}