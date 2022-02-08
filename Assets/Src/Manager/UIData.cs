using System;

public class UIData
{
    public string url = null;

    public object[] args = null;

    // 声明委托类型
    public delegate void callback();

    // 定义委托
    public callback myCallback = null;

    public UIData(string url, object[] args, callback myCallback)
    {
        this.url = url;
        this.args = args;
        this.myCallback = myCallback;
    }
}