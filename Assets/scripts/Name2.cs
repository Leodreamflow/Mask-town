using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name2 : MonoBehaviour
{
    bool isShowTip;

    void Start()
    {
        isShowTip = false;
    }

    void OnMouseEnter()
    {
        isShowTip = true;
    }

    void OnMouseExit()
    {
        isShowTip = false;
    }

    void OnGUI()
    {
        if (isShowTip)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 30;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 100, 100), "The Mystery of Masktown is the strongest show of all time!", style);
        }
    }
}
