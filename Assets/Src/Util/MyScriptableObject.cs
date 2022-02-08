using UnityEngine;

[CreateAssetMenu(fileName = "MyScriptableObject", menuName = "MyScriptableObject", order = 1)]
public class MyScriptableObject : ScriptableObject
{
    /** 属性id */
    public int propertyId = 0;

    /** 属性名 */
    public string propertyName = "";

    /** 属性初始值 */
    public int propertyValue = 0;

    /** 位置 */
    public Vector2 pos = new Vector2();

    /** 宽高 */
    public readonly Vector2 size = new Vector2(150, 80);
}
