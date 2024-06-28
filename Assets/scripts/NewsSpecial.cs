using UnityEngine;

[CreateAssetMenu(fileName = "New News", menuName = "News")]
public class NewsSpecial : ScriptableObject
{
    new public string name = "New News";

    public string info = "New text";
    public Sprite icon = null;
    //public float NPpoint = 0;

    public string commentarMZ = "new comment";
    public float NPMZ = 0;
}