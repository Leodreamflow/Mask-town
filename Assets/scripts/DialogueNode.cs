using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    [TextArea(2, 5)]
    public string text;

    public List<DialogueChoice> choices;
}