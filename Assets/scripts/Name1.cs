using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name1 : MonoBehaviour
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
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 100, 100), "The new TV series the Mystery of Masktown has premiered, and it has set a new record for the highest ratings in the town.", style);
        }
    }
}
