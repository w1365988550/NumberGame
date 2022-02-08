using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameBoard : BaseUI
{
    /** 界面属性预制件 */
    public GameObject propertyIconPrefab = null;

    public GameObject parent = null;

    public Transform iconParent = null;

    public List<MyScriptableObject> propertyConfig = new List<MyScriptableObject>();

    private Dictionary<int, string> propertyKIdVName = new Dictionary<int, string>();

    // Start is called before the first frame update
    void Start()
    {
        this.propertyKIdVName.Add(0, "血量");
        this.propertyKIdVName.Add(1, "防御");
        this.propertyKIdVName.Add(2, "攻击");
        this.propertyKIdVName.Add(3, "暴击");
        this.propertyKIdVName.Add(4, "暴击伤害");
        this.propertyKIdVName.Add(5, "闪避");
        this.propertyKIdVName.Add(6, "伤害加深");
        this.propertyKIdVName.Add(7, "固定减伤");
        this.propertyKIdVName.Add(8, "百分比减伤");
        this.propertyKIdVName.Add(9, "血量回复");
        this.propertyKIdVName.Add(10, "攻击眩晕概率");
        this.propertyKIdVName.Add(11, "吸血百分比");
        this.propertyKIdVName.Add(12, "吸血固定值");

        // 伤害计算公式 = 伤害 - 防御
        // 初始伤害 = 攻击 * （暴击？暴击伤害：1） * 伤害加深
        // 结算伤害 = 初始伤害 - 初始伤害 * 百分比减伤 - 固定减伤 - 防御
        // 吸血结算 = 结算伤害 * 吸血百分比 或等于 吸血固定值
        // 眩晕结算 = 攻击结束（命中概率？眩晕1回合：不眩晕）

        for (var i = 0; i < this.propertyConfig.Count; i++)
        {
            MyScriptableObject mso = this.propertyConfig[i];
            GameObject obj = AppContext.instance.poolManager.getPool(this.propertyIconPrefab);
            Transform objTransform = obj.transform;
            objTransform.SetParent(iconParent);
            objTransform.position = new Vector3(mso.pos.x, mso.pos.y);
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mso.size.x);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, mso.size.y);
            PropertyIcon icon = obj.GetComponent<PropertyIcon>();
            icon.Init(mso);
        }
    }

    private void CreateAllPropertyIcon()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnShow(object[] args)
    {
        Debug.Log("onShow");
    }

    public void ClearPool()
    {
        AppContext.instance.poolManager.clearPool();
    }
}