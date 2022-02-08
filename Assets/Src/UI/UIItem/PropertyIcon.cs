using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PropertyIcon : MonoBehaviour
{
    public Text text = null;

    // Start is called before the first frame update
    public void Init(MyScriptableObject mso)
    {
        text.text = mso.propertyName + ":" + mso.propertyValue;
        gameObject.transform.localPosition = mso.pos;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ClickButton()
    {
        Debug.Log("点击了：" + text.text);
        AppContext.instance.poolManager.setPool(gameObject);
    }
}