using UnityEngine;

[CreateAssetMenu(fileName = "New News", menuName = "News")]
public class NewsSpecial : ScriptableObject
{
    new public string name = "New News";

    public string info = "New text";
    public Sprite icon = null;
    //public float NPpoint = 0;

    public string commentarMask01 = null;
    public float NPMask01 = 0;
    public bool articleMask01 = false;

    public string commentarMask02 = null;
    public float NPMask02 = 0;
    public bool articleMask02 = false;

    public string commentarMask03 = null;
    public float NPMask03 = 0;
    public bool articleMask03 = false;

    public string commentarMask04 = null;
    public float NPMask04 = 0;
    public bool articleMask04 = false;

    public string commentarMask05 = null;
    public float NPMask05 = 0;
    public bool articleMask05 = false;
}