using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New News", menuName = "Feedbacks")]
public class NewsFeedback : ScriptableObject
{
    new public string name = "New Mask";

    public float NPMask = 0;
    public bool articleMask = false;

}